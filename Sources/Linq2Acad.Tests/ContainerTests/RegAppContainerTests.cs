using System;
using System.Linq;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  public class RegAppContainerTests
  {
    [AcadTest]
    public void TestCreateRegApp()
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

    [AcadTest]
    public void TestAddRegApp()
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
