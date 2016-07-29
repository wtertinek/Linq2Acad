using System;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;

namespace Linq2Acad.Tests
{
  ///////////////////////////////////////////
  // This code is automatically generated ///
  ///////////////////////////////////////////
  public class LinetypeTableRecordTests
  {
    [CommandMethod("TestCreateLinetypeTableRecord")]
    public void TestCreateLinetypeTableRecord()
    {
      using (var db = AcadDatabase.Active())
      {
        var newLinetype = db.Linetypes.Create("NewLinetype");
        var ok = Assert.Table<LinetypeTable>(db.Database, table => table.Has("NewLinetype"));
        
        Console.WriteLine("Test result TestCreateLinetypeTableRecord: " + (ok ? "PASSED" : "FAILED"));
      }
    }

    [CommandMethod("TestAddLinetypeTableRecord")]
    public void TestAddLinetypeTableRecord()
    {
      using (var db = AcadDatabase.Active())
      {
        db.Linetypes.Add(new LinetypeTableRecord() { Name = "NewLinetype" });
        var ok = Assert.Table<LinetypeTable>(db.Database, table => table.Has("NewLinetype"));
        
        Console.WriteLine("Test result TestAddLinetypeTableRecord: " + (ok ? "PASSED" : "FAILED"));
      }
    }
  }
}
