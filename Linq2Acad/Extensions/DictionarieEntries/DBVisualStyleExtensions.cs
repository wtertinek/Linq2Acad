using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2Acad
{
  public static class DBVisualStyleExtensions
  {
    public static DBVisualStyle GetItem(this IEnumerable<DBVisualStyle> source, string name)
    {
      return DBDictionaryHelpers.GetItem<DBVisualStyle>(source, vd => vd.GetAt(name));
    }

    public static bool Contains(this IEnumerable<DBVisualStyle> source, string name)
    {
      return DBDictionaryHelpers.Contains<DBVisualStyle>(source, vd => vd.Contains(name), s => s.Name == name);
    }

    public static bool Contains(this IEnumerable<DBVisualStyle> source, ObjectId id)
    {
      return DBDictionaryHelpers.Contains<DBVisualStyle>(source, vd => vd.Contains(id), s => s.ObjectId == id);
    }

    public static ObjectId Add(this IEnumerable<DBVisualStyle> source, string name, DBVisualStyle item)
    {
      return DBDictionaryHelpers.Add<DBVisualStyle>(source, name, item);
    }

    public static IEnumerable<ObjectId> Add(this IEnumerable<DBVisualStyle> source, IEnumerable<string> names, IEnumerable<DBVisualStyle> items)
    {
      return DBDictionaryHelpers.AddRange<DBVisualStyle>(source, names, items);
    }

    public static ObjectId Create(this IEnumerable<DBVisualStyle> source, string name)
    {
      return DBDictionaryHelpers.Add<DBVisualStyle>(source, name, new DBVisualStyle());
    }

    public static IEnumerable<ObjectId> Create(this IEnumerable<DBVisualStyle> source, IEnumerable<string> names)
    {
      return DBDictionaryHelpers.AddRange<DBVisualStyle>(source, names, names.Select(n => new DBVisualStyle()));
    }
  }
}
