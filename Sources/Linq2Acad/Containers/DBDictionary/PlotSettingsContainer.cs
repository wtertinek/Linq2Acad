using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Linq2Acad
{
  /// <summary>
  /// A container class that provides access to the elements of the PlottSettings ditionary.
  /// </summary>
  public sealed class PlotSettingsContainer : DBDictionaryEnumerable<PlotSettings>
  {
    private bool modelType;

    internal PlotSettingsContainer(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID)
    {
    }

    /// <summary>
    /// Creates a new PlotSettings element.
    /// </summary>
    /// <param name="name">The unique name of the PlotSettings element.</param>
    /// <param name="modelType">Determines the plot setup type.</param>
    public PlotSettings Create(string name, bool modelType)
    {
      Require.IsValidSymbolName(name, nameof(name));
      Require.NameDoesNotExist<PlotSettings>(Contains(name), name);

      this.modelType = modelType;

      return AddInternal(new PlotSettings(modelType), name);
    }

    /// <summary>
    /// Adds a newly created PlotSettings element.
    /// </summary>
    /// <param name="item">The PlotSettings to element add.</param>
    public void Add(PlotSettings item)
    {
      Require.ParameterNotNull(item, nameof(item));
      Require.IsValidSymbolName(item.PlotSettingsName, nameof(item.PlotSettingsName));
      Require.NameDoesNotExist<PlotSettings>(Contains(item.PlotSettingsName), item.PlotSettingsName);

      AddRangeInternal(new[] { Tuple.Create(item, item.PlotSettingsName) });
    }

    /// <summary>
    /// Adds a collection of newly created PlotSettings elements.
    /// </summary>
    /// <param name="items">The PlotSettings elements to add.</param>
    public void AddRange(IEnumerable<PlotSettings> items)
    {
      Require.ParameterNotNull(items, nameof(items));

      foreach (var item in items)
      {
        Require.ParameterNotNull(item, nameof(item));
        Require.IsValidSymbolName(item.PlotSettingsName, nameof(item.PlotSettingsName));
        Require.NameDoesNotExist<PlotSettings>(Contains(item.PlotSettingsName), item.PlotSettingsName);
      }

      AddRangeInternal(items.Select(i => Tuple.Create(i, i.PlotSettingsName)));
    }
  }
}
