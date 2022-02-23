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
  /// A container class that provides access to the elements of the Block table. In addition to the standard LINQ operations this class provides methods to create, add and import BlockTableRecords.
  /// </summary>
  public sealed class BlockContainer : UniqueNameSymbolTableEnumerable<BlockTableRecord>
  {
    internal BlockContainer(Database database, Transaction transaction)
      : base(database, transaction, database.BlockTableId, ids => Filter(ids, transaction))
    {
    }

    private static IEnumerable<ObjectId> Filter(IEnumerable<ObjectId> ids, Transaction transaction)
    {
      foreach (var id in ids)
      {
        var btr = (BlockTableRecord)transaction.GetObject(id, OpenMode.ForRead);

        if (!btr.Name.Equals(BlockTableRecord.ModelSpace, StringComparison.InvariantCultureIgnoreCase) &&
            !btr.Name.StartsWith(BlockTableRecord.PaperSpace, StringComparison.InvariantCultureIgnoreCase) &&
            !btr.IsFromExternalReference)
        {
          yield return btr.ObjectId;
        }
      }
    }

    /// <summary>
    /// Converts the Block with the given ObjectId into an EntityContainer that allows querying for entities.
    /// </summary>
    /// <param name="id">The ID of the object.</param>
    /// <returns></returns>
    public EntityContainer ElementAsEntityContainer(ObjectId id)
      => new EntityContainer(database, transaction, id);

    /// <summary>
    /// Converts each Block into an EntityContainer that allows querying for entities.
    /// </summary>
    /// <returns>The elements of the Block table as EntitiyContainers.</returns>
    public IEnumerable<EntityContainer> AsEntityContainers()
      => this.Select(b => new EntityContainer(database, transaction, b.ObjectId));

    /// <summary>
    /// Creates a new BlockTableRecord with the specified name and adds the Entities to it.
    /// </summary>
    /// <param name="name">The name of the new BlockTableRecord.</param>
    /// <param name="entities">The Entities that should be added to the BlockTableRecord.</param>
    /// <returns>A new instance of BlockTableRecord.</returns>
    /// <exception cref="System.ArgumentNullException">Thrown when parameters <i>name</i> or <i>entities</i> is null.</exception>
    public BlockTableRecord Create(string name, IEnumerable<Entity> entities)
    {
      Require.IsValidSymbolName(name, nameof(name));
      Require.NameDoesNotExist<BlockTableRecord>(Contains(name), name);
      Require.ElementsNotNull(entities, nameof(entities));

      // TODO: What about this check?
      //var alreadyInBlock = entities.FirstOrDefault(e => !e.ObjectId.IsNull);
      //if (alreadyInBlock != null) throw Error.WrongOrigin(alreadyInBlock.ObjectId);

      var block = CreateInternal(name);

      foreach (var entity in entities.UpgradeOpen())
      {
        block.AppendEntity(entity);
      }

      return block;
    }

    /// <summary>
    /// Creates a new block and imports all model space entities from the given drawing file to it.
    /// </summary>
    /// <param name="newBlockName">The name of the new BlockTableRecord.</param>
    /// <param name="fileName">The name of the drawing file that should be imported.</param>
    /// <returns>A new instance of BlockTableRecord.</returns>
    /// <exception cref="System.ArgumentNullException">Thrown when parameters <i>newBlockName</i> or <i>fileName</i> is null.</exception>
    public BlockTableRecord Import(string newBlockName, string fileName)
    {
      Require.ParameterNotNull(newBlockName, nameof(newBlockName));
      Require.NameDoesNotExist<BlockTableRecord>(Contains(newBlockName), newBlockName);
      Require.ParameterNotNull(fileName, nameof(fileName));
      Require.FileExists(fileName, nameof(fileName));

      var blockId = ObjectId.Null;

      using (var db = AcadDatabase.OpenReadOnly(fileName))
      {
        blockId = database.Insert(newBlockName, db.Database, true);
      }

      return (BlockTableRecord)transaction.GetObject(blockId, OpenMode.ForRead);
    }
  }
}