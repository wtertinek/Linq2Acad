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
    /// <param name="item">The DetailViewStyle element to add.</param>
    public void Add(DetailViewStyle item)
    {
      Require.ParameterNotNull(item, nameof(item));
      Require.IsValidSymbolName(item.Name, nameof(item.Name));
      Require.NameDoesNotExist<DetailViewStyle>(Contains(item.Name), item.Name);

      AddInternal(item, item.Name);
    }

    /// <summary>
    /// Adds a collection of newly created DetailViewStyle elements.
    /// </summary>
    /// <param name="items">The DetailViewStyle elements to add.</param>
    public void AddRange(IEnumerable<DetailViewStyle> items)
    {
      Require.ParameterNotNull(items, nameof(items));

      foreach (var item in items)
      {
        Require.ParameterNotNull(item, nameof(item));
        Require.IsValidSymbolName(item.Name, nameof(item.Name));
        Require.NameDoesNotExist<DetailViewStyle>(Contains(item.Name), item.Name);
      }

      AddRangeInternal(items.Select(i => Tuple.Create(i, i.Name)));
    }
  }
}
