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
      var dict = (DBDictionary)transaction.GetObject(ContainerID, OpenMode.ForRead);
      return dict.Contains(name);
    }

    public override sealed bool Contains(ObjectId id)
    {
      var dict = (DBDictionary)transaction.GetObject(ContainerID, OpenMode.ForRead);
      return dict.Contains(id);
    }

    public override sealed T Item(string name)
    {
      var dict = (DBDictionary)transaction.GetObject(ContainerID, OpenMode.ForRead);
      return (T)transaction.GetObject(dict.GetAt(name), OpenMode.ForRead);
    }

    public override sealed IEnumerable<T> Items(IEnumerable<string> names)
    {
      var dict = (DBDictionary)transaction.GetObject(ContainerID, OpenMode.ForRead);

      foreach (var name in names)
      {
        yield return (T)transaction.GetObject(dict.GetAt(name), OpenMode.ForRead);
      }
    }

    public override sealed int Count()
    {
      return ((DBDictionary)transaction.GetObject(ContainerID, OpenMode.ForRead)).Count;
    }

    public ObjectId Add(string name, T item)
    {
      return AddRange(new[] { name }, new[] { item }).First();
    }

    public IEnumerable<ObjectId> AddRange(IEnumerable<string> names, IEnumerable<T> items)
    {
      var a_names = names.ToArray();
      var a_items = items.ToArray();

      if (a_names.Length != a_items.Length)
      {
        throw new ArgumentException();
      }

      var dict = (DBDictionary)transaction.GetObject(ContainerID, OpenMode.ForWrite);

      for (int i = 0; i < a_items.Length; i++)
      {
        if (dict.Contains(a_names[i]))
        {
          throw new Exception(typeof(T).Name + " \"" + a_names[i] + "\" already exists");
        }

        var id = dict.SetAt(a_names[i], a_items[i]);
       transaction.AddNewlyCreatedDBObject(a_items[i], true);
        yield return id;
      }
    }

    protected abstract T CreateNew();

    public T Create(string name)
    {
      var item = CreateNew();
      Add(name, item);
      return item;
    }

    public IEnumerable<T> Create(IEnumerable<string> names)
    {
      var items = names.Select(n => CreateNew())
                      .ToArray();
      AddRange(names, items);
      return items;
    }
  }
}
