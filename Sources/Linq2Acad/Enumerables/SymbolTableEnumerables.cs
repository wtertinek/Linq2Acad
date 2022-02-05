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
  /// A container class that provides access to the elements of the Block table.
  /// </summary>
  public sealed class BlockContainer : UniqueNameSymbolTableEnumerable<BlockTableRecord>
  {
    /// <summary>
    /// Creates a new instance of BlockContainer.
    /// </summary>
    /// <param name="database">The drawing database to use.</param>
    /// <param name="transaction">The transaction to use.</param>
    internal BlockContainer(Database database, Transaction transaction)
      : base(database, transaction, database.BlockTableId, ids => Filter(ids, transaction))
    {
    }

    /// <summary>
    /// Filters the initial set of ObjectIds. We ignore the model space, all paper space layouts and all XRefs.
    /// </summary>
    /// <param name="ids">The initial set of ObjectIds.</param>
    /// <param name="transaction">The transaction to use.</param>
    /// <returns>A filtered set of ObjectIds.</returns>
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
    /// <param name="id">The id of the object.</param>
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
    /// Creates a new BlockTableRecord and adds the given Entites to it.
    /// </summary>
    /// <param name="name">The name of the new BlockTableRecord.</param>
    /// <param name="entities">The Entities that should be added to the BlockTableRecord.</param>
    /// <returns>A new instance of BlockTableRecord.</returns>
    /// <exception cref="System.ArgumentNullException">Thrown when parameters <i>name</i> or <i>entities</i> is null.</exception>
    public BlockTableRecord Create(string name, IEnumerable<Entity> entities)
    {
      Require.IsValidSymbolName(name, nameof(name));
      Require.NameDoesNotExists<BlockTableRecord>(Contains(name), name);
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
    /// Create a new block and imports all model space entities from the given drawing file to it.
    /// </summary>
    /// <param name="newBlockName">The name of the new BlockTableRecord.</param>
    /// <param name="fileName">The name of the drawing file that should be imported.</param>
    /// <returns>A new instance of BlockTableRecord.</returns>
    /// <exception cref="System.ArgumentNullException">Thrown when parameters <i>newBlockName</i> or <i>fileName</i> is null.</exception>
    public BlockTableRecord Import(string newBlockName, string fileName)
    {
      Require.ParameterNotNull(newBlockName, nameof(newBlockName));
      Require.NameDoesNotExists<BlockTableRecord>(Contains(newBlockName), newBlockName);
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

  /// <summary>
  /// A container class that provides access to the elements of the DimStyle table.
  /// </summary>
  public sealed class DimStyleContainer : UniqueNameSymbolTableEnumerable<DimStyleTableRecord>
  {
    /// <summary>
    /// Creates a new instance of DimStyleContainer.
    /// </summary>
    /// <param name="database">The drawing database to use.</param>
    /// <param name="transaction">The transaction to use.</param>
    internal DimStyleContainer(Database database, Transaction transaction)
      : base(database, transaction, database.DimStyleTableId)
    {
    }
  }

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
    /// Creates a new LayerTableRecord and adds the given Entites to it.
    /// </summary>
    /// <param name="name">The name of the new LayerTableRecord.</param>
    /// <param name="entities">The Entities that should be added to the new LayerTableRecord.</param>
    /// <returns>A new instance of LayerTableRecord.</returns>
    /// <exception cref="System.ArgumentNullException">Thrown when parameters <i>name</i> or <i>entities</i> is null.</exception>
    public LayerTableRecord Create(string name, IEnumerable<Entity> entities)
    {
      Require.IsValidSymbolName(name, nameof(name));
      Require.NameDoesNotExists<BlockTableRecord>(Contains(name), name);
      Require.ElementsNotNull(entities, nameof(entities));

      var layer = CreateInternal(name);

      foreach (var entity in entities.UpgradeOpen())
      {
         entity.LayerId = layer.ObjectId;
      }

      return layer;
    }
  }

  /// <summary>
  /// A container class that provides access to the elements of the Linetype table.
  /// </summary>
  public sealed class LinetypeContainer : UniqueNameSymbolTableEnumerable<LinetypeTableRecord>
  {
    /// <summary>
    /// Creates a new instance of LinetypeContainer.
    /// </summary>
    /// <param name="database">The drawing database to use.</param>
    /// <param name="transaction">The transaction to use.</param>
    internal LinetypeContainer(Database database, Transaction transaction)
      : base(database, transaction, database.LinetypeTableId)
    {
    }
  }

  /// <summary>
  /// A container class that provides access to the elements of the RegApp table.
  /// </summary>
  public sealed class RegAppContainer : UniqueNameSymbolTableEnumerable<RegAppTableRecord>
  {
    /// <summary>
    /// Creates a new instance of RegAppContainer.
    /// </summary>
    /// <param name="database">The drawing database to use.</param>
    /// <param name="transaction">The transaction to use.</param>
    internal RegAppContainer(Database database, Transaction transaction)
      : base(database, transaction, database.RegAppTableId)
    {
    }
  }

  /// <summary>
  /// A container class that provides access to the elements of the TextStyle table.
  /// </summary>
  public sealed class TextStyleContainer : UniqueNameSymbolTableEnumerable<TextStyleTableRecord>
  {
    /// <summary>
    /// Creates a new instance of TextStyleContainer.
    /// </summary>
    /// <param name="database">The drawing database to use.</param>
    /// <param name="transaction">The transaction to use.</param>
    internal TextStyleContainer(Database database, Transaction transaction)
      : base(database, transaction, database.TextStyleTableId)
    {
    }
  }

  /// <summary>
  /// A container class that provides access to the elements of the Ucs table.
  /// </summary>
  public sealed class UcsContainer : UniqueNameSymbolTableEnumerable<UcsTableRecord>
  {
    /// <summary>
    /// Creates a new instance of UcsContainer.
    /// </summary>
    /// <param name="database">The drawing database to use.</param>
    /// <param name="transaction">The transaction to use.</param>
    internal UcsContainer(Database database, Transaction transaction)
      : base(database, transaction, database.UcsTableId)
    {
    }
  }

  /// <summary>
  /// A container class that provides access to the elements of the Viewport table.
  /// </summary>
  public sealed class ViewportContainer : NonUniqueNameSymbolTableEnumerable<ViewportTableRecord>
  {
    /// <summary>
    /// Creates a new instance of ViewportContainer.
    /// </summary>
    /// <param name="database">The drawing database to use.</param>
    /// <param name="transaction">The transaction to use.</param>
    internal ViewportContainer(Database database, Transaction transaction)
      : base(database, transaction, database.ViewportTableId)
    {
    }

    /// <summary>
    /// Returns the current Viewport or null, if there is no current Viewport.
    /// </summary>
    /// <exception cref="System.Exception">Thrown when an AutoCAD error occurs.</exception>
    public ViewportTableRecord Current
      => database.CurrentViewportTableRecordId.IsValid
           ? (ViewportTableRecord)transaction.GetObject(database.CurrentViewportTableRecordId, OpenMode.ForRead)
           : null;
  }

  /// <summary>
  /// A container class that provides access to the elements of the View table.
  /// </summary>
  public sealed class ViewContainer : UniqueNameSymbolTableEnumerable<ViewTableRecord>
  {
    /// <summary>
    /// Creates a new instance of ViewContainer.
    /// </summary>
    /// <param name="database">The drawing database to use.</param>
    /// <param name="transaction">The transaction to use.</param>
    internal ViewContainer(Database database, Transaction transaction)
      : base(database, transaction, database.ViewTableId)
    {
    }

  /// <summary>
  /// A container class that provides access to the XRef elements.
  /// </summary>
  internal class XRefBlockContainer : UniqueNameSymbolTableEnumerableBase<BlockTableRecord>
  {
    /// <summary>
    /// Creates a new instance of XRefContainer.
    /// </summary>
    /// <param name="database">The drawing database to use.</param>
    /// <param name="transaction">The transaction to use.</param>
    internal XRefBlockContainer(Database database, Transaction transaction)
      : base(database, transaction, database.BlockTableId, ids => Filter(ids, transaction))
    {
    }

    /// <summary>
    /// Filters the initial set of ObjectIds. Here we only take XRefs.
    /// </summary>
    /// <param name="ids">The initial set of ObjectIds.</param>
    /// <param name="transaction">The transaction to use.</param>
    /// <returns>A filtered set of ObjectIds.</returns>
    private static IEnumerable<ObjectId> Filter(IEnumerable<ObjectId> ids, Transaction transaction)
    {
      foreach (var id in ids)
      {
        var btr = (BlockTableRecord)transaction.GetObject(id, OpenMode.ForRead);

        if (btr.IsFromExternalReference)
        {
          yield return btr.ObjectId;
        }
      }
    }

    protected override BlockTableRecord CreateNew()
    {
      throw new NotImplementedException();
    }
  }
}
