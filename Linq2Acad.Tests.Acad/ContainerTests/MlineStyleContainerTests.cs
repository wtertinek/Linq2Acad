using System;
using System.Linq;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  public class MlineStyleContainerTests
  {
    [CommandMethod("TestCreateMlineStyle")]
    public void TestCreateMlineStyle()
    {
      var notifier = new Notification("TestCreateMlineStyle");

      try
      {
        using (var db = AcadDatabase.Active())
        {
          var newMlineStyle = db.MlineStyles.Create("NewMlineStyle");

          var ok = Check.Dictionary(db.Database, db.Database.MLStyleDictionaryId, dict => dict.Contains("NewMlineStyle"));
          if (!ok) { notifier.TestFailed("MlineStyle dictionary does not contain an element with name 'NewMlineStyle'"); return; }

          ok = Check.DictionaryIDs(db.Database, db.Database.MLStyleDictionaryId, ids => ids.Any(id => id == newMlineStyle.ObjectId));
          if (!ok) { notifier.TestFailed("MlineStyle dictionary does not contain the newly created element"); return; }
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
