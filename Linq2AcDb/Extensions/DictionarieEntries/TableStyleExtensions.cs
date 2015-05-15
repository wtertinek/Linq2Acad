using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2AcDb
{
  public static class TableStyleExtensions
  {
    public static TableStyle GetItem(this IEnumerable<TableStyle> source, string name)
    {
      return DBDictionaryHelpers.GetItem<TableStyle>(source, ld => ld.GetAt(name));
    }

    public static bool Contains(this IEnumerable<TableStyle> source, string name)
    {
      return DBDictionaryHelpers.Contains<TableStyle>(source, ld => ld.Contains(name));
    }

    public static bool Contains(this IEnumerable<TableStyle> source, ObjectId id)
    {
      return DBDictionaryHelpers.Contains<TableStyle>(source, ld => ld.Contains(id));
    }

    public static ObjectId Set(this IEnumerable<TableStyle> source, string name, TableStyle item)
    {
      return DBDictionaryHelpers.Set<TableStyle>(source, name, item);
    }

    public static IEnumerable<ObjectId> Set(this IEnumerable<TableStyle> source, IEnumerable<string> names, IEnumerable<TableStyle> items)
    {
      return DBDictionaryHelpers.SetRange<TableStyle>(source, names, items);
    }
  }
}
