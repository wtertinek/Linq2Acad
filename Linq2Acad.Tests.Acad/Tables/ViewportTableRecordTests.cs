using System;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;

namespace Linq2Acad.Tests
{
  ///////////////////////////////////////////
  // This code is automatically generated ///
  ///////////////////////////////////////////
  public class ViewportTableRecordTests
  {
    [CommandMethod("TestCreateViewportTableRecord")]
    public void TestCreateViewportTableRecord()
    {
      using (var db = AcadDatabase.Active())
      {
        var newViewport = db.Viewports.Create("NewViewport");
        var ok = Assert.Table<ViewportTable>(db.Database, table => table.Has("NewViewport"));
        
        Console.WriteLine("Test result TestCreateViewportTableRecord: " + (ok ? "PASSED" : "FAILED"));
      }
    }

    [CommandMethod("TestAddViewportTableRecord")]
    public void TestAddViewportTableRecord()
    {
      using (var db = AcadDatabase.Active())
      {
        db.Viewports.Add(new ViewportTableRecord() { Name = "NewViewport" });
        var ok = Assert.Table<ViewportTable>(db.Database, table => table.Has("NewViewport"));
        
        Console.WriteLine("Test result TestAddViewportTableRecord: " + (ok ? "PASSED" : "FAILED"));
      }
    }
  }
}
