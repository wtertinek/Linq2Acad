using System;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;

namespace Linq2Acad.Tests
{
  ///////////////////////////////////////////
  // This code is automatically generated ///
  ///////////////////////////////////////////
  public class RegAppTableRecordTests
  {
    [CommandMethod("TestCreateRegAppTableRecord")]
    public void TestCreateRegAppTableRecord()
    {
      using (var db = AcadDatabase.Active())
      {
        var newRegApp = db.RegApps.Create("NewRegApp");
        var ok = Assert.Table<RegAppTable>(db.Database, table => table.Has("NewRegApp"));
        
        Console.WriteLine("Test result TestCreateRegAppTableRecord: " + (ok ? "PASSED" : "FAILED"));
      }
    }

    [CommandMethod("TestAddRegAppTableRecord")]
    public void TestAddRegAppTableRecord()
    {
      using (var db = AcadDatabase.Active())
      {
        db.RegApps.Add(new RegAppTableRecord() { Name = "NewRegApp" });
        var ok = Assert.Table<RegAppTable>(db.Database, table => table.Has("NewRegApp"));
        
        Console.WriteLine("Test result TestAddRegAppTableRecord: " + (ok ? "PASSED" : "FAILED"));
      }
    }
  }
}
