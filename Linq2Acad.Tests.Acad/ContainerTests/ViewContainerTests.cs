using System;
using System.Linq;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  public class ViewContainerTests
  {
    [CommandMethod("TestCreateView")]
    public void TestCreateView()
    {
      var notifier = new Notification("TestCreateView");

      try
      {
        using (var db = AcadDatabase.Active())
        {
          var newView = db.Views.Create("NewView");

          var ok = Check.Table(db.Database, db.Database.ViewportTableId, table => table.Has("NewView"));
          if (!ok) { notifier.TestFailed("ViewTable does not contain an element with name 'NewView'"); return; }

          ok = Check.TableIDs(db.Database, db.Database.ViewportTableId, ids => ids.Any(id => id == newView.ObjectId));
          if (!ok) { notifier.TestFailed("ViewTable does not contain the newly created element"); return; }
        }
      }
      catch (System.Exception e)
      {
        notifier.TestFailed(e);
      }

      notifier.TestPassed();
    }

    [CommandMethod("TestAddView")]
    public void TestAddView()
    {
      var notifier = new Notification("TestAddView");

      try
      {
        using (var db = AcadDatabase.Active())
        {
          var newElement = new ViewTableRecord() { Name = "NewView" };
          db.Views.Add(newElement);

          var ok = Check.Table(db.Database, db.Database.ViewportTableId, table => table.Has("NewView"));
          if (!ok) { notifier.TestFailed("ViewTable does not contain an element with name 'NewView'"); return; }
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
