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

    public override T Element(string name, bool forWrite)
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
      var table = (SymbolTable)transaction.GetObject(ID, OpenMode.ForRead);
      return (T)transaction.GetObject(table[name], forWrite ? OpenMode.ForWrite : OpenMode.ForRead);
    }

    public override sealed T ElementOrDefault(string name)
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

    public override T ElementOrDefault(string name, bool forWrite)
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
      var table = (SymbolTable)transaction.GetObject(ID, OpenMode.ForRead);

      if (table.Has(name))
      {
        return (T)transaction.GetObject(table[name], forWrite ? OpenMode.ForWrite : OpenMode.ForRead);
      }
      else
      {
        return null;
      }
    }

    public void Add(T item)
    {
      if (item == null) throw Error.ArgumentNull("item");
      if (!Helpers.IsNameValid(item.Name)) throw Error.InvalidName(item.Name);
      if (Contains(item.Name)) throw Error.Generic("An object with name " + item.Name + " already exists");

      AddRangeInternal(new[] { item }, new [] { item.Name });
    }

    public void AddRange(IEnumerable<T> items)
    {
      if (items == null) throw Error.ArgumentNull("items");

      foreach (var item in items)
      {
        if (item == null) throw Error.ArgumentNull("item");
        if (!Helpers.IsNameValid(item.Name)) throw Error.InvalidName(item.Name);
        if (Contains(item.Name)) throw Error.Generic("An object with name " + item.Name + " already exists");
      }

      AddRangeInternal(items, items.Select(i => i.Name));
    }

    protected override sealed void AddRangeInternal(IEnumerable<T> items, IEnumerable<string> names)
    {
      var table = (SymbolTable)transaction.GetObject(ID, OpenMode.ForWrite);

      foreach (var item in items)
      {
        table.Add(item);
        transaction.AddNewlyCreatedDBObject(item, true);
      }
    }
  }
}
