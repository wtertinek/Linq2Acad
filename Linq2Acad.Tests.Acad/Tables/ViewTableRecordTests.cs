using System;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;

namespace Linq2Acad.Tests
{
  ///////////////////////////////////////////
  // This code is automatically generated ///
  ///////////////////////////////////////////
  public class ViewTableRecordTests
  {
    [CommandMethod("TestCreateViewTableRecord")]
    public void TestCreateViewTableRecord()
    {
      using (var db = AcadDatabase.Active())
      {
        var newView = db.Views.Create("NewView");
        var ok = Assert.Table<ViewTable>(db.Database, table => table.Has("NewView"));
        
        Console.WriteLine("Test result TestCreateViewTableRecord: " + (ok ? "PASSED" : "FAILED"));
      }
    }

    [CommandMethod("TestAddViewTableRecord")]
    public void TestAddViewTableRecord()
    {
      using (var db = AcadDatabase.Active())
      {
        db.Views.Add(new ViewTableRecord() { Name = "NewView" });
        var ok = Assert.Table<ViewTable>(db.Database, table => table.Has("NewView"));
        
        Console.WriteLine("Test result TestAddViewTableRecord: " + (ok ? "PASSED" : "FAILED"));
      }
    }
  }
}
