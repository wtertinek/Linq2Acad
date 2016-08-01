using System;
using System.Linq;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  public class MLeaderStyleContainerTests
  {
    [CommandMethod("TestCreateMLeaderStyle")]
    public void TestCreateMLeaderStyle()
    {
      var notifier = new Notification("TestCreateMLeaderStyle");

      try
      {
        using (var db = AcadDatabase.Active())
        {
          var newMLeaderStyle = db.MLeaderStyles.Create("NewMLeaderStyle");

          var ok = Check.Dictionary(db.Database, db.Database.MLeaderStyleDictionaryId, dict => dict.Contains("NewMLeaderStyle"));
          if (!ok) { notifier.TestFailed("MLeaderStyle dictionary does not contain an element with name 'NewMLeaderStyle'"); return; }

          ok = Check.DictionaryIDs(db.Database, db.Database.MLeaderStyleDictionaryId, ids => ids.Any(id => id == newMLeaderStyle.ObjectId));
          if (!ok) { notifier.TestFailed("MLeaderStyle dictionary does not contain the newly created element"); return; }
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
