using System;
using System.Linq;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  [AcadTestClass("TextStyleContainerTests")]
  public partial class TextStyleContainerTests
  {
    [AcadTestMethod("TestCreateTextStyle")]
    public void CreateTextStyle()
    {
      using (var db = AcadDatabase.Active())
      {
        var newTextStyle = db.TextStyles.Create("NewTextStyle");

        Assert.Table(db.Database, db.Database.TextStyleTableId, table => table.Has("NewTextStyle"),
                     "TextStyleTable does not contain an element with name 'NewTextStyle'");
        Assert.TableIDs(db.Database, db.Database.TextStyleTableId, ids => ids.Any(id => id == newTextStyle.ObjectId),
                        "TextStyleTable does not contain the newly created element");
      }
    }

    [AcadTestMethod("TestAddTextStyle")]
    public void AddTextStyle()
    {
      using (var db = AcadDatabase.Active())
      {
        var newElement = new TextStyleTableRecord() { Name = "NewTextStyle" };
        db.TextStyles.Add(newElement);
          
        Assert.Table(db.Database, db.Database.TextStyleTableId, table => table.Has("NewTextStyle"),
                     "TextStyleTable does not contain an element with name 'NewTextStyle'");
      }
    }
  }
}
