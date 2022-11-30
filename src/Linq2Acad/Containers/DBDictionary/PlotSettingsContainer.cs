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
  /// A container class that provides access to the elements of the PlotSettings dictionary. In addition to the standard LINQ operations this class provides methods to create, add and import Plotsettings.
  /// </summary>
  public sealed class PlotSettingsContainer : DBDictionaryEnumerableBase<PlotSettings>
  {
    internal PlotSettingsContainer(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID, s => s.PlotSettingsName, () => nameof(PlotSettings.PlotSettingsName))
    {
    }

    /// <summary>
    /// Creates a new PlotSettings with the specified name.
    /// </summary>
    /// <param name="name">The unique name of the PlotSettings element.</param>
    /// <param name="modelType">Determines the plot setup type.</param>
    public PlotSettings Create(string name, bool modelType)
    {
      Require.NotDisposed(database.IsDisposed, nameof(AcadDatabase));
      Require.TransactionNotDisposed(transaction.IsDisposed);
      Require.IsValidSymbolName(name, nameof(name));
      Require.NameDoesNotExist<PlotSettings>(Contains(name), name);

      return AddInternal(new PlotSettings(modelType), name);
    }
  }
}
