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
    /// <param name="item">The SectionViewStyle to element add.</param>
    public void Add(SectionViewStyle item)
    {
      Require.ParameterNotNull(item, nameof(item));
      Require.IsValidSymbolName(item.Name, nameof(item.Name));
      Require.NameDoesNotExist<SectionViewStyle>(Contains(item.Name), item.Name);

      AddInternal(item, item.Name);
    }

    /// <summary>
    /// Adds a collection of newly created SectionViewStyle elements.
    /// </summary>
    /// <param name="items">The SectionViewStyle elements to add.</param>
    public void AddRange(IEnumerable<SectionViewStyle> items)
    {
      Require.ParameterNotNull(items, nameof(items));

      foreach (var item in items)
      {
        Require.ParameterNotNull(item, nameof(item));
        Require.IsValidSymbolName(item.Name, nameof(item.Name));
        Require.NameDoesNotExist<SectionViewStyle>(Contains(item.Name), item.Name);
      }

      AddRangeInternal(items.Select(i => (i, i.Name)));
    }
  }
}
