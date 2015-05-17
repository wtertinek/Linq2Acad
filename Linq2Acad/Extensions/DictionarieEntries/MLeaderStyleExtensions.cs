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
      return DBDictionaryHelpers.Add<MLeaderStyle>(source, name, item);
    }

    public static IEnumerable<ObjectId> Add(this IEnumerable<MLeaderStyle> source, IEnumerable<string> names, IEnumerable<MLeaderStyle> items)
    {
      return DBDictionaryHelpers.AddRange<MLeaderStyle>(source, names, items);
    }

    public static ObjectId Create(this IEnumerable<MLeaderStyle> source, string name)
    {
      return DBDictionaryHelpers.Add<MLeaderStyle>(source, name, new MLeaderStyle());
    }

    public static IEnumerable<ObjectId> Create(this IEnumerable<MLeaderStyle> source, IEnumerable<string> names)
    {
      return DBDictionaryHelpers.AddRange<MLeaderStyle>(source, names, names.Select(n => new MLeaderStyle()));
    }
  }
}
