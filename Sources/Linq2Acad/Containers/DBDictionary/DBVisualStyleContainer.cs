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
  /// A container class that provides access to the elements of the DBVisualStyle ditionary.
  /// </summary>
  public sealed class DBVisualStyleContainer : DBDictionaryEnumerable<DBVisualStyle>
  {
    internal DBVisualStyleContainer(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID)
    {
    }

    /// <summary>
    /// Creates a new DBVisualStyle element.
    /// </summary>
    /// <param name="name">The unique name of the DBVisualStyle element.</param>
    public DBVisualStyle Create(string name)
    {
      Require.IsValidSymbolName(name, nameof(name));
      Require.NameDoesNotExist<DBVisualStyle>(Contains(name), name);

      return AddInternal(new DBVisualStyle(), name);
    }

    /// <summary>
    /// Adds a newly created DBVisualStyle element.
    /// </summary>
    /// <param name="item">The DBVisualStyle element to add.</param>
    public void Add(DBVisualStyle item)
    {
      Require.ParameterNotNull(item, nameof(item));
      Require.IsValidSymbolName(item.Name, nameof(item.Name));
      Require.NameDoesNotExist<DBVisualStyle>(Contains(item.Name), item.Name);

      AddInternal(item, item.Name);
    }

    /// <summary>
    /// Adds a collection of newly created DBVisualStyle elements.
    /// </summary>
    /// <param name="items">The DBVisualStyle elements to add.</param>
    public void AddRange(IEnumerable<DBVisualStyle> items)
    {
      Require.ParameterNotNull(items, nameof(items));

      foreach (var item in items)
      {
        Require.ParameterNotNull(item, nameof(item));
        Require.IsValidSymbolName(item.Name, nameof(item.Name));
        Require.NameDoesNotExist<DBVisualStyle>(Contains(item.Name), item.Name);
      }

      AddRangeInternal(items.Select(i => Tuple.Create(i, i.Name)));
    }
  }
}
