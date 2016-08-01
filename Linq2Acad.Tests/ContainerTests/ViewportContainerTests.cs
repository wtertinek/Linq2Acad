using System;
using System.Linq;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  [AcadTestClass("ViewportContainerTests")]
  public partial class ViewportContainerTests
  {
    [AcadTestMethod("TestCreateViewport")]
    public void CreateViewport()
    {
      using (var db = AcadDatabase.Active())
      {
        var newViewport = db.Viewports.Create("NewViewport");

        Assert.Table(db.Database, db.Database.ViewportTableId, table => table.Has("NewViewport"),
                     "ViewportTable does not contain an element with name 'NewViewport'");
        Assert.TableIDs(db.Database, db.Database.ViewportTableId, ids => ids.Any(id => id == newViewport.ObjectId),
                        "ViewportTable does not contain the newly created element");
      }
    }

    [AcadTestMethod("TestAddViewport")]
    public void AddViewport()
    {
      using (var db = AcadDatabase.Active())
      {
        var newElement = new ViewportTableRecord() { Name = "NewViewport" };
        db.Viewports.Add(newElement);
          
        Assert.Table(db.Database, db.Database.ViewportTableId, table => table.Has("NewViewport"),
                     "ViewportTable does not contain an element with name 'NewViewport'");
      }
    }
  }
}
