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
  /// A container class that provides access to the elements of the Material ditionary.
  /// </summary>
  public sealed class MaterialContainer : DBDictionaryEnumerable<Material>
  {
    internal MaterialContainer(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID)
    {
    }

    /// <summary>
    /// Creates a new Material element.
    /// </summary>
    /// <param name="name">The unique name of the Material element.</param>
    public Material Create(string name)
    {
      Require.IsValidSymbolName(name, nameof(name));
      Require.NameDoesNotExist<Material>(Contains(name), name);

      return AddInternal(new Material(), name);
    }

    /// <summary>
    /// Adds a newly created Material element.
    /// </summary>
    /// <param name="element">The Material to element add.</param>
    public void Add(Material element)
    {
      Require.ParameterNotNull(element, nameof(element));
      Require.IsValidSymbolName(element.Name, nameof(element.Name));
      Require.NameDoesNotExist<Material>(Contains(element.Name), element.Name);

      AddInternal(element, element.Name);
    }

    /// <summary>
    /// Adds a collection of newly created Material elements.
    /// </summary>
    /// <param name="elements">The Material elements to add.</param>
    public void AddRange(IEnumerable<Material> elements)
    {
      Require.ParameterNotNull(elements, nameof(elements));

      foreach (var element in elements)
      {
        Require.ParameterNotNull(element, nameof(element));
        Require.IsValidSymbolName(element.Name, nameof(element.Name));
        Require.NameDoesNotExist<Material>(Contains(element.Name), element.Name);
      }

      AddRangeInternal(elements.Select(i => Tuple.Create(i, i.Name)));
    }
  }
}
