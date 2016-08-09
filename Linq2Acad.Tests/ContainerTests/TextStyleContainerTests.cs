using System;
using System.Linq;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  public partial class TextStyleContainerTests
  {
    [AcadTest("CreateTextStyle")]
    public void CreateTextStyle()
    {
      var newId = ObjectId.Null;

      using (var db = AcadDatabase.Active())
      {
        var newTextStyle = db.TextStyles.Create("NewTextStyle");
        newId = newTextStyle.ObjectId;
      }

      Assert.That.TextStyleTable.Contains("NewTextStyle");
      Assert.That.TextStyleTable.Contains(newId);
    }

    [AcadTest("AddTextStyle")]
    public void AddTextStyle()
    {
      var newId = ObjectId.Null;

      using (var db = AcadDatabase.Active())
      {
        var newTextStyle = new TextStyleTableRecord() { Name = "NewTextStyle" };
        db.TextStyles.Add(newTextStyle);
        newId = newTextStyle.ObjectId;
      }

      Assert.That.TextStyleTable.Contains(newId);
    }
  }
}
