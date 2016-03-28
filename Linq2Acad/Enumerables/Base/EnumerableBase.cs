using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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

    private IEnumerable<ObjectId> IDs
    {
      get
      {
        var enumerable = (IEnumerable)transaction.GetObject(ID, OpenMode.ForRead);

        foreach (var item in enumerable)
        {
          yield return GetObjectID(item);
        }
      }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }

    protected abstract ObjectId GetObjectID(object iteratorItem);

    public bool Contains(T value)
    {
      return Contains(value.ObjectId);
    }

    public virtual bool Contains(ObjectId id)
    {
      return IDs.Any(oid => oid.Equals(id));
    }

    public T Element(ObjectId id)
    {
      return (T)transaction.GetObject(id, OpenMode.ForRead);
    }

    #region IEnumerable<T> implementations

    public IEnumerable<T> Concat(IEnumerable<T> second)
    {
      if (second == null) throw new ArgumentNullException("second");
      return new ObjectIdEnumerable<T>(transaction, IDs).Concat(second);
    }

    public virtual int Count()
    {
      return new ObjectIdEnumerable<Table>(transaction, IDs).Count();
    }

    public IEnumerable<T> Distinct()
    {
      return new ObjectIdEnumerable<T>(transaction, IDs).Distinct();
    }

    public T ElementAt(int index)
    {
      return new ObjectIdEnumerable<T>(transaction, IDs).ElementAt(index);
    }

    public T ElementAtOrDefault(int index)
    {
      return new ObjectIdEnumerable<T>(transaction, IDs).ElementAtOrDefault(index);
    }

    public IEnumerable<T> Except(IEnumerable<T> second)
    {
      if (second == null) throw new ArgumentNullException("second");
      return new ObjectIdEnumerable<T>(transaction, IDs).Except(second);
    }

    public IEnumerable<T> Intersect(IEnumerable<T> second)
    {
      if (second == null) throw new ArgumentNullException("second");
      return new ObjectIdEnumerable<T>(transaction, IDs).Intersect(second);
    }

    public T Last()
    {
      return new ObjectIdEnumerable<T>(transaction, IDs).Last();
    }

    public T LastOrDefault()
    {
      return new ObjectIdEnumerable<T>(transaction, IDs).LastOrDefault();
    }

    public virtual long LongCount()
    {
      return new ObjectIdEnumerable<Table>(transaction, IDs).LongCount();
    }

    public IEnumerable<TResult> OfType<TResult>() where TResult : T
    {
      return new ObjectIdEnumerable<T>(transaction, IDs).OfType<TResult>();
    }

    public IEnumerable<T> Reverse()
    {
      return new ObjectIdEnumerable<T>(transaction, IDs).Reverse();
    }

    public bool SequenceEqual(IEnumerable<T> second)
    {
      if (second == null) throw new ArgumentNullException("second");
      return new ObjectIdEnumerable<T>(transaction, IDs).SequenceEqual(second);
    }

    public IEnumerable<T> Skip(int count)
    {
      return new ObjectIdEnumerable<T>(transaction, IDs).Skip(count);
    }

    public IEnumerable<T> Take(int count)
    {
      return new ObjectIdEnumerable<T>(transaction, IDs).Take(count);
    }

    public IEnumerable<T> Union(IEnumerable<T> second)
    {
      if (second == null) throw new ArgumentNullException("second");
      return new ObjectIdEnumerable<T>(transaction, IDs).Union(second);
    }

    #endregion

    public ImportResult<T> Import(T item)
    {
      return Import(item, false);
    }

    public ImportResult<T> Import(T item, bool replaceIfDuplicate)
    {
      if (item == null) throw new ArgumentNullException("item");
      return Import(new[] { item }, replaceIfDuplicate).First();
    }

    public IReadOnlyCollection<ImportResult<T>> Import(IEnumerable<T> items, bool replaceIfDuplicate)
    {
      if (items == null) throw new ArgumentNullException("items");

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
