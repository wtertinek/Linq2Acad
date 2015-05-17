using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2Acad
{
  public static class GroupExtensions
  {
    public static Group GetItem(this IEnumerable<Group> source, string name)
    {
      return DBDictionaryHelpers.GetItem<Group>(source, gd => gd.GetAt(name));
    }

    public static bool Contains(this IEnumerable<Group> source, string name)
    {
      return DBDictionaryHelpers.Contains<Group>(source, gd => gd.Contains(name), g => g.Name == name);
    }

    public static bool Contains(this IEnumerable<Group> source, ObjectId id)
    {
      return DBDictionaryHelpers.Contains<Group>(source, gd => gd.Contains(id), s => s.ObjectId == id);
    }

    public static ObjectId Add(this IEnumerable<Group> source, string name, Group item)
    {
      return DBDictionaryHelpers.Add<Group>(source, name, item);
    }

    public static IEnumerable<ObjectId> Add(this IEnumerable<Group> source, IEnumerable<string> names, IEnumerable<Group> items)
    {
      return DBDictionaryHelpers.AddRange<Group>(source, names, items);
    }

    public static Group Create(this IEnumerable<Group> source, string name)
    {
      return Create(source, name, false);
    }

    public static Group Create(this IEnumerable<Group> source, string name, bool selectable)
    {
      var group = new Group() { Name = name, Selectable = selectable };
      DBDictionaryHelpers.Add<Group>(source, name, group);
      return group;
    }

    public static Group Create(this IEnumerable<Group> source, string name, IEnumerable<ObjectId> ids)
    {
      return Create(source, name, false, ids);
    }

    public static Group Create(this IEnumerable<Group> source, string name, bool selectable, IEnumerable<ObjectId> ids)
    {
      var group = new Group();
      DBDictionaryHelpers.Add<Group>(source, name, group);
      group.Selectable = selectable;
      return group;
    }

    public static IEnumerable<Group> Create(this IEnumerable<Group> source, IEnumerable<string> names)
    {
      var groups = names.Select(n => new Group())
                        .ToArray();
      DBDictionaryHelpers.AddRange<Group>(source, names, groups);
      return groups;
    }

    public static IEnumerable<Group> Create(this IEnumerable<Group> source, IEnumerable<string> names, bool selectable)
    {
      var groups = names.Select(n => new Group())
                        .ToArray();
      DBDictionaryHelpers.AddRange<Group>(source, names, groups);
      return groups.Select(g => { g.Selectable = selectable; return g; });
    }
  }
}
