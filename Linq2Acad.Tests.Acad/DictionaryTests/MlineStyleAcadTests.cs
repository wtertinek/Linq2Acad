using System;
using System.Linq;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  public class MlineStyleAcadTests
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
          
          var ok = Check.Dictionary(db.Database, dict => dict.Contains("NewMlineStyle"));
          if (!ok) { notifier.TestFailed("{ContainerClassName} does not contain an element with name 'NewMlineStyle'"); return; }

          ok = Check.DictionaryIDs(db.Database, ids => ids.Any(id => id == newMlineStyle.ObjectId));
          if (!ok) { notifier.TestFailed("{ContainerClassName} does not contain the newly created element"); return; }
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
