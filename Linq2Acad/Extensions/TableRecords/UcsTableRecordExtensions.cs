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
      return TableHelpers.IsValidName(name, allowVerticalBar);
    }

    public static UcsTableRecord GetItem(this IEnumerable<UcsTableRecord> source, string name)
    {
      return TableHelpers.GetItem<UcsTableRecord, UcsTable>(source, ut => ut[name]);
    }

    public static bool Contains(this IEnumerable<UcsTableRecord> source, string name)
    {
      return TableHelpers.Contains<UcsTableRecord, UcsTable>(source, ut => ut.Has(name), utr => utr.Name == name);
    }

    public static bool Contains(this IEnumerable<UcsTableRecord> source, ObjectId id)
    {
      return TableHelpers.Contains<UcsTableRecord, UcsTable>(source, ut => ut.Has(id), utr => utr.ObjectId == id);
    }

    public static ObjectId Add(this IEnumerable<UcsTableRecord> source, UcsTableRecord item)
    {
      return TableHelpers.Add<UcsTableRecord, UcsTable>(source, item);
    }

    public static IEnumerable<ObjectId> Add(this IEnumerable<UcsTableRecord> source, IEnumerable<UcsTableRecord> items)
    {
      return TableHelpers.AddRange<UcsTableRecord, UcsTable>(source, items);
    }
  }
}
