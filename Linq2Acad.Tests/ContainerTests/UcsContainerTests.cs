using System;
using System.Linq;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  public partial class UcsContainerTests
  {
    [AcadTest("CreateUcs")]
    public void CreateUcs()
    {
      using (var db = AcadDatabase.Active())
      {
        var newUcs = db.Ucss.Create("NewUcs");

        Assert.Table(db.Database, db.Database.UcsTableId, table => table.Has("NewUcs"),
                     "UcsTable does not contain an element with name 'NewUcs'");
        Assert.TableIDs(db.Database, db.Database.UcsTableId, ids => ids.Any(id => id == newUcs.ObjectId),
                        "UcsTable does not contain the newly created element");
      }
    }

    [AcadTest("AddUcs")]
    public void AddUcs()
    {
      using (var db = AcadDatabase.Active())
      {
        var newElement = new UcsTableRecord() { Name = "NewUcs" };
        db.Ucss.Add(newElement);
          
        Assert.Table(db.Database, db.Database.UcsTableId, table => table.Has("NewUcs"),
                     "UcsTable does not contain an element with name 'NewUcs'");
      }
    }
  }
}
