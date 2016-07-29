using System;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;

namespace Linq2Acad.Tests
{
  ///////////////////////////////////////////
  // This code is automatically generated ///
  ///////////////////////////////////////////
  public class LayoutTests
  {
    [CommandMethod("TestCreateLayout")]
    public void TestCreateLayout()
    {
      using (var db = AcadDatabase.Active())
      {
        var newLayout = db.Layouts.Create("NewLayout");
        var ok = Assert.Dictionary(db.Database, dict => dict.Contains("NewLayout"));
        
        Console.WriteLine("Test result TestCreateLayout: " + (ok ? "PASSED" : "FAILED"));
      }
    }
  }
}
