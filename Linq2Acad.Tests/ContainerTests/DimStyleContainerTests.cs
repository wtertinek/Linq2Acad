using System;
using System.Linq;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  public partial class DimStyleContainerTests
  {
    [AcadTest("CreateDimStyle")]
    public void CreateDimStyle()
    {
      var newId = ObjectId.Null;

      using (var db = AcadDatabase.Active())
      {
        var newDimStyle = db.DimStyles.Create("NewDimStyle");
        newId = newDimStyle.ObjectId;
      }

      Assert.That.DimStyleTable.Contains("NewDimStyle");
      Assert.That.DimStyleTable.Contains(newId);
    }

    [AcadTest("AddDimStyle")]
    public void AddDimStyle()
    {
      var newId = ObjectId.Null;

      using (var db = AcadDatabase.Active())
      {
        var newDimStyle = new DimStyleTableRecord() { Name = "NewDimStyle" };
        db.DimStyles.Add(newDimStyle);
        newId = newDimStyle.ObjectId;
      }

      Assert.That.DimStyleTable.Contains(newId);
    }
  }
}
