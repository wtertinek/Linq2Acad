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
  /// A container class that provides access to the elements of the SectionViewStyle ditionary.
  /// </summary>
  public sealed class SectionViewStyleContainer : DBDictionaryEnumerable<SectionViewStyle>
  {
    internal SectionViewStyleContainer(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID)
    {
    }

    /// <summary>
    /// Creates a new SectionViewStyle element.
    /// </summary>
    /// <param name="name">The unique name of the SectionViewStyle element.</param>
    public SectionViewStyle Create(string name)
    {
      Require.IsValidSymbolName(name, nameof(name));
      Require.NameDoesNotExist<SectionViewStyle>(Contains(name), name);

      return AddInternal(new SectionViewStyle(), name);
    }

    /// <summary>
    /// Adds a newly created SectionViewStyle element.
    /// </summary>
    /// <param name="element">The SectionViewStyle to element add.</param>
    public void Add(SectionViewStyle element)
    {
      Require.ParameterNotNull(element, nameof(element));
      Require.IsValidSymbolName(element.Name, nameof(element.Name));
      Require.NameDoesNotExist<SectionViewStyle>(Contains(element.Name), element.Name);

      AddInternal(element, element.Name);
    }

    /// <summary>
    /// Adds a collection of newly created SectionViewStyle elements.
    /// </summary>
    /// <param name="elements">The SectionViewStyle elements to add.</param>
    public void AddRange(IEnumerable<SectionViewStyle> elements)
    {
      Require.ParameterNotNull(elements, nameof(elements));

      foreach (var element in elements)
      {
        Require.ParameterNotNull(element, nameof(element));
        Require.IsValidSymbolName(element.Name, nameof(element.Name));
        Require.NameDoesNotExist<SectionViewStyle>(Contains(element.Name), element.Name);
      }

      AddRangeInternal(elements.Select(i => Tuple.Create(i, i.Name)));
    }
  }
}
