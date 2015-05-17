using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2Acad
{
  public static class PlotSettingsExtensions
  {
    public static PlotSettings GetItem(this IEnumerable<PlotSettings> source, string name)
    {
      return DBDictionaryHelpers.GetItem<PlotSettings>(source, sd => sd.GetAt(name));
    }

    public static bool Contains(this IEnumerable<PlotSettings> source, string name)
    {
      return DBDictionaryHelpers.Contains<PlotSettings>(source, sd => sd.Contains(name), s => s.PlotSettingsName == name);
    }

    public static bool Contains(this IEnumerable<PlotSettings> source, ObjectId id)
    {
      return DBDictionaryHelpers.Contains<PlotSettings>(source, sd => sd.Contains(id), s => s.ObjectId == id);
    }

    public static ObjectId Add(this IEnumerable<PlotSettings> source, string name, PlotSettings item)
    {
      return DBDictionaryHelpers.Add<PlotSettings>(source, name, item);
    }

    public static IEnumerable<ObjectId> Add(this IEnumerable<PlotSettings> source, IEnumerable<string> names, IEnumerable<PlotSettings> items)
    {
      return DBDictionaryHelpers.AddRange<PlotSettings>(source, names, items);
    }

    public static ObjectId Create(this IEnumerable<PlotSettings> source, string name, bool modelType)
    {
      return DBDictionaryHelpers.Add<PlotSettings>(source, name, new PlotSettings(modelType));
    }

    public static IEnumerable<ObjectId> Create(this IEnumerable<PlotSettings> source, IEnumerable<string> names, bool modelType)
    {
      return DBDictionaryHelpers.AddRange<PlotSettings>(source, names, names.Select(n => new PlotSettings(modelType)));
    }
  }
}
