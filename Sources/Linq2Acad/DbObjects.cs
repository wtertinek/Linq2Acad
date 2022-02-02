using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;

namespace Linq2Acad
{
  public class DbObjects
  {
    private readonly Database database;
    private readonly Transaction transaction;

    internal DbObjects(Database database, Transaction transaction)
    {
      this.database = database;
      this.transaction = transaction;
    }

    /// <summary>
    /// Returns the database object with the given ObjectId. The object is readonly.
    /// </summary>
    /// <typeparam name="T">The type of the object.</typeparam>
    /// <param name="id">The id of the object.</param>
    /// <exception cref="System.ArgumentOutOfRangeException">Thrown when an invalid ObjectId is passed.</exception>
    /// <exception cref="System.InvalidCastException">Thrown when the object cannot be casted to the target type.</exception>
    /// <exception cref="System.Exception">Thrown when getting the element throws an exception.</exception>
    /// <returns>The object with the given ObjectId.</returns>
    public T Element<T>(ObjectId id) where T : DBObject
    {
      Require.NotDisposed(database.IsDisposed, nameof(AcadDatabase));
      Require.IsValid(id, nameof(id));

      return ElementInternal<T>(id, false);
    }

    /// <summary>
    /// Returns the database object with the given ObjectId.
    /// </summary>
    /// <typeparam name="T">The type of the object.</typeparam>
    /// <param name="id">The id of the object.</param>
    /// <param name="forWrite">True, if the object should be opened for-write.</param>
    /// <exception cref="System.ArgumentOutOfRangeException">Thrown when an invalid ObjectId is passed.</exception>
    /// <exception cref="System.InvalidCastException">Thrown when the object cannot be casted to the target type.</exception>
    /// <exception cref="System.Exception">Thrown when getting the element throws an exception.</exception>
    /// <returns>The object with the given ObjectId.</returns>
    public T Element<T>(ObjectId id, bool forWrite) where T : DBObject
    {
      Require.NotDisposed(database.IsDisposed, nameof(AcadDatabase));
      Require.IsValid(id, nameof(id));

      return ElementInternal<T>(id, forWrite);
    }

    /// <summary>
    /// Returns the database object with the given ObjectId, or a default value if the element does not exist. The object is readonly.
    /// </summary>
    /// <typeparam name="T">The type of the object.</typeparam>
    /// <param name="id">The id of the object.</param>
    /// <exception cref="System.ArgumentOutOfRangeException">Thrown when an invalid ObjectId is passed.</exception>
    /// <exception cref="System.InvalidCastException">Thrown when the object cannot be casted to the target type.</exception>
    /// <exception cref="System.Exception">Thrown when getting the element throws an exception.</exception>
    /// <returns>The object with the given ObjectId.</returns>
    public T ElementOrDefault<T>(ObjectId id) where T : DBObject
    {
      Require.NotDisposed(database.IsDisposed, nameof(AcadDatabase));

      if (!id.IsValid)
      {
        return null;
      }
      else
      {
        return ElementInternal<T>(id, false);
      }
    }

    /// <summary>
    /// Returns the database object with the given ObjectId, or a default value if the element does not exist.
    /// </summary>
    /// <typeparam name="T">The type of the object.</typeparam>
    /// <param name="id">The id of the object.</param>
    /// <param name="forWrite">True, if the object should be opened for-write.</param>
    /// <exception cref="System.ArgumentOutOfRangeException">Thrown when an invalid ObjectId is passed.</exception>
    /// <exception cref="System.InvalidCastException">Thrown when the object cannot be casted to the target type.</exception>
    /// <exception cref="System.Exception">Thrown when getting the element throws an exception.</exception>
    /// <returns>The object with the given ObjectId.</returns>
    public T ElementOrDefault<T>(ObjectId id, bool forWrite) where T : DBObject
    {
      Require.NotDisposed(database.IsDisposed, nameof(AcadDatabase));

      if (!id.IsValid)
      {
        return null;
      }
      else
      {
        return ElementInternal<T>(id, forWrite);
      }
    }

    /// <summary>
    /// Returns the database object with the given ObjectId.
    /// </summary>
    /// <typeparam name="T">The type of the object.</typeparam>
    /// <param name="id">The id of the object.</param>
    /// <param name="forWrite">True, if the object should be opened for-write.</param>
    /// <returns>The object with the given ObjectId.</returns>
    private T ElementInternal<T>(ObjectId id, bool forWrite) where T : DBObject
    {
      Require.NotDisposed(database.IsDisposed, nameof(AcadDatabase));

      return (T)transaction.GetObject(id, forWrite ? OpenMode.ForWrite : OpenMode.ForRead);
    }

    /// <summary>
    /// Returns the database objects with the given ObjectIds. The objects are readonly.
    /// </summary>
    /// <typeparam name="T">The type of the objects.</typeparam>
    /// <param name="ids">The ids of the objects.</param>
    /// <exception cref="System.ArgumentNullException">Thrown when parameter <i>ids</i> is null.</exception>
    /// <exception cref="System.InvalidCastException">Thrown when an object cannot be casted to the target type.</exception>
    /// <exception cref="System.Exception">Thrown when an ObjectId is invalid or getting an element throws an exception.</exception>
    /// <returns>The objects with the given ObjectIds.</returns>
    public IEnumerable<T> Elements<T>(IEnumerable<ObjectId> ids) where T : DBObject
    {
      Require.NotDisposed(database.IsDisposed, nameof(AcadDatabase));
      Require.ElementsValid(ids, nameof(ids));

      return ElementsInternal<T>(ids, false);
    }

    /// <summary>
    /// Returns the database objects with the given ObjectIds.
    /// </summary>
    /// <typeparam name="T">The type of the objects.</typeparam>
    /// <param name="ids">The ids of the objects.</param>
    /// <param name="forWrite">True, if the objects should be opened for-write.</param>
    /// <exception cref="System.ArgumentNullException">Thrown when parameter <i>ids</i> is null.</exception>
    /// <exception cref="System.InvalidCastException">Thrown when an object cannot be casted to the target type.</exception>
    /// <exception cref="System.Exception">Thrown when an ObjectId is invalid or getting an element throws an exception.</exception>
    /// <returns>The objects with the given ObjectIds.</returns>
    public IEnumerable<T> Elements<T>(IEnumerable<ObjectId> ids, bool forWrite) where T : DBObject
    {
      Require.NotDisposed(database.IsDisposed, nameof(AcadDatabase));
      Require.ElementsValid(ids, nameof(ids));

      return ElementsInternal<T>(ids, forWrite);
    }

    /// <summary>
    /// Returns the database objects with the given ObjectIds. The objects are readonly.
    /// </summary>
    /// <typeparam name="T">The type of the objects.</typeparam>
    /// <param name="ids">The ids of the objects.</param>
    /// <exception cref="System.ArgumentNullException">Thrown when parameter <i>ids</i> is null.</exception>
    /// <exception cref="System.InvalidCastException">Thrown when an object cannot be casted to the target type.</exception>
    /// <exception cref="System.Exception">Thrown when an ObjectId is invalid or getting an element throws an exception.</exception>
    /// <returns>The objects with the given ObjectIds.</returns>
    public IEnumerable<T> Elements<T>(ObjectIdCollection ids) where T : DBObject
    {
      Require.NotDisposed(database.IsDisposed, nameof(AcadDatabase));
      Require.ParameterNotNull(ids, nameof(ids));
      Require.ElementsValid(ids.Cast<ObjectId>(), nameof(ids));

      return ElementsInternal<T>(ids.Cast<ObjectId>(), false);
    }

    /// <summary>
    /// Returns the database objects with the given ObjectIds.
    /// </summary>
    /// <typeparam name="T">The type of the objects.</typeparam>
    /// <param name="ids">The ids of the objects.</param>
    /// <param name="forWrite">True, if the objects should be opened for-write.</param>
    /// <exception cref="System.ArgumentNullException">Thrown when parameter <i>ids</i> is null.</exception>
    /// <exception cref="System.InvalidCastException">Thrown when an object cannot be casted to the target type.</exception>
    /// <exception cref="System.Exception">Thrown when an ObjectId is invalid or getting an element throws an exception.</exception>
    /// <returns>The objects with the given ObjectIds.</returns>
    public IEnumerable<T> Elements<T>(ObjectIdCollection ids, bool forWrite) where T : DBObject
    {
      Require.NotDisposed(database.IsDisposed, nameof(AcadDatabase));
      Require.ParameterNotNull(ids, nameof(ids));
      Require.ElementsValid(ids.Cast<ObjectId>(), nameof(ids));

      return ElementsInternal<T>(ids.Cast<ObjectId>(), forWrite);
    }

    /// <summary>
    /// Returns the database objects with the given ObjectIds.
    /// </summary>
    /// <typeparam name="T">The type of the objects.</typeparam>
    /// <param name="ids">The ids of the objects.</param>
    /// <param name="forWrite">True, if the objects should be opened for-write.</param>
    /// <returns>The objects with the given ObjectIds.</returns>
    private IEnumerable<T> ElementsInternal<T>(IEnumerable<ObjectId> ids, bool forWrite) where T : DBObject
    {
      var openMode = forWrite ? OpenMode.ForWrite : OpenMode.ForRead;

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
