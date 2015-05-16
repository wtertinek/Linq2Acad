using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2Acad
{
  public static class MLeaderStyleExtensions
  {
    public static MLeaderStyle GetItem(this IEnumerable<MLeaderStyle> source, string name)
    {
      return DBDictionaryHelpers.GetItem<MLeaderStyle>(source, sd => sd.GetAt(name));
    }

    public static bool Contains(this IEnumerable<MLeaderStyle> source, string name)
    {
      return DBDictionaryHelpers.Contains<MLeaderStyle>(source, sd => sd.Contains(name), s => s.Name == name);
    }

    public static bool Contains(this IEnumerable<MLeaderStyle> source, ObjectId id)
    {
      return DBDictionaryHelpers.Contains<MLeaderStyle>(source, sd => sd.Contains(id), s => s.ObjectId == id);
    }

    public static ObjectId Add(this IEnumerable<MLeaderStyle> source, string name, MLeaderStyle item)
    {
      return DBDictionaryHelpers.Set<MLeaderStyle>(source, name, item);
    }

    public static IEnumerable<ObjectId> Add(this IEnumerable<MLeaderStyle> source, IEnumerable<string> names, IEnumerable<MLeaderStyle> items)
    {
      return DBDictionaryHelpers.SetRange<MLeaderStyle>(source, names, items);
    }
  }
}
