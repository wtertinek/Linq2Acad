using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2AcDb
{
  public static class SectionViewStyleExtensions
  {
    public static SectionViewStyle GetItem(this IEnumerable<SectionViewStyle> source, string name)
    {
      return DBDictionaryHelpers.GetItem<SectionViewStyle>(source, ld => ld.GetAt(name));
    }

    public static bool Contains(this IEnumerable<SectionViewStyle> source, string name)
    {
      return DBDictionaryHelpers.Contains<SectionViewStyle>(source, ld => ld.Contains(name));
    }

    public static bool Contains(this IEnumerable<SectionViewStyle> source, ObjectId id)
    {
      return DBDictionaryHelpers.Contains<SectionViewStyle>(source, ld => ld.Contains(id));
    }

    public static ObjectId Set(this IEnumerable<SectionViewStyle> source, string name, SectionViewStyle item)
    {
      return DBDictionaryHelpers.Set<SectionViewStyle>(source, name, item);
    }

    public static IEnumerable<ObjectId> Set(this IEnumerable<SectionViewStyle> source, IEnumerable<string> names, IEnumerable<SectionViewStyle> items)
    {
      return DBDictionaryHelpers.SetRange<SectionViewStyle>(source, names, items);
    }
  }
}
