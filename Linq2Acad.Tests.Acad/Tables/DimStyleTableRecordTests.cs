using System;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;

namespace Linq2Acad.Tests
{
  ///////////////////////////////////////////
  // This code is automatically generated ///
  ///////////////////////////////////////////
  public class DimStyleTableRecordTests
  {
    [CommandMethod("TestCreateDimStyleTableRecord")]
    public void TestCreateDimStyleTableRecord()
    {
      using (var db = AcadDatabase.Active())
      {
        var newDimStyle = db.DimStyles.Create("NewDimStyle");
        var ok = Assert.Table<DimStyleTable>(db.Database, table => table.Has("NewDimStyle"));
        
        Console.WriteLine("Test result TestCreateDimStyleTableRecord: " + (ok ? "PASSED" : "FAILED"));
      }
    }

    [CommandMethod("TestAddDimStyleTableRecord")]
    public void TestAddDimStyleTableRecord()
    {
      using (var db = AcadDatabase.Active())
      {
        db.DimStyles.Add(new DimStyleTableRecord() { Name = "NewDimStyle" });
        var ok = Assert.Table<DimStyleTable>(db.Database, table => table.Has("NewDimStyle"));
        
        Console.WriteLine("Test result TestAddDimStyleTableRecord: " + (ok ? "PASSED" : "FAILED"));
      }
    }
  }
}
