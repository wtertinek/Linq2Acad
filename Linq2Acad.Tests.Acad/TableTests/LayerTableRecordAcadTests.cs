using System;
using System.Linq;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  public class LayerTableRecordAcadTests
  {
    [CommandMethod("TestCreateLayerTableRecord")]
    public void TestCreateLayerTableRecord()
    {
      var notifier = new Notification("TestCreateLayerTableRecord");

      try
      {
        using (var db = AcadDatabase.Active())
        {
          var newLayer = db.Layers.Create("NewLayer");
          
          var ok = Check.Table(db.Database, table => table.Has("NewLayer"));
          if (!ok) { notifier.TestFailed("LayerTable does not contain an element with name 'NewLayer'"); return; }

          ok = Check.DictionaryIDs(db.Database, ids => ids.Any(id => id == newLayer.ObjectId));
          if (!ok) { notifier.TestFailed("LayerTable does not contain the newly created element"); return; }
        }
      }
      catch (System.Exception e)
      {
        notifier.TestFailed(e);
      }

      notifier.TestPassed();
    }

    [CommandMethod("TestAddLayerTableRecord")]
    public void TestAddLayerTableRecord()
    {
      var notifier = new Notification("TestCreateLayerTableRecord");

      try
      {
        using (var db = AcadDatabase.Active())
        {
          var newElement = new LayerTableRecord() { Name = "NewLayer" };
          db.Layers.Add(newElement);
          
          var ok = Check.Table(db.Database, table => table.Has("NewLayer"));
          if (!ok) { notifier.TestFailed("LayerTable does not contain an element with name 'NewLayer'"); return; }
        }
      }
      catch (System.Exception e)
      {
        notifier.TestFailed(e);
      }
    
      notifier.TestPassed();
    }
  }
}
