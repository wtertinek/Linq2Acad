using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2Acad
{
  public class L2ABlockTable : SymbolTableEnumerableBase<BlockTableRecord>
  {
    internal L2ABlockTable(Lazy<Transaction> transaction, ObjectId containerID)
      : base(transaction, containerID)
    {
    }

    protected override BlockTableRecord CreateNew()
    {
      return new BlockTableRecord();
    }
  }

  public class L2ADimStyleTable : SymbolTableEnumerableBase<DimStyleTableRecord>
  {
    internal L2ADimStyleTable(Lazy<Transaction> transaction, ObjectId containerID)
      : base(transaction, containerID)
    {
    }

    protected override DimStyleTableRecord CreateNew()
    {
      return new DimStyleTableRecord();
    }
  }

  public class L2ALayerTable : SymbolTableEnumerableBase<LayerTableRecord>
  {
    internal L2ALayerTable(Lazy<Transaction> transaction, ObjectId containerID)
      : base(transaction, containerID)
    {
    }

    protected override LayerTableRecord CreateNew()
    {
      return new LayerTableRecord();
    }
  }

  public class L2ALinetypeTable : SymbolTableEnumerableBase<LinetypeTableRecord>
  {
    internal L2ALinetypeTable(Lazy<Transaction> transaction, ObjectId containerID)
      : base(transaction, containerID)
    {
    }

    protected override LinetypeTableRecord CreateNew()
    {
      return new LinetypeTableRecord();
    }
  }

  public class L2ARegAppTable : SymbolTableEnumerableBase<RegAppTableRecord>
  {
    internal L2ARegAppTable(Lazy<Transaction> transaction, ObjectId containerID)
      : base(transaction, containerID)
    {
    }

    protected override RegAppTableRecord CreateNew()
    {
      return new RegAppTableRecord();
    }
  }

  public class L2ATextStyleTable : SymbolTableEnumerableBase<TextStyleTableRecord>
  {
    internal L2ATextStyleTable(Lazy<Transaction> transaction, ObjectId containerID)
      : base(transaction, containerID)
    {
    }

    protected override TextStyleTableRecord CreateNew()
    {
      return new TextStyleTableRecord();
    }
  }

  public class L2AUcsTable : SymbolTableEnumerableBase<UcsTableRecord>
  {
    internal L2AUcsTable(Lazy<Transaction> transaction, ObjectId containerID)
      : base(transaction, containerID)
    {
    }

    protected override UcsTableRecord CreateNew()
    {
      return new UcsTableRecord();
    }
  }

  public class L2AViewportTable : SymbolTableEnumerableBase<ViewportTableRecord>
  {
    internal L2AViewportTable(Lazy<Transaction> transaction, ObjectId containerID)
      : base(transaction, containerID)
    {
    }
    
    public ViewportTableRecord Current
    {
      get { return (ViewportTableRecord)transaction.Value.GetObject(L2ADatabase.Database.CurrentViewportTableRecordId, OpenMode.ForRead); }
    }

    protected override ViewportTableRecord CreateNew()
    {
      return new ViewportTableRecord();
    }
  }

  public class L2AViewTable : DBDictionaryEnumerableBase<ViewTableRecord>
  {
    internal L2AViewTable(Lazy<Transaction> transaction, ObjectId containerID)
      : base(transaction, containerID)
    {
    }

    protected override ViewTableRecord CreateNew()
    {
      return new ViewTableRecord();
    }
  }
}
