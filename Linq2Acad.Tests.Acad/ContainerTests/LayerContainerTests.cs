using System;
using System.Linq;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  public class LayerContainerTests
  {
    [CommandMethod("TestCreateLayer")]
    public void TestCreateLayer()
    {
      var notifier = new Notification("TestCreateLayer");

      try
      {
        using (var db = AcadDatabase.Active())
        {
          var newLayer = db.Layers.Create("NewLayer");

          var ok = Check.Table(db.Database, db.Database.LayerTableId, table => table.Has("NewLayer"));
          if (!ok) { notifier.TestFailed("LayerTable does not contain an element with name 'NewLayer'"); return; }

          ok = Check.TableIDs(db.Database, db.Database.LayerTableId, ids => ids.Any(id => id == newLayer.ObjectId));
          if (!ok) { notifier.TestFailed("LayerTable does not contain the newly created element"); return; }
        }
      }
      catch (System.Exception e)
      {
        notifier.TestFailed(e);
      }

      notifier.TestPassed();
    }

    [CommandMethod("TestAddLayer")]
    public void TestAddLayer()
    {
      var notifier = new Notification("TestAddLayer");

      try
      {
        using (var db = AcadDatabase.Active())
        {
          var newElement = new LayerTableRecord() { Name = "NewLayer" };
          db.Layers.Add(newElement);

          var ok = Check.Table(db.Database, db.Database.LayerTableId, table => table.Has("NewLayer"));
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
