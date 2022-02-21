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
      var element = new T();
      AddRangeInternal(new[] { element });
      element.Name = name;
      return element;
    }

    protected IEnumerable<T> CreateInternal(IEnumerable<string> names)
    {
      var tmpNames = names.ToArray();
      var elements = new T[tmpNames.Length];

      for (int i = 0; i < elements.Length; i++)
      {
        elements[i] = new T();
      }

      AddRangeInternal(elements);

      for (int i = 0; i < elements.Length; i++)
      {
        elements[i].Name = tmpNames[i];
      }

      return elements;
    }

    protected void AddRangeInternal(IEnumerable<T> elements)
    {
      Require.ParameterNotNull(elements, nameof(elements));

      var table = (SymbolTable)transaction.GetObject(ID, OpenMode.ForWrite);

      foreach (var element in elements)
      {
        table.Add(element);
        transaction.AddNewlyCreatedDBObject(element, true);
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

    /// <summary>
    /// Returns the element with the specified name.
    /// </summary>
    /// <param name="name">The name of the element.</param>
    /// <param name="openForWrite">True, if the object should be opened for-write. By default the object is opened readonly.</param>
    /// <returns>The element with the specified name.</returns>
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

    /// <summary>
    /// Returns the element with the specified name or a default value if the element cannot be found.
    /// </summary>
    /// <param name="name">The name of the element.</param>
    /// <param name="openForWrite">True, if the object should be opened for-write. By default the object is opened readonly.</param>
    /// <returns>The element with the specified name.</returns>
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
      Require.NameDoesNotExist<T>(Contains(name), name);

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
      Require.NameDoesNotExist<T>(Contains(existingName), existingName);

      return CreateInternal(names);
    }

    /// <summary>
    /// Adds a new element to the table.
    /// </summary>
    /// <param name="element">The element to add.</param>
    public void Add(T element)
    {
      Require.ParameterNotNull(element, nameof(element));
      Require.IsValidSymbolName(element.Name, nameof(element.Name));
      Require.NameDoesNotExist<T>(Contains(element.Name), element.Name);

      AddRangeInternal(new[] { element });
    }

    /// <summary>
    /// Adds new elements to the table.
    /// </summary>
    /// <param name="elements">The elements to add.</param>
    public void AddRange(IEnumerable<T> elements)
    {
      Require.ParameterNotNull(elements, nameof(elements));

      foreach (var element in elements)
      {
        Require.ParameterNotNull(element, nameof(element));
        Require.IsValidSymbolName(element.Name, nameof(element.Name));
        Require.NameDoesNotExist<T>(Contains(element.Name), element.Name);
      }

      AddRangeInternal(elements);
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
    /// <param name="element">The element to add.</param>
    public void Add(T element)
    {
      Require.ParameterNotNull(element, nameof(element));
      Require.IsValidSymbolName(element.Name, nameof(element.Name));

      AddRangeInternal(new[] { element });
    }

    /// <summary>
    /// Adds new elements to the table.
    /// </summary>
    /// <param name="elements">The elements to add.</param>
    public void AddRange(IEnumerable<T> elements)
    {
      Require.ParameterNotNull(elements, nameof(elements));

      foreach (var element in elements)
      {
        Require.ParameterNotNull(element, nameof(element));
        Require.IsValidSymbolName(element.Name, nameof(element.Name));
      }

      AddRangeInternal(elements);
    }
  }
}
