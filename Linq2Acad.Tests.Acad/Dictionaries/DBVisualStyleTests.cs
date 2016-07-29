using System;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;

namespace Linq2Acad.Tests
{
  ///////////////////////////////////////////
  // This code is automatically generated ///
  ///////////////////////////////////////////
  public class DBVisualStyleTests
  {
    [CommandMethod("TestCreateDBVisualStyle")]
    public void TestCreateDBVisualStyle()
    {
      using (var db = AcadDatabase.Active())
      {
        var newDBVisualStyle = db.DBVisualStyles.Create("NewDBVisualStyle");
        var ok = Assert.Dictionary(db.Database, dict => dict.Contains("NewDBVisualStyle"));
        
        Console.WriteLine("Test result TestCreateDBVisualStyle: " + (ok ? "PASSED" : "FAILED"));
      }
    }
  }
}
