using System;
using System.Linq;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  public class UcsTableRecordAcadTests
  {
    [CommandMethod("TestCreateUcsTableRecord")]
    public void TestCreateUcsTableRecord()
    {
      var notifier = new Notification("TestCreateUcsTableRecord");

      try
      {
        using (var db = AcadDatabase.Active())
        {
          var newUcs = db.Ucss.Create("NewUcs");
          
          var ok = Check.Table(db.Database, table => table.Has("NewUcs"));
          if (!ok) { notifier.TestFailed("UcsTable does not contain an element with name 'NewUcs'"); return; }

          ok = Check.DictionaryIDs(db.Database, ids => ids.Any(id => id == newUcs.ObjectId));
          if (!ok) { notifier.TestFailed("UcsTable does not contain the newly created element"); return; }
        }
      }
      catch (System.Exception e)
      {
        notifier.TestFailed(e);
      }

      notifier.TestPassed();
    }

    [CommandMethod("TestAddUcsTableRecord")]
    public void TestAddUcsTableRecord()
    {
      var notifier = new Notification("TestCreateUcsTableRecord");

      try
      {
        using (var db = AcadDatabase.Active())
        {
          var newElement = new UcsTableRecord() { Name = "NewUcs" };
          db.Ucss.Add(newElement);
          
          var ok = Check.Table(db.Database, table => table.Has("NewUcs"));
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
