using System;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;

namespace Linq2Acad.Tests
{
  ///////////////////////////////////////////
  // This code is automatically generated ///
  ///////////////////////////////////////////
  public class MLeaderStyleTests
  {
    [CommandMethod("TestCreateMLeaderStyle")]
    public void TestCreateMLeaderStyle()
    {
      using (var db = AcadDatabase.Active())
      {
        var newMLeaderStyle = db.MLeaderStyles.Create("NewMLeaderStyle");
        var ok = Assert.Dictionary(db.Database, dict => dict.Contains("NewMLeaderStyle"));
        
        Console.WriteLine("Test result TestCreateMLeaderStyle: " + (ok ? "PASSED" : "FAILED"));
      }
    }
  }
}
