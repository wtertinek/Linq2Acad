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

    protected override BlockTableRecord CreateNew(string name)
    {
      return new BlockTableRecord() { Name = name };
    }
  }

  public class DimStyleContainer : SymbolTableEnumerable<DimStyleTableRecord>
  {
    internal DimStyleContainer(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID)
    {
    }

    protected override DimStyleTableRecord CreateNew(string name)
    {
      return new DimStyleTableRecord() { Name = name };
    }
  }

  public class LayerContainer : SymbolTableEnumerable<LayerTableRecord>
  {
    internal LayerContainer(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID)
    {
    }

    protected override LayerTableRecord CreateNew(string name)
    {
      return new LayerTableRecord() { Name = name };
    }
  }

  public class LinetypeContainer : SymbolTableEnumerable<LinetypeTableRecord>
  {
    internal LinetypeContainer(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID)
    {
    }

    protected override LinetypeTableRecord CreateNew(string name)
    {
      return new LinetypeTableRecord() { Name = name };
    }
  }

  public class RegAppContainer : SymbolTableEnumerable<RegAppTableRecord>
  {
    internal RegAppContainer(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID)
    {
    }

    protected override RegAppTableRecord CreateNew(string name)
    {
      return new RegAppTableRecord() { Name = name };
    }
  }

  public class TextStyleContainer : SymbolTableEnumerable<TextStyleTableRecord>
  {
    internal TextStyleContainer(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID)
    {
    }

    protected override TextStyleTableRecord CreateNew(string name)
    {
      return new TextStyleTableRecord() { Name = name };
    }
  }

  public class UcsContainer : SymbolTableEnumerable<UcsTableRecord>
  {
    internal UcsContainer(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID)
    {
    }

    protected override UcsTableRecord CreateNew(string name)
    {
      return new UcsTableRecord() { Name = name };
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

    protected override ViewportTableRecord CreateNew(string name)
    {
      return new ViewportTableRecord() { Name = name };
    }
  }

  public class ViewContainer : DBDictionaryEnumerable<ViewTableRecord>
  {
    internal ViewContainer(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID)
    {
    }

    protected override ViewTableRecord CreateNew(string name)
    {
      return new ViewTableRecord() { Name = name };
    }
  }
}
