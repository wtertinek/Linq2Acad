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
  /// A container class that provides access to the elements of the DetailViewStyle ditionary.
  /// </summary>
  public sealed class DetailViewStyleContainer : DBDictionaryEnumerable<DetailViewStyle>
  {
    internal DetailViewStyleContainer(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID)
    {
    }

    /// <summary>
    /// Creates a new DetailViewStyle element.
    /// </summary>
    /// <param name="name">The unique name of the DetailViewStyle element.</param>
    public DetailViewStyle Create(string name)
    {
      Require.IsValidSymbolName(name, nameof(name));
      Require.NameDoesNotExist<DetailViewStyle>(Contains(name), name);

      return AddInternal(new DetailViewStyle(), name);
    }

    /// <summary>
    /// Adds a newly created DetailViewStyle element.
    /// </summary>
    /// <param name="element">The DetailViewStyle element to add.</param>
    public void Add(DetailViewStyle element)
    {
      Require.ParameterNotNull(element, nameof(element));
      Require.IsValidSymbolName(element.Name, nameof(element.Name));
      Require.NameDoesNotExist<DetailViewStyle>(Contains(element.Name), element.Name);

      AddInternal(element, element.Name);
    }

    /// <summary>
    /// Adds a collection of newly created DetailViewStyle elements.
    /// </summary>
    /// <param name="elements">The DetailViewStyle elements to add.</param>
    public void AddRange(IEnumerable<DetailViewStyle> elements)
    {
      Require.ParameterNotNull(elements, nameof(elements));

      foreach (var element in elements)
      {
        Require.ParameterNotNull(element, nameof(element));
        Require.IsValidSymbolName(element.Name, nameof(element.Name));
        Require.NameDoesNotExist<DetailViewStyle>(Contains(element.Name), element.Name);
      }

      AddRangeInternal(elements.Select(i => Tuple.Create(i, i.Name)));
    }
  }
}
