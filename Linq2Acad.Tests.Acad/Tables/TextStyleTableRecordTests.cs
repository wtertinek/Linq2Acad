using System;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;

namespace Linq2Acad.Tests
{
  ///////////////////////////////////////////
  // This code is automatically generated ///
  ///////////////////////////////////////////
  public class TextStyleTableRecordTests
  {
    [CommandMethod("TestCreateTextStyleTableRecord")]
    public void TestCreateTextStyleTableRecord()
    {
      using (var db = AcadDatabase.Active())
      {
        var newTextStyle = db.TextStyles.Create("NewTextStyle");
        var ok = Assert.Table<TextStyleTable>(db.Database, table => table.Has("NewTextStyle"));
        
        Console.WriteLine("Test result TestCreateTextStyleTableRecord: " + (ok ? "PASSED" : "FAILED"));
      }
    }

    [CommandMethod("TestAddTextStyleTableRecord")]
    public void TestAddTextStyleTableRecord()
    {
      using (var db = AcadDatabase.Active())
      {
        db.TextStyles.Add(new TextStyleTableRecord() { Name = "NewTextStyle" });
        var ok = Assert.Table<TextStyleTable>(db.Database, table => table.Has("NewTextStyle"));
        
        Console.WriteLine("Test result TestAddTextStyleTableRecord: " + (ok ? "PASSED" : "FAILED"));
      }
    }
  }
}
