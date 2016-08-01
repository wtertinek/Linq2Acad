using System;
using System.Linq;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  public class UcsContainerTests
  {
    [CommandMethod("TestCreateUcs")]
    public void TestCreateUcs()
    {
      var notifier = new Notification("TestCreateUcs");

      try
      {
        using (var db = AcadDatabase.Active())
        {
          var newUcs = db.Ucss.Create("NewUcs");

          var ok = Check.Table(db.Database, db.Database.UcsTableId, table => table.Has("NewUcs"));
          if (!ok) { notifier.TestFailed("UcsTable does not contain an element with name 'NewUcs'"); return; }

          ok = Check.TableIDs(db.Database, db.Database.UcsTableId, ids => ids.Any(id => id == newUcs.ObjectId));
          if (!ok) { notifier.TestFailed("UcsTable does not contain the newly created element"); return; }
        }
      }
      catch (System.Exception e)
      {
        notifier.TestFailed(e);
      }

      notifier.TestPassed();
    }

    [CommandMethod("TestAddUcs")]
    public void TestAddUcs()
    {
      var notifier = new Notification("TestAddUcs");

      try
      {
        using (var db = AcadDatabase.Active())
        {
          var newElement = new UcsTableRecord() { Name = "NewUcs" };
          db.Ucss.Add(newElement);

          var ok = Check.Table(db.Database, db.Database.UcsTableId, table => table.Has("NewUcs"));
          if (!ok) { notifier.TestFailed("UcsTable does not contain an element with name 'NewUcs'"); return; }
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
