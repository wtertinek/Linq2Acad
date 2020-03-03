using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;

namespace Linq2Acad
{
  /// <summary>
  /// Represents a container that holds Entity objects.
  /// </summary>
  public sealed class EntityContainer : ContainerEnumerableBase<Entity>
  {
    /// <summary>
    /// Create a new instance of EntityContainer.
    /// </summary>
    /// <param name="database">The drawing database to use.</param>
    /// <param name="transaction">The transaction to use.</param>
    /// <param name="containerID">The ObjectId of the container.</param>
    public EntityContainer(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID, i => (ObjectId)i)
    {
    }

    /// <summary>
    /// The ObjectId of the container.
    /// </summary>
    public ObjectId ObjectId
    {
      get { return ID; }
    }

    /// <summary>
    /// Adds an Entity to the container.
    /// </summary>
    /// <param name="entity">The Entity to be added.</param>
    /// <exception cref="System.ArgumentNullException">Thrown when parameter  <i>entity</i> is null.</exception>
    /// <exception cref="System.Exception">Thrown when the given Entity belongs to another block or an AutoCAD error occurs.</exception>
    /// <returns>The ObjectId of the Entity that was added.</returns>
    public ObjectId Add(Entity entity)
    {
      if (entity == null) throw Error.ArgumentNull("entity");
      if (!entity.ObjectId.IsNull) throw Error.EntityBelongsToBlock();
      
      try
      {
        return AddInternal(new[] { entity }, false).First();
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e);
      }
    }

    /// <summary>
    /// Adds an Entity to the container.
    /// </summary>
    /// <param name="entity">The Entity to be added.</param>
    /// <param name="setDatabaseDefaults">True, if the database defaults should be set.</param>
    /// <exception cref="System.ArgumentNullException">Thrown when parameter  <i>entity</i> is null.</exception>
    /// <exception cref="System.Exception">Thrown when the given Entity belongs to another block or an AutoCAD error occurs.</exception>
    /// <returns>The ObjectId of the Entity that was added.</returns>
    public ObjectId Add(Entity entity, bool setDatabaseDefaults)
    {
      if (entity == null) throw Error.ArgumentNull("entity");
      if (!entity.ObjectId.IsNull) throw Error.EntityBelongsToBlock();

      try
      {
        return AddInternal(new[] { entity }, setDatabaseDefaults).First();
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e);
      }
    }

    /// <summary>
    /// Adds Entities to the container.
    /// </summary>
    /// <param name="entities">The Entities to be added.</param>
    /// <exception cref="System.ArgumentNullException">Thrown when parameter  <i>entities</i> is null.</exception>
    /// <exception cref="System.Exception">Thrown when the an Entity belongs to another block or an AutoCAD error occurs.</exception>
    /// <returns>The ObjectIds of the Entities that were added.</returns>
    public IEnumerable<ObjectId> Add(IEnumerable<Entity> entities)
    {
      if (entities == null) throw Error.ArgumentNull("entities");
      if (entities.Any(e => e == null)) throw Error.ElementNull("entities");
      var alreadyInBlock = entities.FirstOrDefault(e => !e.ObjectId.IsNull);
      if (alreadyInBlock != null) throw Error.EntityBelongsToBlock(alreadyInBlock.ObjectId);

      try
      {
        return AddInternal(entities, false);
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e);
      }
    }

    /// <summary>
    /// Adds Entities to the container.
    /// </summary>
    /// <param name="entities">The Entities to be added.</param>
    /// <param name="setDatabaseDefaults">True, if the database defaults should be set.</param>
    /// <exception cref="System.ArgumentNullException">Thrown when parameter  <i>entities</i> is null.</exception>
    /// <exception cref="System.Exception">Thrown when the an Entity belongs to another block or an AutoCAD error occurs.</exception>
    /// <returns>The ObjectIds of the Entities that were added.</returns>
    public IEnumerable<ObjectId> Add(IEnumerable<Entity> entities, bool setDatabaseDefaults)
    {
      if (entities == null) throw Error.ArgumentNull("entities");
      if (entities.Any(e => e == null)) throw Error.ElementNull("entities");
      var alreadyInBlock = entities.FirstOrDefault(e => !e.ObjectId.IsNull);
      if (alreadyInBlock != null) throw Error.EntityBelongsToBlock(alreadyInBlock.ObjectId);
      
      try
      {
        return AddInternal(entities, setDatabaseDefaults);
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e);
      }
    }

    /// <summary>
    /// Adds Entities to the container.
    /// </summary>
    /// <param name="items">The Entities to be added.</param>
    /// <param name="setDatabaseDefaults">True, if the database defaults should be set.</param>
    /// <returns>The ObjectIds of the Entities that were added.</returns>
    private IEnumerable<ObjectId> AddInternal(IEnumerable<Entity> items, bool setDatabaseDefaults)
    {
      var btr = (BlockTableRecord)transaction.GetObject(ID, OpenMode.ForWrite);
      return items.Select(i =>
                          {
                            if (setDatabaseDefaults)
                            {
                              i.SetDatabaseDefaults();
                            }

                            var id = btr.AppendEntity(i);
                            transaction.AddNewlyCreatedDBObject(i, true);
                            return id;
                          });
    }

    /// <summary>
    /// Removes all Entities from this container.
    /// </summary>
    /// <exception cref="System.Exception">Thrown when an AutoCAD error occurs.</exception>
    public void Clear()
    {
      try
      {
        this.UpgradeOpen()
             .ForEach(e => e.Erase());
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e);
      }
    }
  }
}
