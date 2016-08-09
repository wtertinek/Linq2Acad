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
      var newId = ObjectId.Null;

      using (var db = AcadDatabase.Active())
      {
        var newPlotSettings = db.PlotSettings.Create("NewPlotSettings", true);
        newId = newPlotSettings.ObjectId;
      }

      Assert.That.PlotSettingsDictionary.Contains("NewPlotSettings");
      Assert.That.PlotSettingsDictionary.Contains(newId);
    }
  }
}
