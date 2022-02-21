using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Linq2Acad
{
  public abstract class ContainerEnumerableBase<T> : LazyElementEnumerable<T, ObjectId, DBObject> where T : DBObject
  {
    protected readonly Database database;
    protected readonly Transaction transaction;

    protected ContainerEnumerableBase(Database database, Transaction transaction, ObjectId containerID, Func<object, ObjectId> getID)
      : base(new LazayIdEnumerable<ObjectId>(((IEnumerable)transaction.GetObject(containerID, OpenMode.ForRead)).Cast<object>(), getID),
             new DataProvider(transaction))
    {
      this.database = database;
      this.transaction = transaction;
      ID = containerID;
    }

    protected ContainerEnumerableBase(Database database, Transaction transaction, ObjectId containerID, Func<object, ObjectId> getID,
                                      Func<IEnumerable<ObjectId>, IEnumerable<ObjectId>> filter)
      : base(new MaterializedIdEnumerable<ObjectId>(filter(((IEnumerable)transaction.GetObject(containerID, OpenMode.ForRead)).Cast<object>().Select(i => getID(i)))),
             new DataProvider(transaction))
    {
      this.database = database;
      this.transaction = transaction;
      ID = containerID;
    }

    internal ObjectId ID { get; }

    /// <summary>
    /// Determines whether a sequence contains the specified element.
    /// </summary>
    /// <param name="element">The element to locate in the sequence.</param>
    /// <returns>true if the source sequence contains the specified element; otherwise, false.</returns>
    public override bool Contains(T element)
    {
      Require.ParameterNotNull(element, nameof(element));
      
      return ContainsInternal(element.ObjectId);
    }

    /// <summary>
    /// Determines whether a sequence contains the element with the specified ID.
    /// </summary>
    /// <param name="id">The ID of the object.</param>
    /// <returns>true if the source sequence contains an element that has the specified ID; otherwise, false.</returns>
    public bool Contains(ObjectId id)
      => id.IsValid && ContainsInternal(id);

    protected virtual bool ContainsInternal(ObjectId id)
      => IDs.Any(oid => oid.Equals(id));

    /// <summary>
    /// Returns the element with the specified ID.
    /// </summary>
    /// <param name="id">The ID of the element.</param>
    /// <param name="openForWrite">True, if the object should be opened for-write. By default the object is opened readonly.</param>
    /// <returns>The element with the specified ID.</returns>
    public T Element(ObjectId id, bool openForWrite = false)
    {
      Require.IsValid(id, nameof(id));
      
      return ElementInternal(id, openForWrite);
    }

    /// <summary>
    /// Returns the element with the specified ID or a default value if the element cannot be found.
    /// </summary>
    /// <param name="id">The ID of the element.</param>
    /// <param name="openForWrite">True, if the object should be opened for-write. By default the object is opened readonly.</param>
    /// <returns>The element with the specified ID.</returns>
    public T ElementOrDefault(ObjectId id, bool openForWrite = false)
      => id.IsValid ? ElementInternal(id, openForWrite) : null;

    private T ElementInternal(ObjectId id, bool openForWrite)
      => (T)transaction.GetObject(id, openForWrite ? OpenMode.ForWrite : OpenMode.ForRead);

    public ImportResult<T> Import(T item)
    {
      Require.ParameterNotNull(item, nameof(item));

      return ImportInternal(item, false);
    }

    public ImportResult<T> Import(T item, bool replaceIfDuplicate)
    /// <summary>
    /// Imports the specified element into the current database.
    /// </summary>
    /// <param name="element">The element to import.</param>
    /// <param name="replaceIfDuplicate">true, if the the imported element should be replaced if it is already present; otherwise, false.</param>
    /// <returns>An object that represents the result of an import operation.</returns>
    {
      Require.ParameterNotNull(element, nameof(element));

      return ImportInternal(element, replaceIfDuplicate);
    }

    public IReadOnlyCollection<ImportResult<T>> Import(IEnumerable<T> items, bool replaceIfDuplicate)
    /// <summary>
    /// Imports the specified elements into the current database.
    /// </summary>
    /// <param name="elements">The elements to import.</param>
    /// <param name="replaceIfDuplicate">true, if the the imported element should be replaced if it is already present; otherwise, false.</param>
    /// <returns>An object that represents the result of an import operation.</returns>
    {
      Require.ParameterNotNull(elements, nameof(elements));
      Require.DifferentOrigin(database, elements, nameof(elements));

      var result = new List<ImportResult<T>>();

      foreach (var element in elements)
      {
        result.Add(ImportInternal(element, replaceIfDuplicate));
      }

      return result;
    }

    private ImportResult<T> ImportInternal(T element, bool replaceIfDuplicate)
    {
      using (var idCollection = new ObjectIdCollection(new[] { element.ObjectId }))
      using (var mapping = new IdMapping())
      {
        var type = replaceIfDuplicate ? DuplicateRecordCloning.Replace : DuplicateRecordCloning.Ignore;
        database.WblockCloneObjects(idCollection, ID, mapping, type, false);

        return new ImportResult<T>((T)transaction.GetObject(mapping[element.ObjectId].Value, OpenMode.ForRead), mapping);
      }
    }

    #region Private nested class DataProvider

    private class DataProvider : IDataProvider<ObjectId, DBObject>
    {
      private readonly Transaction transaction;

      public DataProvider(Transaction transaction)
        => this.transaction = transaction;

      public TElement GetElement<TElement>(ObjectId id) where TElement : DBObject
        => (TElement)transaction.GetObject(id, OpenMode.ForRead);

      public ObjectId GetId<TElement>(TElement element) where TElement : DBObject
        => element.ObjectId;

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
