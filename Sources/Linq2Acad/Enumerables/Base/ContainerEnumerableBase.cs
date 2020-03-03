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

    protected internal ContainerEnumerableBase(Database database, Transaction transaction, ObjectId containerID, Func<object, ObjectId> getID)
      : base(new LazayIdEnumerable<ObjectId>(((IEnumerable)transaction.GetObject(containerID, OpenMode.ForRead)).Cast<object>(), getID),
             new DataProvider(transaction))
    {
      this.database = database;
      this.transaction = transaction;
      ID = containerID;
    }

    protected internal ContainerEnumerableBase(Database database, Transaction transaction, ObjectId containerID, Func<object, ObjectId> getID,
                                               Func<IEnumerable<ObjectId>, IEnumerable<ObjectId>> filter)
      : base(new MaterializedIdEnumerable<ObjectId>(filter(((IEnumerable)transaction.GetObject(containerID, OpenMode.ForRead)).Cast<object>().Select(i => getID(i)))),
             new DataProvider(transaction))
    {
      this.database = database;
      this.transaction = transaction;
      ID = containerID;
    }

    internal ObjectId ID { get; private set; }

    public override bool Contains(T value)
    {
      if (value == null) throw Error.ArgumentNull("value");
      
      try
      {
        return ContainsInternal(value.ObjectId);
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e);
      }
    }

    public bool Contains(ObjectId id)
    {
      if (!id.IsValid)
      {
        return false;
      }
      else
      {
        try
        {
          return ContainsInternal(id);
        }
        catch (Exception e)
        {
          throw Error.AutoCadException(e);
        }
      }
    }

    protected virtual bool ContainsInternal(ObjectId id)
    {
      return IDs.Any(oid => oid.Equals(id));
    }

    public T Element(ObjectId id)
    {
      if (!id.IsValid) throw Error.InvalidObject("ObjectId");

      try
      {
        return ElementInternal(id, false);
      }
      catch (InvalidCastException e)
      {
        throw e;
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e);
      }
    }

    public T Element(ObjectId id, bool forWrite)
    {
      if (!id.IsValid) throw Error.InvalidObject("ObjectId");
      
      try
      {
        return ElementInternal(id, forWrite);
      }
      catch (InvalidCastException e)
      {
        throw e;
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e);
      }
    }

    public T ElementOrDefault(ObjectId id)
    {
      if (!id.IsValid)
      {
        return null;
      }
      else
      {
        try
        {
          return ElementInternal(id, false);
        }
        catch (InvalidCastException e)
        {
          throw e;
        }
        catch (Exception e)
        {
          throw Error.AutoCadException(e);
        }
      }
    }

    public T ElementOrDefault(ObjectId id, bool forWrite)
    {
      if (!id.IsValid)
      {
        return null;
      }
      else
      {
        try
        {
          return ElementInternal(id, forWrite);
        }
        catch (InvalidCastException e)
        {
          throw e;
        }
        catch (Exception e)
        {
          throw Error.AutoCadException(e);
        }
      }
    }

    private T ElementInternal(ObjectId id, bool forWrite)
    {
      return (T)transaction.GetObject(id, forWrite ? OpenMode.ForWrite : OpenMode.ForRead);
    }

    public ImportResult<T> Import(T item)
    {
      if (item == null) Error.ArgumentNull("item");

      try
      {
        return ImportInternal(item, false);
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e);
      }
    }

    public ImportResult<T> Import(T item, bool replaceIfDuplicate)
    {
      if (item == null) Error.ArgumentNull("item");

      try
      {
        return ImportInternal(item, replaceIfDuplicate);
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e);
      }
    }

    public IReadOnlyCollection<ImportResult<T>> Import(IEnumerable<T> items, bool replaceIfDuplicate)
    {
      if (items == null) Error.ArgumentNull("items");
      if (items.Any(i => i.Database == database)) throw Error.Generic("Wrong database origin");

      var result = new List<ImportResult<T>>();

      foreach (var item in items)
      {
        try
        {
          result.Add(ImportInternal(item, replaceIfDuplicate));
        }
        catch (Exception e)
        {
          throw Error.AutoCadException(e);
        }
      }

      return result;
    }

    private ImportResult<T> ImportInternal(T item, bool replaceIfDuplicate)
    {
      using (var idCollection = new ObjectIdCollection(new[] { item.ObjectId }))
      using (var mapping = new IdMapping())
      {
        var type = replaceIfDuplicate ? DuplicateRecordCloning.Replace : DuplicateRecordCloning.Ignore;
        database.WblockCloneObjects(idCollection, ID, mapping, type, false);

        return new ImportResult<T>((T)transaction.GetObject(mapping[item.ObjectId].Value, OpenMode.ForRead), mapping);
      }
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
