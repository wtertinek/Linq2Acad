using System;
using System.Linq;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  public class TextStyleContainerTests
  {
    [CommandMethod("TestCreateTextStyle")]
    public void TestCreateTextStyle()
    {
      var notifier = new Notification("TestCreateTextStyle");

      try
      {
        using (var db = AcadDatabase.Active())
        {
          var newTextStyle = db.TextStyles.Create("NewTextStyle");

          var ok = Check.Table(db.Database, db.Database.TextStyleTableId, table => table.Has("NewTextStyle"));
          if (!ok) { notifier.TestFailed("TextStyleTable does not contain an element with name 'NewTextStyle'"); return; }

          ok = Check.TableIDs(db.Database, db.Database.TextStyleTableId, ids => ids.Any(id => id == newTextStyle.ObjectId));
          if (!ok) { notifier.TestFailed("TextStyleTable does not contain the newly created element"); return; }
        }
      }
      catch (System.Exception e)
      {
        notifier.TestFailed(e);
      }

      notifier.TestPassed();
    }

    [CommandMethod("TestAddTextStyle")]
    public void TestAddTextStyle()
    {
      var notifier = new Notification("TestAddTextStyle");

      try
      {
        using (var db = AcadDatabase.Active())
        {
          var newElement = new TextStyleTableRecord() { Name = "NewTextStyle" };
          db.TextStyles.Add(newElement);

          var ok = Check.Table(db.Database, db.Database.TextStyleTableId, table => table.Has("NewTextStyle"));
          if (!ok) { notifier.TestFailed("TextStyleTable does not contain an element with name 'NewTextStyle'"); return; }
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
