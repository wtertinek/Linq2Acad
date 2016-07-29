using System;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;

namespace Linq2Acad.Tests
{
  ///////////////////////////////////////////
  // This code is automatically generated ///
  ///////////////////////////////////////////
  public class MaterialTests
  {
    [CommandMethod("TestCreateMaterial")]
    public void TestCreateMaterial()
    {
      using (var db = AcadDatabase.Active())
      {
        var newMaterial = db.Materials.Create("NewMaterial");
        var ok = Assert.Dictionary(db.Database, dict => dict.Contains("NewMaterial"));
        
        Console.WriteLine("Test result TestCreateMaterial: " + (ok ? "PASSED" : "FAILED"));
      }
    }
  }
}
