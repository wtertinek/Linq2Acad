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

    public IEnumerable<EntityContainer> AsEntityContainer()
    {
      return this.Select(b => new EntityContainer(database, transaction, b.ObjectId));
    }

    protected override BlockTableRecord CreateNew()
    {
      return new BlockTableRecord();
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
  }

  public class EntityContainer : ContainerEnumerableBase<Entity>
  {
    public EntityContainer(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID, i => (ObjectId)i)
    {
    }

    public ObjectId ObjectId
    {
      get { return ID; }
    }

    public ObjectId Add(Entity item)
    {
      return Add(item, false);
    }

    public ObjectId Add(Entity item, bool setDatabaseDefaults)
    {
      return Add(new[] { item }, setDatabaseDefaults).First();
    }

    public IEnumerable<ObjectId> Add(IEnumerable<Entity> items)
    {
      return Add(items, false);
    }

    public IEnumerable<ObjectId> Add(IEnumerable<Entity> items, bool setDatabaseDefaults)
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
                   }).ToArray();
    }

    public void Clear()
    {
      this.ForEach(e => e.Erase());
    }
  }
}
