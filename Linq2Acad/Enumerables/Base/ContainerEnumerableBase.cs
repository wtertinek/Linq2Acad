using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Linq2Acad
{
  public abstract class ContainerEnumerableBase<T> : LazyElementEnumerable<T, ObjectId, DBObject> where T : DBObject
  {
    protected Database database;
    protected Transaction transaction;

    internal protected ContainerEnumerableBase(Database database, Transaction transaction, ObjectId containerID,
                                               Func<object, ObjectId> getID)
      : base(new LazayIdEnumerable<ObjectId>(((IEnumerable)transaction.GetObject(containerID, OpenMode.ForRead)).Cast<object>(), getID),
             new DataProvider(transaction))
    {
      this.database = database;
      this.transaction = transaction;
      ID = containerID;
    }

    internal ObjectId ID { get; private set; }

    public override bool Contains(T value)
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

    public ImportResult<T> Import(T item)
    {
      return Import(item, false);
    }

    public ImportResult<T> Import(T item, bool replaceIfDuplicate)
    {
      if (item == null) Error.ArgumentNull("item");
      return Import(new[] { item }, replaceIfDuplicate).First();
    }

    public IReadOnlyCollection<ImportResult<T>> Import(IEnumerable<T> items, bool replaceIfDuplicate)
    {
      if (items == null) Error.ArgumentNull("items");

      if (items.Any(i => i.Database == database))
      {
        throw Error.Generic("Wrong database origin");
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

    #region Nested class DataProvider

    private class DataProvider : IDataProvider<ObjectId, DBObject>
    {
      private Transaction transaction;

      public DataProvider(Transaction transaction)
      {
        this.transaction = transaction;
      }

      public TElement GetElement<TElement>(ObjectId id) where TElement : DBObject
      {
        return (TElement)transaction.GetObject(id, OpenMode.ForRead);
      }

      public ObjectId GetId<TElement>(TElement element) where TElement : DBObject
      {
        return element.ObjectId;
      }

      public IEnumerable<ObjectId> Filter<TElement>(IEnumerable<ObjectId> ids) where TElement : DBObject
      {
        // TODO: What is TElement's RXClass if it is a derived type?
        var rxType = Autodesk.AutoCAD.Runtime.RXClass.GetClass(typeof(TElement));
        return ids.Where(id => id.ObjectClass.IsDerivedFrom(rxType));
      }
    }

    #endregion
  }
}
