using System;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;

namespace Linq2Acad.Tests
{
  ///////////////////////////////////////////
  // This code is automatically generated ///
  ///////////////////////////////////////////
  public class TableStyleTests
  {
    [CommandMethod("TestCreateTableStyle")]
    public void TestCreateTableStyle()
    {
      using (var db = AcadDatabase.Active())
      {
        var newTableStyle = db.TableStyles.Create("NewTableStyle");
        var ok = Assert.Dictionary(db.Database, dict => dict.Contains("NewTableStyle"));
        
        Console.WriteLine("Test result TestCreateTableStyle: " + (ok ? "PASSED" : "FAILED"));
      }
    }
  }
}
