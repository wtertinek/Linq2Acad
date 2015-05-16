using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2Acad
{
  public static class MaterialExtensions
  {
    public static Material GetItem(this IEnumerable<Material> source, string name)
    {
      return DBDictionaryHelpers.GetItem<Material>(source, md => md.GetAt(name));
    }

    public static bool Contains(this IEnumerable<Material> source, string name)
    {
      return DBDictionaryHelpers.Contains<Material>(source, md => md.Contains(name), m => m.Name == name);
    }

    public static bool Contains(this IEnumerable<Material> source, ObjectId id)
    {
      return DBDictionaryHelpers.Contains<Material>(source, md => md.Contains(id), m => m.ObjectId == id);
    }

    public static ObjectId Add(this IEnumerable<Material> source, string name, Material item)
    {
      return DBDictionaryHelpers.Set<Material>(source, name, item);
    }

    public static IEnumerable<ObjectId> Add(this IEnumerable<Material> source, IEnumerable<string> names, IEnumerable<Material> items)
    {
      return DBDictionaryHelpers.SetRange<Material>(source, names, items);
    }
  }
}
