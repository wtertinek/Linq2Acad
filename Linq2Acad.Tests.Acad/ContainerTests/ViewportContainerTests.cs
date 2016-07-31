using System;
using System.Linq;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  public class ViewportContainerTests
  {
    [CommandMethod("TestCreateViewport")]
    public void TestCreateViewport()
    {
      var notifier = new Notification("TestCreateViewport");

      try
      {
        using (var db = AcadDatabase.Active())
        {
          var newViewport = db.Viewports.Create("NewViewport");
          
          var ok = Check.Table(db.Database, table => table.Has("NewViewport"));
          if (!ok) { notifier.TestFailed("ViewportTable does not contain an element with name 'NewViewport'"); return; }

          ok = Check.DictionaryIDs(db.Database, ids => ids.Any(id => id == newViewport.ObjectId));
          if (!ok) { notifier.TestFailed("ViewportTable does not contain the newly created element"); return; }
        }
      }
      catch (System.Exception e)
      {
        notifier.TestFailed(e);
      }

      notifier.TestPassed();
    }

    [CommandMethod("TestAddViewport")]
    public void TestAddViewport()
    {
      var notifier = new Notification("TestAddViewport");

      try
      {
        using (var db = AcadDatabase.Active())
        {
          var newElement = new ViewportTableRecord() { Name = "NewViewport" };
          db.Viewports.Add(newElement);
          
          var ok = Check.Table(db.Database, table => table.Has("NewViewport"));
          if (!ok) { notifier.TestFailed("ViewportTable does not contain an element with name 'NewViewport'"); return; }
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
