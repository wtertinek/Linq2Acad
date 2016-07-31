using System;
using System.Linq;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  public class ViewportTableRecordAcadTests
  {
    [CommandMethod("TestCreateViewportTableRecord")]
    public void TestCreateViewportTableRecord()
    {
      var notifier = new Notification("TestCreateViewportTableRecord");

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

    [CommandMethod("TestAddViewportTableRecord")]
    public void TestAddViewportTableRecord()
    {
      var notifier = new Notification("TestCreateViewportTableRecord");

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
