using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;

namespace Linq2Acad
{
  public abstract class DBDictionaryEnumerableBase<T> : NameBasedContainerEnumerableBase<T> where T : DBObject
  {
    private readonly Func<T, string> getName;
    private readonly Func<string> getNamePropertyName;

    protected DBDictionaryEnumerableBase(Database database, Transaction transaction, ObjectId containerID,
                                     Func<T, string> getName, Func<string> getNamePropertyName)
      : base(database, transaction, containerID, i => (ObjectId)((DictionaryEntry)i).Value)
    {
      this.getName = getName;
      this.getNamePropertyName = getNamePropertyName;
    }

    /// <summary>
    /// Adds a newly created element.
    /// </summary>
    /// <param name="element">The element to add.</param>
    public void Add(T element)
    {
      Require.NotDisposed(database.IsDisposed, nameof(AcadDatabase));
      Require.TransactionNotDisposed(transaction.IsDisposed);
      Require.ParameterNotNull(element, nameof(element));

      var name = getName(element);
      Require.IsValidSymbolName(name, getNamePropertyName());
      Require.NameDoesNotExist<T>(Contains(name), name);

      AddInternal(element, name);
    }

    /// <summary>
    /// Adds a range of newly created elements.
    /// </summary>
    /// <param name="elements">The elements to add.</param>
    public void AddRange(IEnumerable<T> elements)
    {
      Require.NotDisposed(database.IsDisposed, nameof(AcadDatabase));
      Require.TransactionNotDisposed(transaction.IsDisposed);
      Require.ParameterNotNull(elements, nameof(elements));

      var namePropertyName = getNamePropertyName();

      foreach (var element in elements)
      {
        Require.ParameterNotNull(element, nameof(element));

        var name = getName(element);
        Require.IsValidSymbolName(name, namePropertyName);
        Require.NameDoesNotExist<T>(Contains(name), name);
      }

      AddRangeInternal(elements.Select(i => Tuple.Create(i, getName(i))));
    }

    protected T AddInternal(T newItem, string name)
    {
      AddRangeInternal(new[] { Tuple.Create(newItem, name) });
      return newItem;
    }

    private void AddRangeInternal(IEnumerable<Tuple<T, string>> elements)
    {
      var dict = (DBDictionary)transaction.GetObject(ID, OpenMode.ForWrite);

      foreach (var element in elements)
      {
        dict.SetAt(element.Item2, element.Item1);
        transaction.AddNewlyCreatedDBObject(element.Item1, true);
      }
    }

    protected sealed override bool ContainsInternal(ObjectId id)
      => ((DBDictionary)transaction.GetObject(ID, OpenMode.ForRead)).Contains(id);

    protected sealed override bool ContainsInternal(string name)
      => ((DBDictionary)transaction.GetObject(ID, OpenMode.ForRead)).Contains(name);

    /// <summary>
    /// Returns the element with the specified name.
    /// </summary>
    /// <param name="name">The name of the element.</param>
    /// <param name="openForWrite">True, if the object should be opened for-write. By default the object is opened readonly.</param>
    /// <returns>The element with the specified name.</returns>
    public T Element(string name, bool openForWrite = false)
    {
      Require.NotDisposed(database.IsDisposed, nameof(AcadDatabase));
      Require.TransactionNotDisposed(transaction.IsDisposed);
      Require.StringNotEmpty(name, nameof(name));
      Require.NameExists<T>(Contains(name), name);

      return ElementInternal(name, openForWrite);
    }

    private T ElementInternal(string name, bool openForWrite)
    {
      var dict = (DBDictionary)transaction.GetObject(ID, OpenMode.ForRead);
      var id = dict.GetAt(name);
      return (T)transaction.GetObject(id, openForWrite ? OpenMode.ForWrite : OpenMode.ForRead);
    }

    /// <summary>
    /// Returns the element with the specified name or <i>null</i> if the element cannot be found.
    /// </summary>
    /// <param name="name">The name of the element.</param>
    /// <param name="openForWrite">True, if the object should be opened for-write. By default the object is opened readonly.</param>
    /// <returns>The element with the specified name.</returns>
    public T ElementOrDefault(string name, bool openForWrite = false)
    {
      Require.NotDisposed(database.IsDisposed, nameof(AcadDatabase));
      Require.TransactionNotDisposed(transaction.IsDisposed);
      Require.StringNotEmpty(name, nameof(name));

      return ElementOrDefaultInternal(name, openForWrite);
    }

    private T ElementOrDefaultInternal(string name, bool openForWrite)
    {
      var dict = (DBDictionary)transaction.GetObject(ID, openForWrite ? OpenMode.ForWrite : OpenMode.ForRead);

      if (dict.Contains(name))
      {
        var id = dict.GetAt(name);
        return (T)transaction.GetObject(id, OpenMode.ForRead);
      }
      else
      {
        return null;
      }
    }

    public sealed override int Count()
    {
      Require.NotDisposed(database.IsDisposed, nameof(AcadDatabase));
      Require.TransactionNotDisposed(transaction.IsDisposed);

      return ((DBDictionary)transaction.GetObject(ID, OpenMode.ForRead)).Count;
    }
  }

  public abstract class DBDictionaryEnumerable<T> : DBDictionaryEnumerableBase<T> where T : DBObject, new()
  {
    protected DBDictionaryEnumerable(Database database, Transaction transaction, ObjectId containerID,
                                     Func<T, string> getName, Func<string> getNamePropertyName)
      : base(database, transaction, containerID, getName, getNamePropertyName)
    {
    }

    /// <summary>
    /// Creates a new element with the specified name.
    /// </summary>
    /// <param name="name">The unique name of the element.</param>
    public T Create(string name)
    {
      Require.NotDisposed(database.IsDisposed, nameof(AcadDatabase));
      Require.TransactionNotDisposed(transaction.IsDisposed);
      Require.IsValidSymbolName(name, nameof(name));
      Require.NameDoesNotExist<T>(Contains(name), name);

      return AddInternal(new T(), name);
    }
  }
}
