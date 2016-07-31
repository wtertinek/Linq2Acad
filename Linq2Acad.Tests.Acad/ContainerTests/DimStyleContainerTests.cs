using System;
using System.Linq;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  public class DimStyleContainerTests
  {
    [CommandMethod("TestCreateDimStyle")]
    public void TestCreateDimStyle()
    {
      var notifier = new Notification("TestCreateDimStyle");

      try
      {
        using (var db = AcadDatabase.Active())
        {
          var newDimStyle = db.DimStyles.Create("NewDimStyle");
          
          var ok = Check.Table(db.Database, table => table.Has("NewDimStyle"));
          if (!ok) { notifier.TestFailed("DimStyleTable does not contain an element with name 'NewDimStyle'"); return; }

          ok = Check.DictionaryIDs(db.Database, ids => ids.Any(id => id == newDimStyle.ObjectId));
          if (!ok) { notifier.TestFailed("DimStyleTable does not contain the newly created element"); return; }
        }
      }
      catch (System.Exception e)
      {
        notifier.TestFailed(e);
      }

      notifier.TestPassed();
    }

    [CommandMethod("TestAddDimStyle")]
    public void TestAddDimStyle()
    {
      var notifier = new Notification("TestAddDimStyle");

      try
      {
        using (var db = AcadDatabase.Active())
        {
          var newElement = new DimStyleTableRecord() { Name = "NewDimStyle" };
          db.DimStyles.Add(newElement);
          
          var ok = Check.Table(db.Database, table => table.Has("NewDimStyle"));
          if (!ok) { notifier.TestFailed("DimStyleTable does not contain an element with name 'NewDimStyle'"); return; }
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
