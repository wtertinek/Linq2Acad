using System;
using System.Linq;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  [AcadTestClass("DimStyleContainerTests")]
  public partial class DimStyleContainerTests
  {
    [AcadTestMethod("TestCreateDimStyle")]
    public void CreateDimStyle()
    {
      using (var db = AcadDatabase.Active())
      {
        var newDimStyle = db.DimStyles.Create("NewDimStyle");

        Assert.Table(db.Database, db.Database.DimStyleTableId, table => table.Has("NewDimStyle"),
                     "DimStyleTable does not contain an element with name 'NewDimStyle'");
        Assert.TableIDs(db.Database, db.Database.DimStyleTableId, ids => ids.Any(id => id == newDimStyle.ObjectId),
                        "DimStyleTable does not contain the newly created element");
      }
    }

    [AcadTestMethod("TestAddDimStyle")]
    public void AddDimStyle()
    {
      using (var db = AcadDatabase.Active())
      {
        var newElement = new DimStyleTableRecord() { Name = "NewDimStyle" };
        db.DimStyles.Add(newElement);
          
        Assert.Table(db.Database, db.Database.DimStyleTableId, table => table.Has("NewDimStyle"),
                     "DimStyleTable does not contain an element with name 'NewDimStyle'");
      }
    }
  }
}
