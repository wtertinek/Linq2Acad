using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2Acad
{
  public static class ViewTableRecordExtensions
  {
    public static bool IsValidName(this IEnumerable<ViewTableRecord> source, string name, bool allowVerticalBar)
    {
      return TableHelpers.IsValidName(name, allowVerticalBar);
    }

    public static ViewTableRecord GetItem(this IEnumerable<ViewTableRecord> source, string name)
    {
      return TableHelpers.GetItem<ViewTableRecord, ViewTable>(source, vt => vt[name]);
    }

    public static bool Contains(this IEnumerable<ViewTableRecord> source, string name)
    {
      return TableHelpers.Contains<ViewTableRecord, ViewTable>(source, vt => vt.Has(name), vtr => vtr.Name == name);
    }

    public static bool Contains(this IEnumerable<ViewTableRecord> source, ObjectId id)
    {
      return TableHelpers.Contains<ViewTableRecord, ViewTable>(source, vt => vt.Has(id), vtr => vtr.ObjectId == id);
    }

    public static ObjectId Add(this IEnumerable<ViewTableRecord> source, ViewTableRecord item)
    {
      return TableHelpers.Add<ViewTableRecord, ViewTable>(source, item);
    }

    public static IEnumerable<ObjectId> Add(this IEnumerable<ViewTableRecord> source, IEnumerable<ViewTableRecord> items)
    {
      return TableHelpers.AddRange<ViewTableRecord, ViewTable>(source, items);
    }

    public static ObjectId Create(this IEnumerable<ViewTableRecord> source, string name)
    {
      return TableHelpers.Add<ViewTableRecord, ViewTable>(source, new ViewTableRecord() { Name = name });
    }

    public static IEnumerable<ObjectId> Create(this IEnumerable<ViewTableRecord> source, IEnumerable<string> names)
    {
      return TableHelpers.AddRange<ViewTableRecord, ViewTable>(source, names.Select(n => new ViewTableRecord() { Name = n }));
    }
  }
}
