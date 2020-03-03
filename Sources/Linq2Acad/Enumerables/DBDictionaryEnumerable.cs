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
    protected internal DBDictionaryEnumerable(Database database, Transaction transaction, ObjectId containerID)
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
      Require.StringNotEmpty(name, nameof(name));
      Require.NameExists<T>(Contains(name), name);

      return ElementInternal(name, false);
    }

    public T Element(string name, bool forWrite)
    {
      Require.StringNotEmpty(name, nameof(name));
      Require.NameExists<T>(Contains(name), name);

      return ElementInternal(name, forWrite);
    }

    private T ElementInternal(string name, bool forWrite)
    {
      var dict = (DBDictionary)transaction.GetObject(ID, OpenMode.ForRead);
      var id = dict.GetAt(name);
      return (T)transaction.GetObject(id, forWrite ? OpenMode.ForWrite : OpenMode.ForRead);
    }

    public T ElementOrDefault(string name)
    {
      Require.StringNotEmpty(name, nameof(name));

      return ElementOrDefaultInternal(name, false);
    }

    public T ElementOrDefault(string name, bool forWrite)
    {
      Require.StringNotEmpty(name, nameof(name));

      return ElementOrDefaultInternal(name, forWrite);
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
      return ((DBDictionary)transaction.GetObject(ID, OpenMode.ForRead)).Count;
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
