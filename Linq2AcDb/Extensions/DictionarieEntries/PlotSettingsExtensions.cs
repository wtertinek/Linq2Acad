using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2AcDb
{
  public static class PlotSettingsExtensions
  {
    public static PlotSettings GetItem(this IEnumerable<PlotSettings> source, string name)
    {
      return DBDictionaryHelpers.GetItem<PlotSettings>(source, ld => ld.GetAt(name));
    }

    public static bool Contains(this IEnumerable<PlotSettings> source, string name)
    {
      return DBDictionaryHelpers.Contains<PlotSettings>(source, ld => ld.Contains(name));
    }

    public static bool Contains(this IEnumerable<PlotSettings> source, ObjectId id)
    {
      return DBDictionaryHelpers.Contains<PlotSettings>(source, ld => ld.Contains(id));
    }

    public static ObjectId Add(this IEnumerable<PlotSettings> source, string name, PlotSettings item)
    {
      return DBDictionaryHelpers.Set<PlotSettings>(source, name, item);
    }

    public static IEnumerable<ObjectId> Add(this IEnumerable<PlotSettings> source, IEnumerable<string> names, IEnumerable<PlotSettings> items)
    {
      return DBDictionaryHelpers.SetRange<PlotSettings>(source, names, items);
    }
  }
}
