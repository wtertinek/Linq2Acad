using System;
using System.Linq;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  public partial class RegAppContainerTests
  {
    [AcadTest("CreateRegApp")]
    public void CreateRegApp()
    {
      using (var db = AcadDatabase.Active())
      {
        var newRegApp = db.RegApps.Create("NewRegApp");

        Assert.Table(db.Database, db.Database.RegAppTableId, table => table.Has("NewRegApp"),
                     "RegAppTable does not contain an element with name 'NewRegApp'");
        Assert.TableIDs(db.Database, db.Database.RegAppTableId, ids => ids.Any(id => id == newRegApp.ObjectId),
                        "RegAppTable does not contain the newly created element");
      }
    }

    [AcadTest("AddRegApp")]
    public void AddRegApp()
    {
      using (var db = AcadDatabase.Active())
      {
        var newElement = new RegAppTableRecord() { Name = "NewRegApp" };
        db.RegApps.Add(newElement);
          
        Assert.Table(db.Database, db.Database.RegAppTableId, table => table.Has("NewRegApp"),
                     "RegAppTable does not contain an element with name 'NewRegApp'");
      }
    }
  }
}
