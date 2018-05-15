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

    protected override sealed bool ContainsInternal(ObjectId id)
    {
      return ((DBDictionary)transaction.GetObject(ID, OpenMode.ForRead)).Contains(id);
    }

    protected override sealed bool ContainsInternal(string name)
    {
      return ((DBDictionary)transaction.GetObject(ID, OpenMode.ForRead)).Contains(name);
    }

    public T Element(string name)
    {
      if (name == null) throw Error.ArgumentNull("name");

      try
      {
        return ElementInternal(name, false);
      }
      catch
      {
        throw Error.KeyNotFound(name);
      }
    }

    public T Element(string name, bool forWrite)
    {
      if (name == null) throw Error.ArgumentNull("name");

      try
      {
        return ElementInternal(name, forWrite);
      }
      catch
      {
        throw Error.KeyNotFound(name);
      }
    }

    private T ElementInternal(string name, bool forWrite)
    {
      var dict = (DBDictionary)transaction.GetObject(ID, OpenMode.ForRead);
      var id = dict.GetAt(name);
      return (T)transaction.GetObject(id, forWrite ? OpenMode.ForWrite : OpenMode.ForRead);
    }

    public T ElementOrDefault(string name)
    {
      if (name == null) throw Error.ArgumentNull("name");

      try
      {
        return ElementOrDefaultInternal(name, false);
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e);
      }
    }

    public T ElementOrDefault(string name, bool forWrite)
    {
      if (name == null) throw Error.ArgumentNull("name");

      try
      {
        return ElementOrDefaultInternal(name, forWrite);
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e);
      }
    }

    private T ElementOrDefaultInternal(string name, bool forWrite)
    {
      var dict = (DBDictionary)transaction.GetObject(ID, forWrite ? OpenMode.ForWrite : OpenMode.ForRead);

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
    {
      try
      {
        return ((DBDictionary)transaction.GetObject(ID, OpenMode.ForRead)).Count;
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e);
      }
    }

    protected override sealed void AddRangeInternal(IEnumerable<T> items, IEnumerable<string> names)
    {
      var dict = (DBDictionary)transaction.GetObject(ID, OpenMode.ForWrite);
      var mItems = items.ToArray();
      var mNames = names.ToArray();

      for (int i = 0; i < mItems.Length; i++)
      {
        dict.SetAt(mNames[i], mItems[i]);
        transaction.AddNewlyCreatedDBObject(mItems[i], true);
      }
    }
  }
}
