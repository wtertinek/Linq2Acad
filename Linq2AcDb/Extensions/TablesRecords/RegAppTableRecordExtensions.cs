using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2AcDb
{
  public static class RegAppTableRecordExtensions
  {
    public static RegAppTableRecord GetItem(this IEnumerable<RegAppTableRecord> source, string name)
    {
      return SymbolTableHelpers.GetItem<RegAppTableRecord, RegAppTable>(source, rat => rat[name]);
    }

    public static bool Contains(this IEnumerable<RegAppTableRecord> source, string name)
    {
      return SymbolTableHelpers.Contains<RegAppTableRecord, RegAppTable>(source, rat => rat.Has(name));
    }

    public static bool Contains(this IEnumerable<RegAppTableRecord> source, ObjectId id)
    {
      return SymbolTableHelpers.Contains<RegAppTableRecord, RegAppTable>(source, rat => rat.Has(id));
    }

    public static ObjectId Add(this IEnumerable<RegAppTableRecord> source, RegAppTableRecord item)
    {
      return SymbolTableHelpers.Add<RegAppTableRecord, RegAppTable>(source, item);
    }

    public static IEnumerable<ObjectId> Add(this IEnumerable<RegAppTableRecord> source, IEnumerable<RegAppTableRecord> items)
    {
      return SymbolTableHelpers.Add<RegAppTableRecord, RegAppTable>(source, items);
    }
  }
}
