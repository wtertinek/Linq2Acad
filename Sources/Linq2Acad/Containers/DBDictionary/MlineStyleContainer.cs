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
  /// A container class that provides access to the elements of the MLineStyle ditionary.
  /// </summary>
  public sealed class MlineStyleContainer : DBDictionaryEnumerable<MlineStyle>
  {
    internal MlineStyleContainer(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID)
    {
    }

    /// <summary>
    /// Creates a new MlineStyle element.
    /// </summary>
    /// <param name="name">The unique name of the MlineStyle element.</param>
    public MlineStyle Create(string name)
    {
      Require.IsValidSymbolName(name, nameof(name));
      Require.NameDoesNotExist<MlineStyle>(Contains(name), name);

      return AddInternal(new MlineStyle(), name);
    }

    /// <summary>
    /// Adds a newly created MlineStyle element.
    /// </summary>
    /// <param name="element">The MlineStyle to element add.</param>
    public void Add(MlineStyle element)
    {
      Require.ParameterNotNull(element, nameof(element));
      Require.IsValidSymbolName(element.Name, nameof(element.Name));
      Require.NameDoesNotExist<MlineStyle>(Contains(element.Name), element.Name);

      AddInternal(element, element.Name);
    }

    /// <summary>
    /// Adds a collection of newly created MlineStyle elements.
    /// </summary>
    /// <param name="elements">The MlineStyle elements to add.</param>
    public void AddRange(IEnumerable<MlineStyle> elements)
    {
      Require.ParameterNotNull(elements, nameof(elements));

      foreach (var element in elements)
      {
        Require.ParameterNotNull(element, nameof(element));
        Require.IsValidSymbolName(element.Name, nameof(element.Name));
        Require.NameDoesNotExist<MlineStyle>(Contains(element.Name), element.Name);
      }

      AddRangeInternal(elements.Select(i => Tuple.Create(i, i.Name)));
    }
  }
}
