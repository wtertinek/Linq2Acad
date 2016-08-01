using System;
using System.Linq;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  public class LayoutContainerTests
  {
    [CommandMethod("TestCreateLayout")]
    public void TestCreateLayout()
    {
      var notifier = new Notification("TestCreateLayout");

      try
      {
        using (var db = AcadDatabase.Active())
        {
          var newLayout = db.Layouts.Create("NewLayout");

          var ok = Check.Dictionary(db.Database, db.Database.LayoutDictionaryId, dict => dict.Contains("NewLayout"));
          if (!ok) { notifier.TestFailed("Layout dictionary does not contain an element with name 'NewLayout'"); return; }

          ok = Check.DictionaryIDs(db.Database, db.Database.LayoutDictionaryId, ids => ids.Any(id => id == newLayout.ObjectId));
          if (!ok) { notifier.TestFailed("Layout dictionary does not contain the newly created element"); return; }
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
