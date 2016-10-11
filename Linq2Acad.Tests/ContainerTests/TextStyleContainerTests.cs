using System;
using System.Linq;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  public class TextStyleContainerTests
  {
    [AcadTest]
    public void TestCreateTextStyle()
    {
      var newId = ObjectId.Null;

      using (var db = AcadDatabase.Active())
      {
        var newTextStyle = db.TextStyles.Create("NewTextStyle");
        newId = newTextStyle.ObjectId;
      }

      AcadAssert.That.TextStyleTable.Contains("NewTextStyle");
      AcadAssert.That.TextStyleTable.Contains(newId);
    }

    [AcadTest]
    public void TestAddTextStyle()
    {
      var newId = ObjectId.Null;

      using (var db = AcadDatabase.Active())
      {
        var newTextStyle = new TextStyleTableRecord() { Name = "NewTextStyle" };
        db.TextStyles.Add(newTextStyle);
        newId = newTextStyle.ObjectId;
      }

      AcadAssert.That.TextStyleTable.Contains(newId);
    }
  }
}
