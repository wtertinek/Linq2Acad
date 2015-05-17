using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2Acad
{
  public static class BlockTableRecordExtensions
  {
    public static bool IsValidName(this IEnumerable<BlockTableRecord> source, string name, bool allowVerticalBar)
    {
      return TableHelpers.IsValidName(name, allowVerticalBar);
    }

    public static BlockTableRecord GetItem(this IEnumerable<BlockTableRecord> source, string name)
    {
      return TableHelpers.GetItem<BlockTableRecord, BlockTable>(source, bt => bt[name]);
    }

    public static bool Contains(this IEnumerable<BlockTableRecord> source, string name)
    {
      return TableHelpers.Contains<BlockTableRecord, BlockTable>(source, bt => bt.Has(name), btr => btr.Name == name);
    }

    public static bool Contains(this IEnumerable<BlockTableRecord> source, ObjectId id)
    {
      return TableHelpers.Contains<BlockTableRecord, BlockTable>(source, bt => bt.Has(id), btr => btr.ObjectId == id);
    }

    public static ObjectId Add(this IEnumerable<BlockTableRecord> source, BlockTableRecord item)
    {
      return TableHelpers.Add<BlockTableRecord, BlockTable>(source, item);
    }

    public static IEnumerable<ObjectId> Add(this IEnumerable<BlockTableRecord> source, IEnumerable<BlockTableRecord> items)
    {
      return TableHelpers.AddRange<BlockTableRecord, BlockTable>(source, items);
    }

    public static ObjectId Create(this IEnumerable<BlockTableRecord> source, string name)
    {
      return TableHelpers.Add<BlockTableRecord, BlockTable>(source, new BlockTableRecord() { Name = name });
    }

    public static IEnumerable<ObjectId> Create(this IEnumerable<BlockTableRecord> source, IEnumerable<string> names)
    {
      return TableHelpers.AddRange<BlockTableRecord, BlockTable>(source, names.Select(n => new BlockTableRecord() { Name = n }));
    }
  }
}
