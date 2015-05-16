using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2Acad
{
  public static class LayoutExtensions
  {
    public static Layout GetItem(this IEnumerable<Layout> source, string name)
    {
      return DBDictionaryHelpers.GetItem<Layout>(source, ld => ld.GetAt(name));
    }

    public static bool Contains(this IEnumerable<Layout> source, string name)
    {
      return DBDictionaryHelpers.Contains<Layout>(source, ld => ld.Contains(name), l => l.LayoutName == name);
    }

    public static bool Contains(this IEnumerable<Layout> source, ObjectId id)
    {
      return DBDictionaryHelpers.Contains<Layout>(source, ld => ld.Contains(id), l => l.ObjectId == id);
    }

    public static ObjectId Add(this IEnumerable<Layout> source, string name, Layout item)
    {
      return DBDictionaryHelpers.Set<Layout>(source, name, item);
    }

    public static IEnumerable<ObjectId> Add(this IEnumerable<Layout> source, IEnumerable<string> names, IEnumerable<Layout> items)
    {
      return DBDictionaryHelpers.SetRange<Layout>(source, names, items);
    }
  }
}
