using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2Acad
{
  public static class UcsTableRecordExtensions
  {
    public static bool IsValidName(this IEnumerable<UcsTableRecord> source, string name, bool allowVerticalBar)
    {
      return SymbolTableHelpers.IsValidName(name, allowVerticalBar);
    }

    public static UcsTableRecord GetItem(this IEnumerable<UcsTableRecord> source, string name)
    {
      return SymbolTableHelpers.GetItem<UcsTableRecord, UcsTable>(source, ut => ut[name]);
    }

    public static bool Contains(this IEnumerable<UcsTableRecord> source, string name)
    {
      return SymbolTableHelpers.Contains<UcsTableRecord, UcsTable>(source, ut => ut.Has(name));
    }

    public static bool Contains(this IEnumerable<UcsTableRecord> source, ObjectId id)
    {
      return SymbolTableHelpers.Contains<UcsTableRecord, UcsTable>(source, ut => ut.Has(id));
    }

    public static ObjectId Add(this IEnumerable<UcsTableRecord> source, UcsTableRecord item)
    {
      return SymbolTableHelpers.Add<UcsTableRecord, UcsTable>(source, item);
    }

    public static IEnumerable<ObjectId> Add(this IEnumerable<UcsTableRecord> source, IEnumerable<UcsTableRecord> items)
    {
      return SymbolTableHelpers.AddRange<UcsTableRecord, UcsTable>(source, items);
    }
  }
}
