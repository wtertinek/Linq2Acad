using System;
using System.Linq;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  public class DBVisualStyleContainerTests
  {
    [CommandMethod("TestCreateDBVisualStyle")]
    public void TestCreateDBVisualStyle()
    {
      var notifier = new Notification("TestCreateDBVisualStyle");

      try
      {
        using (var db = AcadDatabase.Active())
        {
          var newDBVisualStyle = db.DBVisualStyles.Create("NewDBVisualStyle");
          
          var ok = Check.Dictionary(db.Database, dict => dict.Contains("NewDBVisualStyle"));
          if (!ok) { notifier.TestFailed("DBVisualStyle dictionary does not contain an element with name 'NewDBVisualStyle'"); return; }

          ok = Check.DictionaryIDs(db.Database, ids => ids.Any(id => id == newDBVisualStyle.ObjectId));
          if (!ok) { notifier.TestFailed("DBVisualStyle dictionary does not contain the newly created element"); return; }
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
