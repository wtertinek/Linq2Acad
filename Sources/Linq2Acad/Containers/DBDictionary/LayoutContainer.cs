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
  /// A container class that provides access to the elements of the Layout ditionary.
  /// </summary>
  public sealed class LayoutContainer : DBDictionaryEnumerable<Layout>
  {
    internal LayoutContainer(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID)
    {
    }

    /// <summary>
    /// Creates a new Layout element.
    /// </summary>
    /// <param name="name">The unique name of the Layout element.</param>
    public Layout Create(string name)
    {
      Require.IsValidSymbolName(name, nameof(name));
      Require.NameDoesNotExist<Layout>(Contains(name), name);

      return AddInternal((Layout)transaction.GetObject(LayoutManager.Current.CreateLayout(name), OpenMode.ForWrite), name);
    }

    /// <summary>
    /// Adds a newly created Layout element.
    /// </summary>
    /// <param name="element">The Layout to element add.</param>
    public void Add(Layout element)
    {
      Require.ParameterNotNull(element, nameof(element));
      Require.IsValidSymbolName(element.LayoutName, nameof(element.LayoutName));
      Require.NameDoesNotExist<Layout>(Contains(element.LayoutName), element.LayoutName);

      AddRangeInternal(new[] { Tuple.Create(element, element.LayoutName) });
    }

    /// <summary>
    /// Adds a collection of newly created Layout elements.
    /// </summary>
    /// <param name="elements">The Layout elements to add.</param>
    public void AddRange(IEnumerable<Layout> elements)
    {
      Require.ParameterNotNull(elements, nameof(elements));

      foreach (var element in elements)
      {
        Require.ParameterNotNull(element, nameof(element));
        Require.IsValidSymbolName(element.LayoutName, nameof(element.LayoutName));
        Require.NameDoesNotExist<Layout>(Contains(element.LayoutName), element.LayoutName);
      }

      AddRangeInternal(elements.Select(i => Tuple.Create(i, i.LayoutName)));
    }
  }
}
