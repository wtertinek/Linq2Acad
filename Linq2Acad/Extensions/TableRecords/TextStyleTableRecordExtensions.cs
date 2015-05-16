using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2Acad
{
  public static class TextStyleTableRecordExtensions
  {
    public static bool IsValidName(this IEnumerable<TextStyleTableRecord> source, string name, bool allowVerticalBar)
    {
      return TableHelpers.IsValidName(name, allowVerticalBar);
    }

    public static TextStyleTableRecord GetItem(this IEnumerable<TextStyleTableRecord> source, string name)
    {
      return TableHelpers.GetItem<TextStyleTableRecord, TextStyleTable>(source, tst => tst[name]);
    }

    public static bool Contains(this IEnumerable<TextStyleTableRecord> source, string name)
    {
      return TableHelpers.Contains<TextStyleTableRecord, TextStyleTable>(source, tst => tst.Has(name));
    }

    public static bool Contains(this IEnumerable<TextStyleTableRecord> source, ObjectId id)
    {
      return TableHelpers.Contains<TextStyleTableRecord, TextStyleTable>(source, tst => tst.Has(id));
    }

    public static ObjectId Add(this IEnumerable<TextStyleTableRecord> source, TextStyleTableRecord item)
    {
      return TableHelpers.Add<TextStyleTableRecord, TextStyleTable>(source, item);
    }

    public static IEnumerable<ObjectId> Add(this IEnumerable<TextStyleTableRecord> source, IEnumerable<TextStyleTableRecord> items)
    {
      return TableHelpers.AddRange<TextStyleTableRecord, TextStyleTable>(source, items);
    }
  }
}
