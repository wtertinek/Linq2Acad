using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;

namespace Linq2Acad
{
  public abstract class DBDictionaryEnumerable<T> : NameBasedEnumerable<T> where T : DBObject
  {
    protected DBDictionaryEnumerable(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID)
    {
    }

    protected override sealed ObjectId GetObjectID(object iteratorItem)
    {
      return ((DBDictionaryEntry)iteratorItem).Value;
    }

    public bool IsValidName(string name)
    {
      // TODO: Implement
      return true;
    }

    public override sealed bool Contains(string name)
    {
      return ((DBDictionary)transaction.GetObject(ID, OpenMode.ForRead)).Contains(name);
    }

    public override sealed bool Contains(ObjectId id)
    {
      return ((DBDictionary)transaction.GetObject(ID, OpenMode.ForRead)).Contains(id);
    }

    public override sealed T this[string name]
    {
      get
      {
        var dict = (DBDictionary)transaction.GetObject(ID, OpenMode.ForRead);

        try
        {
          return (T)transaction.GetObject(dict.GetAt(name), OpenMode.ForRead);
        }
        catch
        {
          throw new KeyNotFoundException("No element with key " + name + " found");
        }
      }
    }

    public override sealed IEnumerable<T> Items(IEnumerable<string> names)
    {
      var dict = (DBDictionary)transaction.GetObject(ID, OpenMode.ForRead);

      foreach (var name in names)
      {
        T item = default(T);

        try
        {
          item = (T)transaction.GetObject(dict.GetAt(name), OpenMode.ForRead);
        }
        catch
        {
          throw new KeyNotFoundException("No element with key " + name + " found");
        }

        yield return item;
      }
    }

    public override sealed int Count()
    {
      return ((DBDictionary)transaction.GetObject(ID, OpenMode.ForRead)).Count;
    }

    public override sealed long LongCount()
    {
      return ((DBDictionary)transaction.GetObject(ID, OpenMode.ForRead)).Count;
    }

    public ObjectId Add(string name, T item)
    {
      var dict = (DBDictionary)transaction.GetObject(ID, OpenMode.ForWrite);

      if (dict.Contains(name))
      {
        throw new Exception(typeof(T).Name + " " + name + " already exists");
      }

      var id = dict.SetAt(name, item);
      transaction.AddNewlyCreatedDBObject(item, true);
      return id;
    }

    protected abstract T CreateNew();

    public T Create(string name)
    {
      var item = CreateNew();
      Add(name, item);
      return item;
    }
  }
}
