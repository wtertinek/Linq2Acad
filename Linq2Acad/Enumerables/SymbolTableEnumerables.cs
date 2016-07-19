using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Linq2Acad
{
  public class BlockContainer : SymbolTableEnumerable<BlockTableRecord>
  {
    internal BlockContainer(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID)
    {
    }

    public IEnumerable<EntityContainer> AsEntityContainers()
    {
      return this.Select(b => new EntityContainer(database, transaction, b.ObjectId));
    }

    protected override BlockTableRecord CreateNew()
    {
      return new BlockTableRecord();
    }

    protected override void SetName(BlockTableRecord item, string name)
    {
      item.Name = name;
    }

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

        entities.ToList()
                .ForEach(e => block.AppendEntity(e));

        return block;
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e);
      }
    }
  }

  public class DimStyleContainer : SymbolTableEnumerable<DimStyleTableRecord>
  {
    internal DimStyleContainer(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID)
    {
    }

    protected override DimStyleTableRecord CreateNew()
    {
      return new DimStyleTableRecord();
    }

    protected override void SetName(DimStyleTableRecord item, string name)
    {
      item.Name = name;
    }
  }

  public class LayerContainer : SymbolTableEnumerable<LayerTableRecord>
  {
    internal LayerContainer(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID)
    {
    }

    protected override LayerTableRecord CreateNew()
    {
      return new LayerTableRecord();
    }

    protected override void SetName(LayerTableRecord item, string name)
    {
      item.Name = name;
    }

    public LayerTableRecord Create(string name, IEnumerable<Entity> entities)
    {
      if (name == null) throw Error.ArgumentNull("name");
      if (!Helpers.IsNameValid(name)) throw Error.InvalidName(name);
      if (Contains(name)) throw Error.ObjectExists<BlockTableRecord>(name);
      if (entities.Any(e => e == null)) throw Error.ElementNull("entities");

      try
      {
        var layer = CreateInternal(name);

        entities.ToList()
                .ForEach(e => e.LayerId = layer.ObjectId);

        return layer;
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e);
      }
    }
  }

  public class LinetypeContainer : SymbolTableEnumerable<LinetypeTableRecord>
  {
    internal LinetypeContainer(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID)
    {
    }

    protected override LinetypeTableRecord CreateNew()
    {
      return new LinetypeTableRecord();
    }

    protected override void SetName(LinetypeTableRecord item, string name)
    {
      item.Name = name;
    }
  }

  public class RegAppContainer : SymbolTableEnumerable<RegAppTableRecord>
  {
    internal RegAppContainer(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID)
    {
    }

    protected override RegAppTableRecord CreateNew()
    {
      return new RegAppTableRecord();
    }

    protected override void SetName(RegAppTableRecord item, string name)
    {
      item.Name = name;
    }
  }

  public class TextStyleContainer : SymbolTableEnumerable<TextStyleTableRecord>
  {
    internal TextStyleContainer(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID)
    {
    }

    protected override TextStyleTableRecord CreateNew()
    {
      return new TextStyleTableRecord();
    }

    protected override void SetName(TextStyleTableRecord item, string name)
    {
      item.Name = name;
    }
  }

  public class UcsContainer : SymbolTableEnumerable<UcsTableRecord>
  {
    internal UcsContainer(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID)
    {
    }

    protected override UcsTableRecord CreateNew()
    {
      return new UcsTableRecord();
    }

    protected override void SetName(UcsTableRecord item, string name)
    {
      item.Name = name;
    }
  }

  public class ViewportContainer : SymbolTableEnumerable<ViewportTableRecord>
  {
    internal ViewportContainer(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID)
    {
    }
    
    public ViewportTableRecord Current
    {
      get { return (ViewportTableRecord)transaction.GetObject(database.CurrentViewportTableRecordId, OpenMode.ForRead); }
    }

    protected override ViewportTableRecord CreateNew()
    {
      return new ViewportTableRecord();
    }

    protected override void SetName(ViewportTableRecord item, string name)
    {
      item.Name = name;
    }
  }

  public class ViewContainer : DBDictionaryEnumerable<ViewTableRecord>
  {
    internal ViewContainer(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID)
    {
    }

    protected override ViewTableRecord CreateNew()
    {
      return new ViewTableRecord();
    }

    protected override void SetName(ViewTableRecord item, string name)
    {
      item.Name = name;
    }
  }
}
