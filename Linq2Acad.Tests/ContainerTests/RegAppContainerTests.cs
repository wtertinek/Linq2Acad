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
      var newId = ObjectId.Null;

      using (var db = AcadDatabase.Active())
      {
        var newRegApp = db.RegApps.Create("NewRegApp");
        newId = newRegApp.ObjectId;
      }

      AcadAssert.That.RegAppTable.Contains("NewRegApp");
      AcadAssert.That.RegAppTable.Contains(newId);
    }

    [AcadTest("AddRegApp")]
    public void AddRegApp()
    {
      var newId = ObjectId.Null;

      using (var db = AcadDatabase.Active())
      {
        var newRegApp = new RegAppTableRecord() { Name = "NewRegApp" };
        db.RegApps.Add(newRegApp);
        newId = newRegApp.ObjectId;
      }

      AcadAssert.That.RegAppTable.Contains(newId);
    }
  }
}
