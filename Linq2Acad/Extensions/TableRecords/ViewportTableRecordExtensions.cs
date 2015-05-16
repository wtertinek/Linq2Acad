using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2Acad
{
  public static class ViewportTableRecordExtensions
  {
    public static bool IsValidName(this IEnumerable<ViewportTableRecord> source, string name, bool allowVerticalBar)
    {
      return SymbolTableHelpers.IsValidName(name, allowVerticalBar);
    }

    public static ViewportTableRecord GetItem(this IEnumerable<ViewportTableRecord> source, string name)
    {
      return SymbolTableHelpers.GetItem<ViewportTableRecord, ViewportTable>(source, vt => vt[name]);
    }

    public static bool Contains(this IEnumerable<ViewportTableRecord> source, string name)
    {
      return SymbolTableHelpers.Contains<ViewportTableRecord, ViewportTable>(source, vt => vt.Has(name));
    }

    public static bool Contains(this IEnumerable<ViewportTableRecord> source, ObjectId id)
    {
      return SymbolTableHelpers.Contains<ViewportTableRecord, ViewportTable>(source, vt => vt.Has(id));
    }

    public static ObjectId Add(this IEnumerable<ViewportTableRecord> source, ViewportTableRecord item)
    {
      return SymbolTableHelpers.Add<ViewportTableRecord, ViewportTable>(source, item);
    }

    public static IEnumerable<ObjectId> Add(this IEnumerable<ViewportTableRecord> source, IEnumerable<ViewportTableRecord> items)
    {
      return SymbolTableHelpers.AddRange<ViewportTableRecord, ViewportTable>(source, items);
    }
  }
}
