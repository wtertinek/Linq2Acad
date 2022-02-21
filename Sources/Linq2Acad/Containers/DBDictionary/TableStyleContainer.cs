using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Linq2Acad
{
  /// <summary>
  /// A container class that provides access to the elements of the TableStyle ditionary.
  /// </summary>
  public sealed class TableStyleContainer : DBDictionaryEnumerable<TableStyle>
  {
    internal TableStyleContainer(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID)
    {
    }

    /// <summary>
    /// Creates a new TableStyle element.
    /// </summary>
    /// <param name="name">The unique name of the TableStyle element.</param>
    public TableStyle Create(string name)
    {
      Require.IsValidSymbolName(name, nameof(name));
      Require.NameDoesNotExist<TableStyle>(Contains(name), name);

      return AddInternal(new TableStyle(), name);
    }

    /// <summary>
    /// Adds a newly created TableStyle element.
    /// </summary>
    /// <param name="element">The TableStyle to element add.</param>
    public void Add(TableStyle element)
    {
      Require.ParameterNotNull(element, nameof(element));
      Require.IsValidSymbolName(element.Name, nameof(element.Name));
      Require.NameDoesNotExist<TableStyle>(Contains(element.Name), element.Name);

      AddInternal(element, element.Name);
    }

    /// <summary>
    /// Adds a collection of newly created TableStyle elements.
    /// </summary>
    /// <param name="elements">The TableStyle elements to add.</param>
    public void AddRange(IEnumerable<TableStyle> elements)
    {
      Require.ParameterNotNull(elements, nameof(elements));

      foreach (var element in elements)
      {
        Require.ParameterNotNull(element, nameof(element));
        Require.IsValidSymbolName(element.Name, nameof(element.Name));
        Require.NameDoesNotExist<TableStyle>(Contains(element.Name), element.Name);
      }

      AddRangeInternal(elements.Select(i => Tuple.Create(i, i.Name)));
    }
  }
}
