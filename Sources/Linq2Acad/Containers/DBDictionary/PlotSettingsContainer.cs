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
    /// <param name="element">The PlotSettings to element add.</param>
    public void Add(PlotSettings element)
    {
      Require.ParameterNotNull(element, nameof(element));
      Require.IsValidSymbolName(element.PlotSettingsName, nameof(element.PlotSettingsName));
      Require.NameDoesNotExist<PlotSettings>(Contains(element.PlotSettingsName), element.PlotSettingsName);

      AddRangeInternal(new[] { Tuple.Create(element, element.PlotSettingsName) });
    }

    /// <summary>
    /// Adds a collection of newly created PlotSettings elements.
    /// </summary>
    /// <param name="elements">The PlotSettings elements to add.</param>
    public void AddRange(IEnumerable<PlotSettings> elements)
    {
      Require.ParameterNotNull(elements, nameof(elements));

      foreach (var element in elements)
      {
        Require.ParameterNotNull(element, nameof(element));
        Require.IsValidSymbolName(element.PlotSettingsName, nameof(element.PlotSettingsName));
        Require.NameDoesNotExist<PlotSettings>(Contains(element.PlotSettingsName), element.PlotSettingsName);
      }

      AddRangeInternal(elements.Select(i => Tuple.Create(i, i.PlotSettingsName)));
    }
  }
}
