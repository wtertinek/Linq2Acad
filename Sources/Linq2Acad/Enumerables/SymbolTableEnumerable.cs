using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;

namespace Linq2Acad
{
  public abstract class SymbolTableEnumerable<T> : NameBasedContainerEnumerableBase<T> where T : SymbolTableRecord, new()
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

    protected T CreateInternal(string name)
    {
      var item = new T();
      AddRangeInternal(new[] { item });
      item.Name = name;
      return item;
    }

    protected IEnumerable<T> CreateInternal(IEnumerable<string> names)
    {
      var tmpNames = names.ToArray();
      var items = new T[tmpNames.Length];

      for (int i = 0; i < items.Length; i++)
      {
        items[i] = new T();
      }

      AddRangeInternal(items);

      for (int i = 0; i < items.Length; i++)
      {
        items[i].Name = tmpNames[i];
      }

      return items;
    }

    protected void AddRangeInternal(IEnumerable<T> items)
    {
      Require.ParameterNotNull(items, nameof(items));

      var table = (SymbolTable)transaction.GetObject(ID, OpenMode.ForWrite);

      foreach (var item in items)
      {
        table.Add(item);
        transaction.AddNewlyCreatedDBObject(item, true);
      }
    }

    protected override sealed bool ContainsInternal(ObjectId id)
      => ((SymbolTable)transaction.GetObject(ID, OpenMode.ForRead)).Has(id);

    protected override sealed bool ContainsInternal(string name)
      => ((SymbolTable)transaction.GetObject(ID, OpenMode.ForRead)).Has(name);
  }

  public abstract class UniqueNameSymbolTableEnumerableBase<T> : SymbolTableEnumerable<T> where T : SymbolTableRecord, new()
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

    public T Element(string name, bool openForWrite = false)
    {
      Require.StringNotEmpty(name, nameof(name));
      Require.NameExists<T>(Contains(name), name);

      return ElementInternal(name, openForWrite);
    }

    private T ElementInternal(string name, bool openForWrite)
    {
      var table = (SymbolTable)transaction.GetObject(ID, OpenMode.ForRead);
      return (T)transaction.GetObject(table[name], openForWrite ? OpenMode.ForWrite : OpenMode.ForRead);
    }

    public T ElementOrDefault(string name, bool openForWrite = false)
    {
      Require.StringNotEmpty(name, nameof(name));

      return ElementOrDefaultInternal(name, openForWrite);
    }

    private T ElementOrDefaultInternal(string name, bool openForWrite)
    {
      var table = (SymbolTable)transaction.GetObject(ID, OpenMode.ForRead);

      return table.Has(name)
               ? (T)transaction.GetObject(table[name], openForWrite ? OpenMode.ForWrite : OpenMode.ForRead)
               : null;
    }
  }

  public abstract class UniqueNameSymbolTableEnumerable<T> : UniqueNameSymbolTableEnumerableBase<T> where T : SymbolTableRecord, new()
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

    /// <summary>
    /// Creates a new element.
    /// </summary>
    /// <param name="name">The unique name of the element.</param>
    public T Create(string name)
    {
      Require.IsValidSymbolName(name, nameof(name));
      Require.NameDoesNotExists<T>(Contains(name), name);

      return CreateInternal(name);
    }

    /// <summary>
    /// Creates a colletion of new elements.
    /// </summary>
    /// <param name="names">The unique names of the new elements.</param>
    public IEnumerable<T> Create(IEnumerable<string> names)
    {
      Require.ParameterNotNull(names, nameof(names));
      Require.ElementsNotNull(names, nameof(names));
      var existingName = names.FirstOrDefault(Contains);
      Require.NameDoesNotExists<T>(Contains(existingName), existingName);

      return CreateInternal(names);
    }

    /// <summary>
    /// Adds a new element to the table.
    /// </summary>
    /// <param name="item">The element to add.</param>
    public void Add(T item)
    {
      Require.ParameterNotNull(item, nameof(item));
      Require.IsValidSymbolName(item.Name, nameof(item.Name));
      Require.NameDoesNotExists<T>(Contains(item.Name), item.Name);

      AddRangeInternal(new[] { item });
    }

    /// <summary>
    /// Adds new elements to the table.
    /// </summary>
    /// <param name="items">The elements to add.</param>
    public void AddRange(IEnumerable<T> items)
    {
      Require.ParameterNotNull(items, nameof(items));

      foreach (var item in items)
      {
        Require.ParameterNotNull(item, nameof(item));
        Require.IsValidSymbolName(item.Name, nameof(item.Name));
        Require.NameDoesNotExists<T>(Contains(item.Name), item.Name);
      }

      AddRangeInternal(items);
    }
  }

  public abstract class NonUniqueNameSymbolTableEnumerable<T> : SymbolTableEnumerable<T> where T : SymbolTableRecord, new()
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

    /// <summary>
    /// Creates a new element.
    /// </summary>
    /// <param name="name">The unique name of the element.</param>
    public T Create(string name)
    {
      Require.IsValidSymbolName(name, nameof(name));

      return CreateInternal(name);
    }

    /// <summary>
    /// Creates a colletion of new elements.
    /// </summary>
    /// <param name="names">The unique names of the new elements.</param>
    public IEnumerable<T> Create(IEnumerable<string> names)
    {
      Require.ElementsNotNull(names, nameof(names));

      return CreateInternal(names);
    }

    /// <summary>
    /// Adds a new element to the table.
    /// </summary>
    /// <param name="item">The element to add.</param>
    public void Add(T item)
    {
      Require.ParameterNotNull(item, nameof(item));
      Require.IsValidSymbolName(item.Name, nameof(item.Name));

      AddRangeInternal(new[] { item });
    }

    /// <summary>
    /// Adds new elements to the table.
    /// </summary>
    /// <param name="items">The elements to add.</param>
    public void AddRange(IEnumerable<T> items)
    {
      Require.ParameterNotNull(items, nameof(items));

      foreach (var item in items)
      {
        Require.ParameterNotNull(item, nameof(item));
        Require.IsValidSymbolName(item.Name, nameof(item.Name));
      }

      AddRangeInternal(items);
    }
  }
}
