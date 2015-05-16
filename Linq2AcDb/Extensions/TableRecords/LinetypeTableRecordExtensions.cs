using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2AcDb
{
  public static class LinetypeTableRecordExtensions
  {
    public static bool IsValidName(this IEnumerable<LinetypeTableRecord> source, string name, bool allowVerticalBar)
    {
      return SymbolTableHelpers.IsValidName(name, allowVerticalBar);
    }

    public static LinetypeTableRecord GetItem(this IEnumerable<LinetypeTableRecord> source, string name)
    {
      return SymbolTableHelpers.GetItem<LinetypeTableRecord, LinetypeTable>(source, lt => lt[name]);
    }

    public static bool Contains(this IEnumerable<LinetypeTableRecord> source, string name)
    {
      return SymbolTableHelpers.Contains<LinetypeTableRecord, LinetypeTable>(source, lt => lt.Has(name));
    }

    public static bool Contains(this IEnumerable<LinetypeTableRecord> source, ObjectId id)
    {
      return SymbolTableHelpers.Contains<LinetypeTableRecord, LinetypeTable>(source, lt => lt.Has(id));
    }

    public static ObjectId Add(this IEnumerable<LinetypeTableRecord> source, LinetypeTableRecord item)
    {
      return SymbolTableHelpers.Add<LinetypeTableRecord, LinetypeTable>(source, item);
    }

    public static IEnumerable<ObjectId> Add(this IEnumerable<LinetypeTableRecord> source, IEnumerable<LinetypeTableRecord> items)
    {
      return SymbolTableHelpers.AddRange<LinetypeTableRecord, LinetypeTable>(source, items);
    }
  }
}
