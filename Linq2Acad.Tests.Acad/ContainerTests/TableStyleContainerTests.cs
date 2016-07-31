using System;
using System.Linq;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  public class TableStyleContainerTests
  {
    [CommandMethod("TestCreateTableStyle")]
    public void TestCreateTableStyle()
    {
      var notifier = new Notification("TestCreateTableStyle");

      try
      {
        using (var db = AcadDatabase.Active())
        {
          var newTableStyle = db.TableStyles.Create("NewTableStyle");
          
          var ok = Check.Dictionary(db.Database, dict => dict.Contains("NewTableStyle"));
          if (!ok) { notifier.TestFailed("TableStyle dictionary does not contain an element with name 'NewTableStyle'"); return; }

          ok = Check.DictionaryIDs(db.Database, ids => ids.Any(id => id == newTableStyle.ObjectId));
          if (!ok) { notifier.TestFailed("TableStyle dictionary does not contain the newly created element"); return; }
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
