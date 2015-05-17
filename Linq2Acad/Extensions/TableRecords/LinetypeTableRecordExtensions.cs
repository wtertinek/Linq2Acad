using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2Acad
{
  public static class LinetypeTableRecordExtensions
  {
    public static bool IsValidName(this IEnumerable<LinetypeTableRecord> source, string name, bool allowVerticalBar)
    {
      return TableHelpers.IsValidName(name, allowVerticalBar);
    }

    public static LinetypeTableRecord GetItem(this IEnumerable<LinetypeTableRecord> source, string name)
    {
      return TableHelpers.GetItem<LinetypeTableRecord, LinetypeTable>(source, lt => lt[name]);
    }

    public static bool Contains(this IEnumerable<LinetypeTableRecord> source, string name)
    {
      return TableHelpers.Contains<LinetypeTableRecord, LinetypeTable>(source, lt => lt.Has(name), ltr => ltr.Name == name);
    }

    public static bool Contains(this IEnumerable<LinetypeTableRecord> source, ObjectId id)
    {
      return TableHelpers.Contains<LinetypeTableRecord, LinetypeTable>(source, lt => lt.Has(id), ltr => ltr.ObjectId == id);
    }

    public static ObjectId Add(this IEnumerable<LinetypeTableRecord> source, LinetypeTableRecord item)
    {
      return TableHelpers.Add<LinetypeTableRecord, LinetypeTable>(source, item);
    }

    public static IEnumerable<ObjectId> Add(this IEnumerable<LinetypeTableRecord> source, IEnumerable<LinetypeTableRecord> items)
    {
      return TableHelpers.AddRange<LinetypeTableRecord, LinetypeTable>(source, items);
    }

    public static ObjectId Create(this IEnumerable<LinetypeTableRecord> source, string name)
    {
      return TableHelpers.Add<LinetypeTableRecord, LinetypeTable>(source, new LinetypeTableRecord() { Name = name });
    }

    public static IEnumerable<ObjectId> Create(this IEnumerable<LinetypeTableRecord> source, IEnumerable<string> names)
    {
      return TableHelpers.AddRange<LinetypeTableRecord, LinetypeTable>(source, names.Select(n => new LinetypeTableRecord() { Name = n }));
    }
  }
}
