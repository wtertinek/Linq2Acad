using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2Acad
{
  public static class TableStyleExtensions
  {
    public static TableStyle GetItem(this IEnumerable<TableStyle> source, string name)
    {
      return DBDictionaryHelpers.GetItem<TableStyle>(source, sd => sd.GetAt(name));
    }

    public static bool Contains(this IEnumerable<TableStyle> source, string name)
    {
      return DBDictionaryHelpers.Contains<TableStyle>(source, sd => sd.Contains(name), s => s.Name == name);
    }

    public static bool Contains(this IEnumerable<TableStyle> source, ObjectId id)
    {
      return DBDictionaryHelpers.Contains<TableStyle>(source, sd => sd.Contains(id), s => s.ObjectId == id);
    }

    public static ObjectId Add(this IEnumerable<TableStyle> source, string name, TableStyle item)
    {
      return DBDictionaryHelpers.Set<TableStyle>(source, name, item);
    }

    public static IEnumerable<ObjectId> Add(this IEnumerable<TableStyle> source, IEnumerable<string> names, IEnumerable<TableStyle> items)
    {
      return DBDictionaryHelpers.SetRange<TableStyle>(source, names, items);
    }
  }
}
