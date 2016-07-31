using System;
using System.Linq;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  public class RegAppContainerTests
  {
    [CommandMethod("TestCreateRegApp")]
    public void TestCreateRegApp()
    {
      var notifier = new Notification("TestCreateRegApp");

      try
      {
        using (var db = AcadDatabase.Active())
        {
          var newRegApp = db.RegApps.Create("NewRegApp");
          
          var ok = Check.Table(db.Database, table => table.Has("NewRegApp"));
          if (!ok) { notifier.TestFailed("RegAppTable does not contain an element with name 'NewRegApp'"); return; }

          ok = Check.DictionaryIDs(db.Database, ids => ids.Any(id => id == newRegApp.ObjectId));
          if (!ok) { notifier.TestFailed("RegAppTable does not contain the newly created element"); return; }
        }
      }
      catch (System.Exception e)
      {
        notifier.TestFailed(e);
      }

      notifier.TestPassed();
    }

    [CommandMethod("TestAddRegApp")]
    public void TestAddRegApp()
    {
      var notifier = new Notification("TestAddRegApp");

      try
      {
        using (var db = AcadDatabase.Active())
        {
          var newElement = new RegAppTableRecord() { Name = "NewRegApp" };
          db.RegApps.Add(newElement);
          
          var ok = Check.Table(db.Database, table => table.Has("NewRegApp"));
          if (!ok) { notifier.TestFailed("RegAppTable does not contain an element with name 'NewRegApp'"); return; }
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
