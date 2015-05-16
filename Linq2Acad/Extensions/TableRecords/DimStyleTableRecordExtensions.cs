using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2Acad
{
  public static class DimStyleTableRecordExtensions
  {
    public static bool IsValidName(this IEnumerable<DimStyleTableRecord> source, string name, bool allowVerticalBar)
    {
      return SymbolTableHelpers.IsValidName(name, allowVerticalBar);
    }

    public static DimStyleTableRecord GetItem(this IEnumerable<DimStyleTableRecord> source, string name)
    {
      return SymbolTableHelpers.GetItem<DimStyleTableRecord, DimStyleTable>(source, dst => dst[name]);
    }

    public static bool Contains(this IEnumerable<DimStyleTableRecord> source, string name)
    {
      return SymbolTableHelpers.Contains<DimStyleTableRecord, DimStyleTable>(source, dst => dst.Has(name));
    }

    public static bool Contains(this IEnumerable<DimStyleTableRecord> source, ObjectId id)
    {
      return SymbolTableHelpers.Contains<DimStyleTableRecord, DimStyleTable>(source, dst => dst.Has(id));
    }

    public static ObjectId Add(this IEnumerable<DimStyleTableRecord> source, DimStyleTableRecord item)
    {
      return SymbolTableHelpers.Add<DimStyleTableRecord, DimStyleTable>(source, item);
    }

    public static IEnumerable<ObjectId> Add(this IEnumerable<DimStyleTableRecord> source, IEnumerable<DimStyleTableRecord> items)
    {
      return SymbolTableHelpers.AddRange<DimStyleTableRecord, DimStyleTable>(source, items);
    }
  }
}
