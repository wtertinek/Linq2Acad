using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2Acad
{
  public class Blocks : SymbolTableEnumerable<BlockTableRecord>
  {
    internal Blocks(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID)
    {
    }

    protected override BlockTableRecord CreateNew()
    {
      return new BlockTableRecord();
    }
  }

  public class DimStyles : SymbolTableEnumerable<DimStyleTableRecord>
  {
    internal DimStyles(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID)
    {
    }

    protected override DimStyleTableRecord CreateNew()
    {
      return new DimStyleTableRecord();
    }
  }

  public class Layers : SymbolTableEnumerable<LayerTableRecord>
  {
    internal Layers(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID)
    {
    }

    protected override LayerTableRecord CreateNew()
    {
      return new LayerTableRecord();
    }
  }

  public class Linetypes : SymbolTableEnumerable<LinetypeTableRecord>
  {
    internal Linetypes(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID)
    {
    }

    protected override LinetypeTableRecord CreateNew()
    {
      return new LinetypeTableRecord();
    }
  }

  public class RegApps : SymbolTableEnumerable<RegAppTableRecord>
  {
    internal RegApps(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID)
    {
    }

    protected override RegAppTableRecord CreateNew()
    {
      return new RegAppTableRecord();
    }
  }

  public class TextStyles : SymbolTableEnumerable<TextStyleTableRecord>
  {
    internal TextStyles(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID)
    {
    }

    protected override TextStyleTableRecord CreateNew()
    {
      return new TextStyleTableRecord();
    }
  }

  public class Ucss : SymbolTableEnumerable<UcsTableRecord>
  {
    internal Ucss(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID)
    {
    }

    protected override UcsTableRecord CreateNew()
    {
      return new UcsTableRecord();
    }
  }

  public class Viewports : SymbolTableEnumerable<ViewportTableRecord>
  {
    internal Viewports(Database database, Transaction transaction, ObjectId containerID)
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

  public class Views : DBDictionaryEnumerable<ViewTableRecord>
  {
    internal Views(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID)
    {
    }

    protected override ViewTableRecord CreateNew()
    {
      return new ViewTableRecord();
    }
  }
}
