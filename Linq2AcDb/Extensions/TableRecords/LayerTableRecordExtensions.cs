using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2AcDb
{
  public static class LayerTableRecordExtensions
  {
    public static bool IsValidName(this IEnumerable<LayerTableRecord> source, string name, bool allowVerticalBar)
    {
      return SymbolTableHelpers.IsValidName(name, allowVerticalBar);
    }

    public static LayerTableRecord GetItem(this IEnumerable<LayerTableRecord> source, string name)
    {
      return SymbolTableHelpers.GetItem<LayerTableRecord, LayerTable>(source, lt => lt[name]);
    }

    public static bool Contains(this IEnumerable<LayerTableRecord> source, string name)
    {
      return SymbolTableHelpers.Contains<LayerTableRecord, LayerTable>(source, lt => lt.Has(name));
    }

    public static bool Contains(this IEnumerable<LayerTableRecord> source, ObjectId id)
    {
      return SymbolTableHelpers.Contains<LayerTableRecord, LayerTable>(source, lt => lt.Has(id));
    }

    public static ObjectId Add(this IEnumerable<LayerTableRecord> source, LayerTableRecord item)
    {
      return SymbolTableHelpers.Add<LayerTableRecord, LayerTable>(source, item);
    }

    public static IEnumerable<ObjectId> Add(this IEnumerable<LayerTableRecord> source, IEnumerable<LayerTableRecord> items)
    {
      return SymbolTableHelpers.AddRange<LayerTableRecord, LayerTable>(source, items);
    }
  }
}
