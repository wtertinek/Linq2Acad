using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;

namespace Linq2Acad
{
  public abstract class DBDictionaryEnumerable<T> : NameBasedContainerEnumerableBase<T> where T : DBObject
  {
    protected DBDictionaryEnumerable(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID, i => (ObjectId)((DictionaryEntry)i).Value)
    {
    }

    protected T AddInternal(T newItem, string name)
    {
      AddRangeInternal(new[] { Tuple.Create(newItem, name) });
      return newItem;
    }

    protected void AddRangeInternal(IEnumerable<Tuple<T, string>> elements)
    {
      var dict = (DBDictionary)transaction.GetObject(ID, OpenMode.ForWrite);

      foreach (var element in elements)
      {
        dict.SetAt(element.Item2, element.Item1);
        transaction.AddNewlyCreatedDBObject(element.Item1, true);
      }
    }

    protected override sealed bool ContainsInternal(ObjectId id)
      => ((DBDictionary)transaction.GetObject(ID, OpenMode.ForRead)).Contains(id);

    protected override sealed bool ContainsInternal(string name)
      => ((DBDictionary)transaction.GetObject(ID, OpenMode.ForRead)).Contains(name);

    /// <summary>
    /// Returns the element with the specified name.
    /// </summary>
    /// <param name="name">The name of the element.</param>
    /// <param name="openForWrite">True, if the object should be opened for-write. By default the object is opened readonly.</param>
    /// <returns>The element with the specified name.</returns>
    public T Element(string name, bool openForWrite = false)
    {
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

    public override sealed int Count()
      => ((DBDictionary)transaction.GetObject(ID, OpenMode.ForRead)).Count;
  }
}
