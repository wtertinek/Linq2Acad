using System;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;

namespace Linq2Acad.Tests
{
  ///////////////////////////////////////////
  // This code is automatically generated ///
  ///////////////////////////////////////////
  public class UcsTableRecordTests
  {
    [CommandMethod("TestCreateUcsTableRecord")]
    public void TestCreateUcsTableRecord()
    {
      using (var db = AcadDatabase.Active())
      {
        var newUcs = db.Ucss.Create("NewUcs");
        var ok = Assert.Table<UcsTable>(db.Database, table => table.Has("NewUcs"));
        
        Console.WriteLine("Test result TestCreateUcsTableRecord: " + (ok ? "PASSED" : "FAILED"));
      }
    }

    [CommandMethod("TestAddUcsTableRecord")]
    public void TestAddUcsTableRecord()
    {
      using (var db = AcadDatabase.Active())
      {
        db.Ucss.Add(new UcsTableRecord() { Name = "NewUcs" });
        var ok = Assert.Table<UcsTable>(db.Database, table => table.Has("NewUcs"));
        
        Console.WriteLine("Test result TestAddUcsTableRecord: " + (ok ? "PASSED" : "FAILED"));
      }
    }
  }
}
