using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;

namespace Linq2Acad
{
  public abstract class SymbolTableEnumerable<T> : NameBasedEnumerableBase<T> where T : SymbolTableRecord
  {
    protected SymbolTableEnumerable(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID, i => (ObjectId)i)
    {
    }

    public override sealed bool Contains(ObjectId id)
    {
      return ((SymbolTable)transaction.GetObject(ID, OpenMode.ForRead)).Has(id);
    }

    public override sealed bool Contains(string name)
    {
      return ((SymbolTable)transaction.GetObject(ID, OpenMode.ForRead)).Has(name);
    }

    public override sealed T Element(string name)
    {
      var table = (SymbolTable)transaction.GetObject(ID, OpenMode.ForRead);

      try
      {
        return (T)transaction.GetObject(table[name], OpenMode.ForRead);
      }
      catch
      {
        throw Error.KeyNotFound("No element with key " + name + " found");
      }
    }

    public void Add(T item)
    {
      if (!AcadDatabase.IsNameValid(item.Name))
      {
        throw Error.InvalidName(item.Name);
      }

      AddRange(new[] { item });
    }

    public void AddRange(IEnumerable<T> items)
    {
      var table = (SymbolTable)transaction.GetObject(ID, OpenMode.ForWrite);

      foreach (var item in items)
      {
        table.Add(item);
        transaction.AddNewlyCreatedDBObject(item, true);
      }
    }

    protected abstract T CreateNew();

    public T Create(string name)
    {
      if (!AcadDatabase.IsNameValid(name))
      {
        throw Error.InvalidName(name);
      }

      var item = CreateNew();
      item.Name = name;
      Add(item);
      return item;
    }

    public IEnumerable<T> Create(IEnumerable<string> names)
    {
      var invalidName = names.FirstOrDefault(n => !AcadDatabase.IsNameValid(n));

      if (invalidName != null)
      {
        throw Error.InvalidName(invalidName);
      }

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
