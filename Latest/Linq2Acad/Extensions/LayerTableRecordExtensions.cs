using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2Acad
{
  /// <summary>
  /// Extension methods for instances of LayerTableRecord.
  /// </summary>
  public static class LayerTableRecordExtensions
  {
    /// <summary>
    /// Adds the given entity to this layer.
    /// </summary>
    /// <param name="layer">The layer instance.</param>
    /// <param name="entity">The entity to add.</param>
    /// <exception cref="System.ArgumentNullException">Thrown when parameter <i>entity</i> is null.</exception>
    /// <exception cref="System.Exception">Thrown when adding the entity throws an exception.</exception>
    public static void Add(this LayerTableRecord layer, Entity entity)
    {
      if (entity == null) throw Error.ArgumentNull("entity");

      try
      {
        AddInternal(layer, entity);
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e);
      }
    }

    /// <summary>
    /// Adds the given entities to this layer.
    /// </summary>
    /// <param name="layer">The layer instance.</param>
    /// <param name="entities">The entities to add.</param>
    /// <exception cref="System.ArgumentNullException">Thrown when parameter <i>entities</i> is null.</exception>
    /// <exception cref="System.Exception">Thrown when adding an entity throws an exception.</exception>
    public static void AddRange(this LayerTableRecord layer, IEnumerable<Entity> entities)
    {
      if (entities == null) throw Error.ArgumentNull("entities");

      foreach (var entity in entities)
      {
        try
        {
          AddInternal(layer, entity);
        }
        catch (Exception e)
        {
          throw Error.AutoCadException(e);
        }
      }
    }

    /// <summary>
    /// Adds the given entity to this layer.
    /// </summary>
    /// <param name="layer">The layer instance.</param>
    /// <param name="entity">The entity to add.</param>
    private static void AddInternal(LayerTableRecord layer, Entity entity)
    {
      Helpers.WriteWrap(entity, () => entity.LayerId = layer.ObjectId);
    }
  }
}
