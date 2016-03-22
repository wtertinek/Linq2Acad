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

    public abstract int Count();

    public abstract long LongCount();

    public IEnumerable<TResult> OfType<TResult>() where TResult : T
    {
      var enumerator = ((IEnumerable)transaction.GetObject(ID, OpenMode.ForRead)).GetEnumerator();
      /* TODO: Doesn't work if TResult is a derived type
         class MyLine : Line { }
         OfType<MyLine>() would return all Lines instead of MyLines only*/
      var rxType = RXClass.GetClass(typeof(TResult));

      while (enumerator.MoveNext())
      {
        var id = GetObjectID(enumerator.Current);

        if (!id.ObjectClass.IsDerivedFrom(rxType))
        {
          continue;
        }

        yield return (TResult)transaction.GetObject(id, OpenMode.ForRead);
      }
    }

    public T ByID(ObjectId id)
    {
      return (T)transaction.GetObject(id, OpenMode.ForRead);
    }

    public IEnumerable<T> ByID(IEnumerable<ObjectId> ids)
    {
      var table = (IEnumerable)transaction.GetObject(ID, OpenMode.ForRead);

      foreach (var id in ids)
      {
        yield return (T)transaction.GetObject(id, OpenMode.ForRead);
      }
    }

    public ImportResult Import(T item)
    {
      return Import(item, false);
    }

    public ImportResult Import(T item, bool replaceIfDuplicate)
    {
      return Import(new[] { item }, replaceIfDuplicate).First();
    }

    public IReadOnlyCollection<ImportResult> Import(IEnumerable<T> items, bool replaceIfDuplicate)
    {
      if (items.Any(i => i.Database == database))
      {
        throw new Exception("Wrong database origin");
      }

      var result = new List<ImportResult>();

      foreach (var item in items)
      {
        var ids = new ObjectIdCollection(new [] { item.ObjectId });
        var mapping = new IdMapping();
        var type = replaceIfDuplicate ? DuplicateRecordCloning.Replace : DuplicateRecordCloning.Ignore;
        database.WblockCloneObjects(ids, ID, mapping, type, false);
        
        result.Add(new ImportResult((BlockTableRecord)transaction.GetObject(mapping[item.ObjectId].Value, OpenMode.ForRead), mapping));
      }

      return result;
    }
  }
}
