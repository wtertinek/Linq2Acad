using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2Acad
{
  public static class MlineStyleExtensions
  {
    public static MlineStyle GetItem(this IEnumerable<MlineStyle> source, string name)
    {
      return DBDictionaryHelpers.GetItem<MlineStyle>(source, ld => ld.GetAt(name));
    }

    public static bool Contains(this IEnumerable<MlineStyle> source, string name)
    {
      return DBDictionaryHelpers.Contains<MlineStyle>(source, ld => ld.Contains(name));
    }

    public static bool Contains(this IEnumerable<MlineStyle> source, ObjectId id)
    {
      return DBDictionaryHelpers.Contains<MlineStyle>(source, ld => ld.Contains(id));
    }

    public static ObjectId Add(this IEnumerable<MlineStyle> source, string name, MlineStyle item)
    {
      return DBDictionaryHelpers.Set<MlineStyle>(source, name, item);
    }

    public static IEnumerable<ObjectId> Add(this IEnumerable<MlineStyle> source, IEnumerable<string> names, IEnumerable<MlineStyle> items)
    {
      return DBDictionaryHelpers.SetRange<MlineStyle>(source, names, items);
    }
  }
}
