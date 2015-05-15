using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2AcDb
{
  public static class TextStyleTableRecordExtensions
  {
    public static TextStyleTableRecord GetItem(this IEnumerable<TextStyleTableRecord> source, string name)
    {
      return SymbolTableHelpers.GetItem<TextStyleTableRecord, TextStyleTable>(source, tst => tst[name]);
    }

    public static bool Contains(this IEnumerable<TextStyleTableRecord> source, string name)
    {
      return SymbolTableHelpers.Contains<TextStyleTableRecord, TextStyleTable>(source, tst => tst.Has(name));
    }

    public static bool Contains(this IEnumerable<TextStyleTableRecord> source, ObjectId id)
    {
      return SymbolTableHelpers.Contains<TextStyleTableRecord, TextStyleTable>(source, tst => tst.Has(id));
    }

    public static ObjectId Add(this IEnumerable<TextStyleTableRecord> source, TextStyleTableRecord item)
    {
      return SymbolTableHelpers.Add<TextStyleTableRecord, TextStyleTable>(source, item);
    }

    public static IEnumerable<ObjectId> Add(this IEnumerable<TextStyleTableRecord> source, IEnumerable<TextStyleTableRecord> items)
    {
      return SymbolTableHelpers.AddRange<TextStyleTableRecord, TextStyleTable>(source, items);
    }
  }
}
