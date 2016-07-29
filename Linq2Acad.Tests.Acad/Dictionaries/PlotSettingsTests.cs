using System;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;

namespace Linq2Acad.Tests
{
  ///////////////////////////////////////////
  // This code is automatically generated ///
  ///////////////////////////////////////////
  public class PlotSettingsTests
  {
    [CommandMethod("TestCreatePlotSettings")]
    public void TestCreatePlotSettings()
    {
      using (var db = AcadDatabase.Active())
      {
        var newPlotSettings = db.PlotSettings.Create("NewPlotSettings", true);
        var ok = Assert.Dictionary(db.Database, dict => dict.Contains("NewPlotSettings"));
        
        Console.WriteLine("Test result TestCreatePlotSettings: " + (ok ? "PASSED" : "FAILED"));
      }
    }
  }
}
