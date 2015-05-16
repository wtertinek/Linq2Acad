using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2Acad
{
  public static class DetailViewStyleExtensions
  {
    public static DetailViewStyle GetItem(this IEnumerable<DetailViewStyle> source, string name)
    {
      return DBDictionaryHelpers.GetItem<DetailViewStyle>(source, ld => ld.GetAt(name));
    }

    public static bool Contains(this IEnumerable<DetailViewStyle> source, string name)
    {
      return DBDictionaryHelpers.Contains<DetailViewStyle>(source, ld => ld.Contains(name));
    }

    public static bool Contains(this IEnumerable<DetailViewStyle> source, ObjectId id)
    {
      return DBDictionaryHelpers.Contains<DetailViewStyle>(source, ld => ld.Contains(id));
    }

    public static ObjectId Add(this IEnumerable<DetailViewStyle> source, string name, DetailViewStyle item)
    {
      return DBDictionaryHelpers.Set<DetailViewStyle>(source, name, item);
    }

    public static IEnumerable<ObjectId> Add(this IEnumerable<DetailViewStyle> source, IEnumerable<string> names, IEnumerable<DetailViewStyle> items)
    {
      return DBDictionaryHelpers.SetRange<DetailViewStyle>(source, names, items);
    }
  }
}
