using System;
using System.Linq;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  public partial class PlotSettingsContainerTests
  {
    [AcadTest("CreatePlotSettings")]
    public void CreatePlotSettings()
    {
      using (var db = AcadDatabase.Active())
      {
        var newPlotSettings = db.PlotSettings.Create("NewPlotSettings", true);

        Assert.Dictionary(db.Database, db.Database.PlotSettingsDictionaryId, dict => dict.Contains("NewPlotSettings"),
                          "PlotSettings dictionary does not contain an element with name 'NewPlotSettings'");
        Assert.DictionaryIDs(db.Database, db.Database.PlotSettingsDictionaryId, ids => ids.Any(id => id == newPlotSettings.ObjectId),
                             "PlotSettings dictionary does not contain the newly created element");
      }
    }
  }
}
