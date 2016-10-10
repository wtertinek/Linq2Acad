using System;
using System.Linq;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  public partial class ViewportContainerTests
  {
    [AcadTest("CreateViewport")]
    public void CreateViewport()
    {
      var newId = ObjectId.Null;

      using (var db = AcadDatabase.Active())
      {
        var newViewport = db.Viewports.Create("NewViewport");
        newId = newViewport.ObjectId;
      }

      AcadAssert.That.ViewportTable.Contains("NewViewport");
      AcadAssert.That.ViewportTable.Contains(newId);
    }

    [AcadTest("AddViewport")]
    public void AddViewport()
    {
      var newId = ObjectId.Null;

      using (var db = AcadDatabase.Active())
      {
        var newViewport = new ViewportTableRecord() { Name = "NewViewport" };
        db.Viewports.Add(newViewport);
        newId = newViewport.ObjectId;
      }

      AcadAssert.That.ViewportTable.Contains(newId);
    }
  }
}
