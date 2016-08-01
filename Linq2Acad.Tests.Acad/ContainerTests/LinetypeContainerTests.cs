using System;
using System.Linq;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  public class LinetypeContainerTests
  {
    [CommandMethod("TestCreateLinetype")]
    public void TestCreateLinetype()
    {
      var notifier = new Notification("TestCreateLinetype");

      try
      {
        using (var db = AcadDatabase.Active())
        {
          var newLinetype = db.Linetypes.Create("NewLinetype");

          var ok = Check.Table(db.Database, db.Database.LinetypeTableId, table => table.Has("NewLinetype"));
          if (!ok) { notifier.TestFailed("LinetypeTable does not contain an element with name 'NewLinetype'"); return; }

          ok = Check.TableIDs(db.Database, db.Database.LinetypeTableId, ids => ids.Any(id => id == newLinetype.ObjectId));
          if (!ok) { notifier.TestFailed("LinetypeTable does not contain the newly created element"); return; }
        }
      }
      catch (System.Exception e)
      {
        notifier.TestFailed(e);
      }

      notifier.TestPassed();
    }

    [CommandMethod("TestAddLinetype")]
    public void TestAddLinetype()
    {
      var notifier = new Notification("TestAddLinetype");

      try
      {
        using (var db = AcadDatabase.Active())
        {
          var newElement = new LinetypeTableRecord() { Name = "NewLinetype" };
          db.Linetypes.Add(newElement);

          var ok = Check.Table(db.Database, db.Database.LinetypeTableId, table => table.Has("NewLinetype"));
          if (!ok) { notifier.TestFailed("LinetypeTable does not contain an element with name 'NewLinetype'"); return; }
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
