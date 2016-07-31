using System;
using System.Linq;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  public class DimStyleTableRecordAcadTests
  {
    [CommandMethod("TestCreateDimStyleTableRecord")]
    public void TestCreateDimStyleTableRecord()
    {
      var notifier = new Notification("TestCreateDimStyleTableRecord");

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

    [CommandMethod("TestAddDimStyleTableRecord")]
    public void TestAddDimStyleTableRecord()
    {
      var notifier = new Notification("TestCreateDimStyleTableRecord");

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
