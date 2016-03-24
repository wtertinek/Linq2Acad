using Autodesk.AutoCAD.DatabaseServices;
using RXClass = Autodesk.AutoCAD.Runtime.RXClass;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2Acad
{
  public abstract class EnumerableBase<T> : IEnumerable<T> where T : DBObject
  {
    protected Database database;
    protected Transaction transaction;

    protected EnumerableBase(Database database, Transaction transaction, ObjectId containerID)
    {
      this.database = database;
      this.transaction = transaction;
      this.ID = containerID;
    }

    internal ObjectId ID { get; private set; }

    public IEnumerator<T> GetEnumerator()
    {
      var enumerable = (IEnumerable)transaction.GetObject(ID, OpenMode.ForRead);

      foreach (var item in enumerable)
      {
        yield return (T)transaction.GetObject(GetObjectID(item), OpenMode.ForRead);
      }
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }

    protected abstract ObjectId GetObjectID(object iteratorItem);

    public abstract bool Contains(ObjectId id);

    public T Element(ObjectId id)
    {
      return (T)transaction.GetObject(id, OpenMode.ForRead);
    }

    public abstract int Count();

    public abstract long LongCount();

    public IEnumerable<TResult> OfType<TResult>() where TResult : T
    {
      var enumerator = ((IEnumerable)transaction.GetObject(ID, OpenMode.ForRead)).GetEnumerator();
      // TODO: To check: What is the RXClass of a derived type? RXClass of the base type?
      var rxType = RXClass.GetClass(typeof(TResult));

      while (enumerator.MoveNext())
      {
        var id = GetObjectID(enumerator.Current);

        if (!id.ObjectClass.IsDerivedFrom(rxType))
        {
          continue;
        }

        var item = transaction.GetObject(id, OpenMode.ForRead) as TResult;

        if (item == null)
        {
          continue;
        }

        yield return item;
      }
    }

    public ImportResult<T> Import(T item)
    {
      return Import(item, false);
    }

    public ImportResult<T> Import(T item, bool replaceIfDuplicate)
    {
      return Import(new[] { item }, replaceIfDuplicate).First();
    }

    public IReadOnlyCollection<ImportResult<T>> Import(IEnumerable<T> items, bool replaceIfDuplicate)
    {
      if (items.Any(i => i.Database == database))
      {
        throw new Exception("Wrong database origin");
      }

      var result = new List<ImportResult<T>>();

      foreach (var item in items)
      {
        var ids = new ObjectIdCollection(new [] { item.ObjectId });
        var mapping = new IdMapping();
        var type = replaceIfDuplicate ? DuplicateRecordCloning.Replace : DuplicateRecordCloning.Ignore;
        database.WblockCloneObjects(ids, ID, mapping, type, false);

        result.Add(new ImportResult<T>((T)transaction.GetObject(mapping[item.ObjectId].Value, OpenMode.ForRead), mapping));
      }

      return result;
    }
  }

  public abstract class NameBasedEnumerableBase<T> : EnumerableBase<T> where T : DBObject
  {
    protected NameBasedEnumerableBase(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID)
    {
    }

    public abstract bool Contains(string name);

    public abstract T Element(string name);
  }
}
