using System;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;

namespace Linq2Acad.Tests
{
  ///////////////////////////////////////////
  // This code is automatically generated ///
  ///////////////////////////////////////////
  public class SectionViewStyleTests
  {
    [CommandMethod("TestCreateSectionViewStyle")]
    public void TestCreateSectionViewStyle()
    {
      using (var db = AcadDatabase.Active())
      {
        var newSectionViewStyle = db.SectionViewStyles.Create("NewSectionViewStyle");
        var ok = Assert.Dictionary(db.Database, dict => dict.Contains("NewSectionViewStyle"));
        
        Console.WriteLine("Test result TestCreateSectionViewStyle: " + (ok ? "PASSED" : "FAILED"));
      }
    }
  }
}
