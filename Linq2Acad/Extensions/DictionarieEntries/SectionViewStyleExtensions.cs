using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2Acad
{
  public static class SectionViewStyleExtensions
  {
    public static SectionViewStyle GetItem(this IEnumerable<SectionViewStyle> source, string name)
    {
      return DBDictionaryHelpers.GetItem<SectionViewStyle>(source, sd => sd.GetAt(name));
    }

    public static bool Contains(this IEnumerable<SectionViewStyle> source, string name)
    {
      return DBDictionaryHelpers.Contains<SectionViewStyle>(source, sd => sd.Contains(name), s => s.Name == name);
    }

    public static bool Contains(this IEnumerable<SectionViewStyle> source, ObjectId id)
    {
      return DBDictionaryHelpers.Contains<SectionViewStyle>(source, sd => sd.Contains(id), s => s.ObjectId == id);
    }

    public static ObjectId Add(this IEnumerable<SectionViewStyle> source, string name, SectionViewStyle item)
    {
      return DBDictionaryHelpers.Set<SectionViewStyle>(source, name, item);
    }

    public static IEnumerable<ObjectId> Add(this IEnumerable<SectionViewStyle> source, IEnumerable<string> names, IEnumerable<SectionViewStyle> items)
    {
      return DBDictionaryHelpers.SetRange<SectionViewStyle>(source, names, items);
    }
  }
}
