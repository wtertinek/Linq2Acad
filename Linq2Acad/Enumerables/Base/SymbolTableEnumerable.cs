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
      : base(database, transaction, containerID)
    {
    }

    protected override sealed ObjectId GetObjectID(object iteratorItem)
    {
      return (ObjectId)iteratorItem;
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
        throw new KeyNotFoundException("No element with key " + name + " found");
      }
    }

    public ObjectId Add(T item)
    {
      if (!AcadDatabase.IsNameValid(item.Name))
      {
        throw Error.InvalidName(item.Name);
      }

      return AddRange(new[] { item }).First();
    }

    public IEnumerable<ObjectId> AddRange(IEnumerable<T> items)
    {
      var table = (SymbolTable)transaction.GetObject(ID, OpenMode.ForWrite);

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
      if (!AcadDatabase.IsNameValid(name))
      {
        throw Error.InvalidName(name);
      }

      var item = CreateNew();
      Add(item);
      item.Name = name;
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
