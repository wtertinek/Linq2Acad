using System;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;

namespace Linq2Acad.Tests
{
  ///////////////////////////////////////////
  // This code is automatically generated ///
  ///////////////////////////////////////////
  public class LayerTableRecordTests
  {
    [CommandMethod("TestCreateLayerTableRecord")]
    public void TestCreateLayerTableRecord()
    {
      using (var db = AcadDatabase.Active())
      {
        var newLayer = db.Layers.Create("NewLayer");
        var ok = Assert.Table<LayerTable>(db.Database, table => table.Has("NewLayer"));
        
        Console.WriteLine("Test result TestCreateLayerTableRecord: " + (ok ? "PASSED" : "FAILED"));
      }
    }

    [CommandMethod("TestAddLayerTableRecord")]
    public void TestAddLayerTableRecord()
    {
      using (var db = AcadDatabase.Active())
      {
        db.Layers.Add(new LayerTableRecord() { Name = "NewLayer" });
        var ok = Assert.Table<LayerTable>(db.Database, table => table.Has("NewLayer"));
        
        Console.WriteLine("Test result TestAddLayerTableRecord: " + (ok ? "PASSED" : "FAILED"));
      }
    }
  }
}
