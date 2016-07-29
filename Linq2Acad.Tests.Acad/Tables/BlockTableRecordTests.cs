using System;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;

namespace Linq2Acad.Tests
{
  ///////////////////////////////////////////
  // This code is automatically generated ///
  ///////////////////////////////////////////
  public class BlockTableRecordTests
  {
    [CommandMethod("TestCreateBlockTableRecord")]
    public void TestCreateBlockTableRecord()
    {
      using (var db = AcadDatabase.Active())
      {
        var newBlock = db.Blocks.Create("NewBlock");
        var ok = Assert.Table<BlockTable>(db.Database, table => table.Has("NewBlock"));
        
        Console.WriteLine("Test result TestCreateBlockTableRecord: " + (ok ? "PASSED" : "FAILED"));
      }
    }

    [CommandMethod("TestAddBlockTableRecord")]
    public void TestAddBlockTableRecord()
    {
      using (var db = AcadDatabase.Active())
      {
        db.Blocks.Add(new BlockTableRecord() { Name = "NewBlock" });
        var ok = Assert.Table<BlockTable>(db.Database, table => table.Has("NewBlock"));
        
        Console.WriteLine("Test result TestAddBlockTableRecord: " + (ok ? "PASSED" : "FAILED"));
      }
    }
  }
}
