using System;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;

namespace Linq2Acad.Tests
{
  ///////////////////////////////////////////
  // This code is automatically generated ///
  ///////////////////////////////////////////
  public class DetailViewStyleTests
  {
    [CommandMethod("TestCreateDetailViewStyle")]
    public void TestCreateDetailViewStyle()
    {
      using (var db = AcadDatabase.Active())
      {
        var newDetailViewStyle = db.DetailViewStyles.Create("NewDetailViewStyle");
        var ok = Assert.Dictionary(db.Database, dict => dict.Contains("NewDetailViewStyle"));
        
        Console.WriteLine("Test result TestCreateDetailViewStyle: " + (ok ? "PASSED" : "FAILED"));
      }
    }
  }
}
