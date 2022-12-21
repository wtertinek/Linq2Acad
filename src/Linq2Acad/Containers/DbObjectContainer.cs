using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;

namespace Linq2Acad
{
  /// <summary>
  /// A container class that provides access to all database objects. In addition to the standard LINQ operations this class provides a method to add newly created DBObjects.
  /// </summary>
  public class DbObjectContainer
  {
    private readonly Database database;
    private readonly Transaction transaction;

    internal DbObjectContainer(Database database, Transaction transaction)
    {
      this.database = database;
      this.transaction = transaction;
    }

    /// <summary>
    /// Returns the database object with the given ObjectId.
    /// </summary>
    /// <typeparam name="T">The type of the object.</typeparam>
    /// <param name="id">The ID of the object.</param>
    /// <param name="openForWrite">True, if the object should be opened for-write. By default the object is opened readonly.</param>
    /// <returns>The object with the given ObjectId.</returns>
    public T Element<T>(ObjectId id, bool openForWrite = false) where T : DBObject
    {
      Require.NotDisposed(database.IsDisposed, nameof(AcadDatabase));
      Require.TransactionNotDisposed(transaction.IsDisposed);
      Require.IsValid(id, nameof(id));

      return ElementInternal<T>(id, openForWrite);
    }

    /// <summary>
    /// Returns the database object with the given ObjectId, or <i>null</i> if the element does not exist.
    /// </summary>
    /// <typeparam name="T">The type of the object.</typeparam>
    /// <param name="id">The ID of the object.</param>
    /// <param name="openForWrite">True, if the object should be opened for-write. By default the object is opened readonly.</param>
    /// <returns>The object with the given ObjectId.</returns>
    public T ElementOrDefault<T>(ObjectId id, bool openForWrite = false) where T : DBObject
    {
      Require.NotDisposed(database.IsDisposed, nameof(AcadDatabase));
      Require.TransactionNotDisposed(transaction.IsDisposed);

      return !id.IsValid ? null : ElementInternal<T>(id, openForWrite);
    }

    /// <summary>
    /// Returns the database object with the given ObjectId.
    /// </summary>
    /// <typeparam name="T">The type of the object.</typeparam>
    /// <param name="id">The ID of the object.</param>
    /// <param name="openForWrite">True, if the object should be opened for-write.</param>
    /// <returns>The object with the given ObjectId.</returns>
    private T ElementInternal<T>(ObjectId id, bool openForWrite) where T : DBObject
    {
      Require.NotDisposed(database.IsDisposed, nameof(AcadDatabase));
      Require.TransactionNotDisposed(transaction.IsDisposed);

      return (T)transaction.GetObject(id, openForWrite ? OpenMode.ForWrite : OpenMode.ForRead);
    }

    /// <summary>
    /// Returns the database objects with the given ObjectIds.
    /// </summary>
    /// <typeparam name="T">The type of the objects.</typeparam>
    /// <param name="ids">The ids of the objects.</param>
    /// <param name="openForWrite">True, if the objects should be opened for-write. By default the objects are opened readonly.</param>
    /// <returns>The objects with the given ObjectIds.</returns>
    public IEnumerable<T> Elements<T>(IEnumerable<ObjectId> ids, bool openForWrite = false) where T : DBObject
    {
      Require.NotDisposed(database.IsDisposed, nameof(AcadDatabase));
      Require.TransactionNotDisposed(transaction.IsDisposed);
      Require.ParameterNotNull(ids, nameof(ids));
      Require.ElementsValid(ids, nameof(ids));

      return ElementsInternal<T>(ids, openForWrite);
    }

    /// <summary>
    /// Returns the database objects with the given ObjectIds.
    /// </summary>
    /// <typeparam name="T">The type of the objects.</typeparam>
    /// <param name="ids">The ids of the objects.</param>
    /// <param name="openForWrite">True, if the objects should be opened for-write. By default the objects are opened readonly.</param>
    /// <returns>The objects with the given ObjectIds.</returns>
    public IEnumerable<T> Elements<T>(ObjectIdCollection ids, bool openForWrite = false) where T : DBObject
    {
      Require.NotDisposed(database.IsDisposed, nameof(AcadDatabase));
      Require.TransactionNotDisposed(transaction.IsDisposed);
      Require.ParameterNotNull(ids, nameof(ids));
      Require.ElementsValid(ids.Cast<ObjectId>(), nameof(ids));

      return ElementsInternal<T>(ids.Cast<ObjectId>(), openForWrite);
    }

    /// <summary>
    /// Returns the database objects with the given ObjectIds.
    /// </summary>
    /// <typeparam name="T">The type of the objects.</typeparam>
    /// <param name="ids">The ids of the objects.</param>
    /// <param name="openForWrite">True, if the objects should be opened for-write.</param>
    /// <returns>The objects with the given ObjectIds.</returns>
    private IEnumerable<T> ElementsInternal<T>(IEnumerable<ObjectId> ids, bool openForWrite) where T : DBObject
    {
      var openMode = openForWrite ? OpenMode.ForWrite : OpenMode.ForRead;

      foreach (var id in ids)
      {
        yield return (T)transaction.GetObject(id, openMode);
      }
    }

    /// <summary>
    /// Adds the given object to the underlaying transaction. This is only needed for objects that are not stored in containers (e.g. AttributeReference).
    /// </summary>
    /// <param name="obj">The object to add to the transaction.</param>
    public void AddNewlyCreatedDBObject(DBObject obj)
    {
      Require.NotDisposed(database.IsDisposed, nameof(AcadDatabase));
      Require.TransactionNotDisposed(transaction.IsDisposed);
      Require.ParameterNotNull(obj, nameof(obj));

      transaction.AddNewlyCreatedDBObject(obj, true);
    }

    #region Overrides to remove methods from IntelliSense

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public override bool Equals(object obj)
    {
      return base.Equals(obj);
    }

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public override int GetHashCode()
    {
      return base.GetHashCode();
    }

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public override string ToString()
    {
      return base.ToString();
    }

    #endregion
  }
}
