using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2AcDb
{
  public static class MLeaderStyleExtensions
  {
    public static MLeaderStyle GetItem(this IEnumerable<MLeaderStyle> source, string name)
    {
      return DBDictionaryHelpers.GetItem<MLeaderStyle>(source, ld => ld.GetAt(name));
    }

    public static bool Contains(this IEnumerable<MLeaderStyle> source, string name)
    {
      return DBDictionaryHelpers.Contains<MLeaderStyle>(source, ld => ld.Contains(name));
    }

    public static bool Contains(this IEnumerable<MLeaderStyle> source, ObjectId id)
    {
      return DBDictionaryHelpers.Contains<MLeaderStyle>(source, ld => ld.Contains(id));
    }

    public static ObjectId Set(this IEnumerable<MLeaderStyle> source, string name, MLeaderStyle item)
    {
      return DBDictionaryHelpers.Set<MLeaderStyle>(source, name, item);
    }

    public static IEnumerable<ObjectId> Set(this IEnumerable<MLeaderStyle> source, IEnumerable<string> names, IEnumerable<MLeaderStyle> items)
    {
      return DBDictionaryHelpers.SetRange<MLeaderStyle>(source, names, items);
    }
  }
}
