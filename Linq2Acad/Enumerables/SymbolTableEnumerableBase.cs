using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;

namespace Linq2Acad
{
  public abstract class SymbolTableEnumerableBase<T> : NameBasedEnumerable<T> where T : SymbolTableRecord
  {
    protected SymbolTableEnumerableBase(Lazy<Transaction> transaction, ObjectId containerID)
      : base(transaction, containerID)
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
      var table = (SymbolTable)L2ADatabase.Transaction.Value.GetObject(ContainerID, OpenMode.ForRead);
      return table.Has(name);
    }

    public override sealed bool Contains(ObjectId id)
    {
      var table = (SymbolTable)L2ADatabase.Transaction.Value.GetObject(ContainerID, OpenMode.ForRead);
      return table.Has(id);
    }

    public override sealed T Item(string name)
    {
      Helpers.CheckTransaction();
      var table = (SymbolTable)L2ADatabase.Transaction.Value.GetObject(ContainerID, OpenMode.ForRead);
      return (T)L2ADatabase.Transaction.Value.GetObject(table[name], OpenMode.ForRead);
    }

    public override sealed IEnumerable<T> Items(IEnumerable<string> names)
    {
      Helpers.CheckTransaction();

      var table = (SymbolTable)L2ADatabase.Transaction.Value.GetObject(ContainerID, OpenMode.ForRead);

      foreach (var name in names)
      {
        yield return (T)L2ADatabase.Transaction.Value.GetObject(table[name], OpenMode.ForRead);
      }
    }

    public override int Count()
    {
      var enumerator = ((IEnumerable)transaction.Value.GetObject(ContainerID, OpenMode.ForRead)).GetEnumerator();

      var count = 0;

      while (enumerator.MoveNext())
      {
        count++;
      }

      return count;
    }

    public ObjectId Add(T item)
    {
      return AddRange(new[] { item }).First();
    }

    public IEnumerable<ObjectId> AddRange(IEnumerable<T> items)
    {
      Helpers.CheckTransaction();

      var table = (SymbolTable)L2ADatabase.Transaction.Value.GetObject(ContainerID, OpenMode.ForWrite);

      foreach (var item in items)
      {
        var id = table.Add(item);
        L2ADatabase.Transaction.Value.AddNewlyCreatedDBObject(item, true);
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
