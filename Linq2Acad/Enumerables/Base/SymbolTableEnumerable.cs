using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;

namespace Linq2Acad
{
  public abstract class SymbolTableEnumerable<T> : NameBasedEnumerable<T> where T : SymbolTableRecord
  {
    protected SymbolTableEnumerable(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID)
    {
    }

    protected override sealed ObjectId GetObjectID(object iteratorItem)
    {
      return (ObjectId)iteratorItem;
    }

    public bool IsValidName(string name)
    {
      try
      {
        SymbolUtilityServices.ValidateSymbolName(name, false);
        return true;
      }
      catch
      {
        return false;
      }
    }

    public override sealed bool Contains(string name)
    {
      return ((SymbolTable)transaction.GetObject(containerID, OpenMode.ForRead)).Has(name);
    }

    public override sealed bool Contains(ObjectId id)
    {
      return ((SymbolTable)transaction.GetObject(containerID, OpenMode.ForRead)).Has(id);
    }

    public override sealed T Item(string name)
    {
      var table = (SymbolTable)transaction.GetObject(containerID, OpenMode.ForRead);

      try
      {
        return (T)transaction.GetObject(table[name], OpenMode.ForRead);
      }
      catch
      {
        throw new KeyNotFoundException("No element with key " + name + " found");
      }
    }

    public override sealed IEnumerable<T> Items(IEnumerable<string> names)
    {
      var table = (SymbolTable)transaction.GetObject(containerID, OpenMode.ForRead);

      foreach (var name in names)
      {
        T item = default(T);

        try
        {
          item = (T)transaction.GetObject(table[name], OpenMode.ForRead);
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
      return Helpers.GetCount(transaction, containerID);
    }

    public override sealed long LongCount()
    {
      return Helpers.GetLongCount(transaction, containerID);
    }

    public ObjectId Add(T item)
    {
      return AddRange(new[] { item }).First();
    }

    public IEnumerable<ObjectId> AddRange(IEnumerable<T> items)
    {
      var table = (SymbolTable)transaction.GetObject(containerID, OpenMode.ForWrite);

      foreach (var item in items)
      {
        var id = table.Add(item);
       transaction.AddNewlyCreatedDBObject(item, true);
        yield return id;
      }
    }

    protected abstract T CreateNew();

    public T Create(string name)
    {
      var item = CreateNew();
      Add(item);
      item.Name = name;
      return item;
    }

    public IEnumerable<T> Create(IEnumerable<string> names)
    {
      var items = names.Select(n => CreateNew())
                      .ToArray();
      AddRange(items);
      return items.Zip(names, (i, n) =>
                              {
                                i.Name = n;
                                return i;
                              });
    }
  }
}
