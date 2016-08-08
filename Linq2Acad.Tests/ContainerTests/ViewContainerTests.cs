using System;
using System.Linq;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  public partial class ViewContainerTests
  {
    [AcadTest("CreateView")]
    public void CreateView()
    {
      using (var db = AcadDatabase.Active())
      {
        var newView = db.Views.Create("NewView");

        Assert.Table(db.Database, db.Database.ViewTableId, table => table.Has("NewView"),
                     "ViewTable does not contain an element with name 'NewView'");
        Assert.TableIDs(db.Database, db.Database.ViewTableId, ids => ids.Any(id => id == newView.ObjectId),
                        "ViewTable does not contain the newly created element");
      }
    }

    [AcadTest("AddView")]
    public void AddView()
    {
      using (var db = AcadDatabase.Active())
      {
        var newElement = new ViewTableRecord() { Name = "NewView" };
        db.Views.Add(newElement);
          
        Assert.Table(db.Database, db.Database.ViewTableId, table => table.Has("NewView"),
                     "ViewTable does not contain an element with name 'NewView'");
      }
    }
  }
}
