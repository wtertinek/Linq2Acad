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
  public class BlockContainer : UniqueNameSymbolTableEnumerable<BlockTableRecord>
  {
    /// <summary>
    /// Creates a new instance of BlockContainer.
    /// </summary>
    /// <param name="database">The drawing database to use.</param>
    /// <param name="transaction">The transaction to use.</param>
    internal BlockContainer(Database database, Transaction transaction)
      : base(database, transaction, database.BlockTableId)
    {
    }

    /// <summary>
    /// Filters the initial set of ObjectIds.
    /// </summary>
    /// <param name="ids">The initial set of ObjectIds.</param>
    /// <param name="transaction">The transaction to use.</param>
    /// <returns>A filtered set of ObjectIds.</returns>
    private static IEnumerable<ObjectId> Filter(IEnumerable<ObjectId> ids, Transaction transaction)
    {
      foreach (var id in ids)
      {
        var btr = (BlockTableRecord)transaction.GetObject(id, OpenMode.ForRead);

        if (btr.Name != BlockTableRecord.ModelSpace &&
            !btr.IsFromExternalReference)
        {
          yield return btr.ObjectId;
        }
      }
    }

    /// <summary>
    /// Factory method that create a new element.
    /// </summary>
    /// <returns>A newly crated element of type BlockTableRecord.</returns>
    protected override BlockTableRecord CreateNew()
    {
      return new BlockTableRecord();
    }

    /// <summary>
    /// Converts each Block into an EntityContainer and allows querying for entities.
    /// </summary>
    /// <returns>The elements of the Block table as EntitiyContainers.</returns>
    public IEnumerable<EntityContainer> AsEntityContainers()
    {
      return this.Select(b => new EntityContainer(database, transaction, b.ObjectId));
    }

    /// <summary>
    /// Creates a new BlockTableRecord and adds the given Entites to it.
    /// </summary>
    /// <param name="name">The name of the new BlockTableRecord.</param>
    /// <param name="entities">The Entities that should be added to the BlockTableRecord.</param>
    /// <returns>A new instance of BlockTableRecord.</returns>
    /// <exception cref="System.ArgumentNullException">Thrown when parameters <i>name</i> or <i>entities</i> is null.</exception>
    public BlockTableRecord Create(string name, IEnumerable<Entity> entities)
    {
      if (name == null) throw Error.ArgumentNull("name");
      if (!Helpers.IsNameValid(name)) throw Error.InvalidName(name);
      if (Contains(name)) throw Error.ObjectExists<BlockTableRecord>(name);
      if (entities.Any(e => e == null)) throw Error.ElementNull("entities");
      var alreadyInBlock = entities.FirstOrDefault(e => !e.ObjectId.IsNull);
      if (alreadyInBlock != null) throw Error.EntityBelongsToBlock(alreadyInBlock.ObjectId);

      try
      {
        var block = CreateInternal(name);
        entities.ForEach(e => block.AppendEntity(e));

        return block;
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e);
      }
    }
  }

  /// <summary>
  /// A container class that provides access to the elements of the DimStyle table.
  /// </summary>
  public class DimStyleContainer : UniqueNameSymbolTableEnumerable<DimStyleTableRecord>
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

    /// <summary>
    /// Factory method that create a new element.
    /// </summary>
    /// <returns>A newly crated element of type DimStypeTableRecord.</returns>
    protected override DimStyleTableRecord CreateNew()
    {
      return new DimStyleTableRecord();
    }

    /// <summary>
    /// Sets the name of a newly created element.
    /// </summary>
    /// <param name="item">The newly created element.</param>
    /// <param name="name">The name of the element.</param>
    protected override void SetName(DimStyleTableRecord item, string name)
    {
      item.Name = name;
    }
  }

  /// <summary>
  /// A container class that provides access to the elements of the Layer table.
  /// </summary>
  public class LayerContainer : UniqueNameSymbolTableEnumerable<LayerTableRecord>
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
    /// Factory method that create a new element.
    /// </summary>
    /// <returns>A newly crated element of type LayerTableRecord.</returns>
    protected override LayerTableRecord CreateNew()
    {
      return new LayerTableRecord();
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
      if (name == null) throw Error.ArgumentNull("name");
      if (!Helpers.IsNameValid(name)) throw Error.InvalidName(name);
      if (Contains(name)) throw Error.ObjectExists<BlockTableRecord>(name);
      if (entities.Any(e => e == null)) throw Error.ElementNull("entities");

      try
      {
        var layer = CreateInternal(name);
        entities.ForEach(e => e.LayerId = layer.ObjectId);

        return layer;
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e);
      }
    }
  }

  /// <summary>
  /// A container class that provides access to the elements of the Linetype table.
  /// </summary>
  public class LinetypeContainer : UniqueNameSymbolTableEnumerable<LinetypeTableRecord>
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

    /// <summary>
    /// Factory method that create a new element.
    /// </summary>
    /// <returns>A newly crated element of type LinetypeTableRecord.</returns>
    protected override LinetypeTableRecord CreateNew()
    {
      return new LinetypeTableRecord();
    }
  }

  /// <summary>
  /// A container class that provides access to the elements of the RegApp table.
  /// </summary>
  public class RegAppContainer : UniqueNameSymbolTableEnumerable<RegAppTableRecord>
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

    /// <summary>
    /// Factory method that create a new element.
    /// </summary>
    /// <returns>A newly crated element of type RegAppTableRecord.</returns>
    protected override RegAppTableRecord CreateNew()
    {
      return new RegAppTableRecord();
    }
  }

  /// <summary>
  /// A container class that provides access to the elements of the TextStyle table.
  /// </summary>
  public class TextStyleContainer : UniqueNameSymbolTableEnumerable<TextStyleTableRecord>
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

    /// <summary>
    /// Factory method that create a new element.
    /// </summary>
    /// <returns>A newly crated element of type TextStyleTableRecord.</returns>
    protected override TextStyleTableRecord CreateNew()
    {
      return new TextStyleTableRecord();
    }
  }

  /// <summary>
  /// A container class that provides access to the elements of the Ucs table.
  /// </summary>
  public class UcsContainer : UniqueNameSymbolTableEnumerable<UcsTableRecord>
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

    /// <summary>
    /// Factory method that create a new element.
    /// </summary>
    /// <returns>A newly crated element of type UcsTableRecord.</returns>
    protected override UcsTableRecord CreateNew()
    {
      return new UcsTableRecord();
    }
  }

  /// <summary>
  /// A container class that provides access to the elements of the Viewport table.
  /// </summary>
  public class ViewportContainer : NonUniqueNameSymbolTableEnumerable<ViewportTableRecord>
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
    /// Factory method that create a new element.
    /// </summary>
    /// <returns>A newly crated element of type ViewportTableRecord.</returns>
    protected override ViewportTableRecord CreateNew()
    {
      return new ViewportTableRecord();
    }

    /// <summary>
    /// Returns the current Viewport or null, if there is no current Viewport.
    /// </summary>
    /// <exception cref="System.Exception">Thrown when an AutoCAD error occurs.</exception>
    public ViewportTableRecord Current
    {
      get
      {
        try
        {
          if (database.CurrentViewportTableRecordId.IsValid)
          {
            return (ViewportTableRecord)transaction.GetObject(database.CurrentViewportTableRecordId, OpenMode.ForRead);
          }
          else
          {
            return null;
          }
        }
        catch (Exception e)
        {
          throw Error.AutoCadException(e);
        }
      }
    }
  }

  /// <summary>
  /// A container class that provides access to the elements of the View table.
  /// </summary>
  public class ViewContainer : UniqueNameSymbolTableEnumerable<ViewTableRecord>
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
    /// Factory method that create a new element.
    /// </summary>
    /// <returns>A newly crated element of type ViewTableRecord.</returns>
    protected override ViewTableRecord CreateNew()
    {
      return new ViewTableRecord();
    }
  }
}
