using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2AcDb
{
  public static class BlockTableRecordExtensions
  {
    public static bool IsValidName(this IEnumerable<BlockTableRecord> source, string name, bool allowVerticalBar)
    {
      return SymbolTableHelpers.IsValidName(name, allowVerticalBar);
    }

    public static BlockTableRecord GetItem(this IEnumerable<BlockTableRecord> source, string name)
    {
      return SymbolTableHelpers.GetItem<BlockTableRecord, BlockTable>(source, bt => bt[name]);
    }

    public static bool Contains(this IEnumerable<BlockTableRecord> source, string name)
    {
      return SymbolTableHelpers.Contains<BlockTableRecord, BlockTable>(source, bt => bt.Has(name));
    }

    public static bool Contains(this IEnumerable<BlockTableRecord> source, ObjectId id)
    {
      return SymbolTableHelpers.Contains<BlockTableRecord, BlockTable>(source, bt => bt.Has(id));
    }

    public static ObjectId Add(this IEnumerable<BlockTableRecord> source, BlockTableRecord item)
    {
      return SymbolTableHelpers.Add<BlockTableRecord, BlockTable>(source, item);
    }

    public static IEnumerable<ObjectId> Add(this IEnumerable<BlockTableRecord> source, IEnumerable<BlockTableRecord> items)
    {
      return SymbolTableHelpers.AddRange<BlockTableRecord, BlockTable>(source, items);
    }
  }
}
