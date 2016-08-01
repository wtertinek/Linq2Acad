using System;
using System.Linq;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  public class GroupContainerTests
  {
    [CommandMethod("TestCreateGroup")]
    public void TestCreateGroup()
    {
      var notifier = new Notification("TestCreateGroup");

      try
      {
        using (var db = AcadDatabase.Active())
        {
          var newGroup = db.Groups.Create("NewGroup");

          var ok = Check.Dictionary(db.Database, db.Database.GroupDictionaryId, dict => dict.Contains("NewGroup"));
          if (!ok) { notifier.TestFailed("Group dictionary does not contain an element with name 'NewGroup'"); return; }

          ok = Check.DictionaryIDs(db.Database, db.Database.GroupDictionaryId, ids => ids.Any(id => id == newGroup.ObjectId));
          if (!ok) { notifier.TestFailed("Group dictionary does not contain the newly created element"); return; }
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
