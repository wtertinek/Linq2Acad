using System;
using System.Linq;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  public class SectionViewStyleContainerTests
  {
    [CommandMethod("TestCreateSectionViewStyle")]
    public void TestCreateSectionViewStyle()
    {
      var notifier = new Notification("TestCreateSectionViewStyle");

      try
      {
        using (var db = AcadDatabase.Active())
        {
          var newSectionViewStyle = db.SectionViewStyles.Create("NewSectionViewStyle");

          var ok = Check.Dictionary(db.Database, db.Database.SectionViewStyleDictionaryId, dict => dict.Contains("NewSectionViewStyle"));
          if (!ok) { notifier.TestFailed("SectionViewStyle dictionary does not contain an element with name 'NewSectionViewStyle'"); return; }

          ok = Check.DictionaryIDs(db.Database, db.Database.SectionViewStyleDictionaryId, ids => ids.Any(id => id == newSectionViewStyle.ObjectId));
          if (!ok) { notifier.TestFailed("SectionViewStyle dictionary does not contain the newly created element"); return; }
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
