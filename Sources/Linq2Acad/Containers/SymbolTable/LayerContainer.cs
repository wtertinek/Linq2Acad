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
  /// A container class that provides access to the elements of the Layer table.
  /// </summary>
  public sealed class LayerContainer : UniqueNameSymbolTableEnumerable<LayerTableRecord>
  {
    /// <summary>
    /// Creates a new instance of LayerContainer.
    /// </summary>
    /// <param name="database">The drawing database to use.</param>
    /// <param name="transaction">The transaction to use.</param>
    internal LayerContainer(Database database, Transaction transaction)
      : base(database, transaction, database.LayerTableId)
    {
    }

    /// <summary>
    /// Creates a new LayerTableRecord and adds the given Entities to it.
    /// </summary>
    /// <param name="name">The name of the new LayerTableRecord.</param>
    /// <param name="entities">The Entities that should be added to the new LayerTableRecord.</param>
    /// <returns>A new instance of LayerTableRecord.</returns>
    /// <exception cref="System.ArgumentNullException">Thrown when parameters <i>name</i> or <i>entities</i> is null.</exception>
    public LayerTableRecord Create(string name, IEnumerable<Entity> entities)
    {
      Require.IsValidSymbolName(name, nameof(name));
      Require.NameDoesNotExist<BlockTableRecord>(Contains(name), name);
      Require.ElementsNotNull(entities, nameof(entities));

      var layer = CreateInternal(name);

      foreach (var entity in entities.UpgradeOpen())
      {
        entity.LayerId = layer.ObjectId;
      }

      return layer;
    }
  }
}
