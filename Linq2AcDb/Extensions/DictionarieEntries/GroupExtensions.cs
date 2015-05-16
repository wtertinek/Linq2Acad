using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2AcDb
{
  public static class GroupExtensions
  {
    public static Group GetItem(this IEnumerable<Group> source, string name)
    {
      return DBDictionaryHelpers.GetItem<Group>(source, ld => ld.GetAt(name));
    }

    public static bool Contains(this IEnumerable<Group> source, string name)
    {
      return DBDictionaryHelpers.Contains<Group>(source, ld => ld.Contains(name));
    }

    public static bool Contains(this IEnumerable<Group> source, ObjectId id)
    {
      return DBDictionaryHelpers.Contains<Group>(source, ld => ld.Contains(id));
    }

    public static ObjectId Add(this IEnumerable<Group> source, string name, Group item)
    {
      return DBDictionaryHelpers.Set<Group>(source, name, item);
    }

    public static IEnumerable<ObjectId> Add(this IEnumerable<Group> source, IEnumerable<string> names, IEnumerable<Group> items)
    {
      return DBDictionaryHelpers.SetRange<Group>(source, names, items);
    }
  }
}
