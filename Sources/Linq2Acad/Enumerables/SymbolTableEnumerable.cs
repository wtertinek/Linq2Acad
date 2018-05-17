using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;

namespace Linq2Acad
{
  public abstract class SymbolTableEnumerable<T> : NameBasedContainerEnumerableBase<T> where T : SymbolTableRecord
  {
    protected SymbolTableEnumerable(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID, i => (ObjectId)i)
    {
    }

    protected SymbolTableEnumerable(Database database, Transaction transaction, ObjectId containerID,
                                    Func<IEnumerable<ObjectId>, IEnumerable<ObjectId>> filter)
      : base(database, transaction, containerID, i => (ObjectId)i, filter)
    {
    }

    /// <summary>
    /// Sets the name of a newly created element.
    /// </summary>
    /// <param name="item">The newly created element.</param>
    /// <param name="name">The name of the element.</param>
    protected override void SetName(T item, string name)
    {
      item.Name = name;
    }

    protected override sealed bool ContainsInternal(ObjectId id)
    {
      return ((SymbolTable)transaction.GetObject(ID, OpenMode.ForRead)).Has(id);
    }

    protected override sealed bool ContainsInternal(string name)
    {
      return ((SymbolTable)transaction.GetObject(ID, OpenMode.ForRead)).Has(name);
    }

    protected IEnumerable<T> CreateInternal(IEnumerable<string> names)
    {
      var tmpNames = names.ToArray();
      var items = new T[tmpNames.Length];

      for (int i = 0; i < items.Length; i++)
      {
        items[i] = CreateNew();
      }

      AddRangeInternal(items, names);

      for (int i = 0; i < items.Length; i++)
      {
        SetName(items[i], tmpNames[i]);
      }

      return items;
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

  public abstract class UniqueNameSymbolTableEnumerableBase<T> : SymbolTableEnumerable<T> where T : SymbolTableRecord
  {
    protected UniqueNameSymbolTableEnumerableBase(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID)
    {
    }

    protected UniqueNameSymbolTableEnumerableBase(Database database, Transaction transaction, ObjectId containerID,
                                                  Func<IEnumerable<ObjectId>, IEnumerable<ObjectId>> filter)
      : base(database, transaction, containerID, filter)
    {
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
      var table = (SymbolTable)transaction.GetObject(ID, OpenMode.ForRead);
      return (T)transaction.GetObject(table[name], forWrite ? OpenMode.ForWrite : OpenMode.ForRead);
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
  }

  public abstract class UniqueNameSymbolTableEnumerable<T> : UniqueNameSymbolTableEnumerableBase<T> where T : SymbolTableRecord
  {
    protected UniqueNameSymbolTableEnumerable(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID)
    {
    }

    protected UniqueNameSymbolTableEnumerable(Database database, Transaction transaction, ObjectId containerID,
                                              Func<IEnumerable<ObjectId>, IEnumerable<ObjectId>> filter)
      : base(database, transaction, containerID, filter)
    {
    }

    public T Create(string name)
    {
      if (name == null) throw Error.ArgumentNull("name");
      if (!Helpers.IsNameValid(name)) throw Error.InvalidName(name);
      if (Contains(name)) throw Error.ObjectExists<T>(name);

      try
      {
        return CreateInternal(name);
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e);
      }
    }

    public IEnumerable<T> Create(IEnumerable<string> names)
    {
      if (names == null) throw Error.ArgumentNull("names");
      var invalidName = names.FirstOrDefault(n => !Helpers.IsNameValid(n));
      if (invalidName != null) throw Error.InvalidName(invalidName);
      var existingName = names.FirstOrDefault(n => Contains(n));
      if (existingName != null) throw Error.ObjectExists<T>(existingName);

      try
      {
        return CreateInternal(names);
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e);
      }
    }

    public void Add(T item)
    {
      if (item == null) throw Error.ArgumentNull("item");
      if (!Helpers.IsNameValid(item.Name)) throw Error.InvalidName(item.Name);
      if (Contains(item.Name)) throw Error.ObjectExists<T>(item.Name);

      AddRangeInternal(new[] { item }, new[] { item.Name });
    }

    public void AddRange(IEnumerable<T> items)
    {
      if (items == null) throw Error.ArgumentNull("items");

      foreach (var item in items)
      {
        if (item == null) throw Error.ArgumentNull("item");
        if (!Helpers.IsNameValid(item.Name)) throw Error.InvalidName(item.Name);
        if (Contains(item.Name)) throw Error.ObjectExists<T>(item.Name);
      }

      AddRangeInternal(items, items.Select(i => i.Name));
    }

  }

  public abstract class NonUniqueNameSymbolTableEnumerable<T> : SymbolTableEnumerable<T> where T : SymbolTableRecord
  {
    protected NonUniqueNameSymbolTableEnumerable(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID)
    {
    }

    protected NonUniqueNameSymbolTableEnumerable(Database database, Transaction transaction, ObjectId containerID,
                                                 Func<IEnumerable<ObjectId>, IEnumerable<ObjectId>> filter)
      : base(database, transaction, containerID, filter)
    {
    }

    public T Create(string name)
    {
      if (name == null) throw Error.ArgumentNull("name");
      if (!Helpers.IsNameValid(name)) throw Error.InvalidName(name);

      try
      {
        return CreateInternal(name);
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e);
      }
    }

    public IEnumerable<T> Create(IEnumerable<string> names)
    {
      if (names == null) throw Error.ArgumentNull("names");
      var invalidName = names.FirstOrDefault(n => !Helpers.IsNameValid(n));
      if (invalidName != null) throw Error.InvalidName(invalidName);

      try
      {
        return CreateInternal(names);
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e);
      }
    }

    public void Add(T item)
    {
      if (item == null) throw Error.ArgumentNull("item");
      if (!Helpers.IsNameValid(item.Name)) throw Error.InvalidName(item.Name);

      AddRangeInternal(new[] { item }, new[] { item.Name });
    }

    public void AddRange(IEnumerable<T> items)
    {
      if (items == null) throw Error.ArgumentNull("items");

      foreach (var item in items)
      {
        if (item == null) throw Error.ArgumentNull("item");
        if (!Helpers.IsNameValid(item.Name)) throw Error.InvalidName(item.Name);
      }

      AddRangeInternal(items, items.Select(i => i.Name));
    }
  }
}
