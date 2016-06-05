using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;

namespace Linq2Acad
{
  public abstract class DBDictionaryEnumerable<T> : NameBasedEnumerableBase<T> where T : DBObject
  {
    protected DBDictionaryEnumerable(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID, i => ((DBDictionaryEntry)i).Value)
    {
    }

    public override sealed bool Contains(ObjectId id)
    {
      return ((DBDictionary)transaction.GetObject(ID, OpenMode.ForRead)).Contains(id);
    }

    public override sealed bool Contains(string name)
    {
      return ((DBDictionary)transaction.GetObject(ID, OpenMode.ForRead)).Contains(name);
    }

    public override sealed T Element(string name)
    {
      var dict = (DBDictionary)transaction.GetObject(ID, OpenMode.ForRead);

      try
      {
        var id = dict.GetAt(name);

        if (id.IsValid)
        {
          return (T)transaction.GetObject(id, OpenMode.ForRead);
        }
        else
        {
          throw Error.KeyNotFound(name);
        }
      }
      catch
      {
        throw Error.KeyNotFound(name);
      }
    }

    public override sealed int Count()
    {
      return ((DBDictionary)transaction.GetObject(ID, OpenMode.ForRead)).Count;
    }

    public void Add(string name, T item)
    {
      Set(name, item, dict =>
                      {
                        if (dict.Contains(name))
                        {
                          throw Error.Generic(typeof(T).Name + " " + name + " already exists");
                        }
                      });
    }

    public void Set(string name, T item)
    {
      Set(name, item, null);
    }

    private void Set(string name, T item, Action<DBDictionary> check)
    {
      if (!AcadDatabase.IsNameValid(name))
      {
        throw Error.InvalidName(name);
      }

      var dict = (DBDictionary)transaction.GetObject(ID, OpenMode.ForWrite);

      if (check != null)
      {
        check(dict);
      }

      dict.SetAt(name, item);
      transaction.AddNewlyCreatedDBObject(item, true);
    }

    protected abstract T CreateNew();

    public T Create(string name)
    {
      if (!AcadDatabase.IsNameValid(name))
      {
        throw Error.InvalidName(name);
      }

      var item = CreateNew();
      Add(name, item);
      return item;
    }
  }
}
