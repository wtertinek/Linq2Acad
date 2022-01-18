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

    protected override void SetName(T item, string name)
    {
      Require.ParameterNotNull(item, nameof(item));

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
      Require.ParameterNotNull(items, nameof(items));
      Require.ParameterNotNull(names, nameof(names));

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
      Require.StringNotEmpty(name, nameof(name));
      Require.NameExists<T>(Contains(name), name);

      return ElementInternal(name, false);
    }

    public T Element(string name, bool forWrite)
    {
      Require.StringNotEmpty(name, nameof(name));
      Require.NameExists<T>(Contains(name), name);

      return ElementInternal(name, forWrite);
    }

    private T ElementInternal(string name, bool forWrite)
    {
      var table = (SymbolTable)transaction.GetObject(ID, OpenMode.ForRead);
      return (T)transaction.GetObject(table[name], forWrite ? OpenMode.ForWrite : OpenMode.ForRead);
    }

    public T ElementOrDefault(string name)
    {
      Require.StringNotEmpty(name, nameof(name));

      return ElementOrDefaultInternal(name, false);
    }

    public T ElementOrDefault(string name, bool forWrite)
    {
      Require.StringNotEmpty(name, nameof(name));

      return ElementOrDefaultInternal(name, forWrite);
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

      AddRangeInternal(new[] { item }, new[] { item.Name });
    }

    /// <summary>
    /// Adds new elemenets to the table.
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

      AddRangeInternal(new[] { item }, new[] { item.Name });
    }

    /// <summary>
    /// Adds new elemenets to the table.
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

      AddRangeInternal(items, items.Select(i => i.Name));
    }
  }
}
