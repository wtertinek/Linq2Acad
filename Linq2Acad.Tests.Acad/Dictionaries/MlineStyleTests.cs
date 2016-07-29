using System;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;

namespace Linq2Acad.Tests
{
  ///////////////////////////////////////////
  // This code is automatically generated ///
  ///////////////////////////////////////////
  public class MlineStyleTests
  {
    [CommandMethod("TestCreateMlineStyle")]
    public void TestCreateMlineStyle()
    {
      using (var db = AcadDatabase.Active())
      {
        var newMlineStyle = db.MlineStyles.Create("NewMlineStyle");
        var ok = Assert.Dictionary(db.Database, dict => dict.Contains("NewMlineStyle"));
        
        Console.WriteLine("Test result TestCreateMlineStyle: " + (ok ? "PASSED" : "FAILED"));
      }
    }
  }
}
