using System;
using System.Linq;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  public partial class UcsContainerTests
  {
    [AcadTest]
    public void CreateUcs()
    {
      var newId = ObjectId.Null;

      using (var db = AcadDatabase.Active())
      {
        var newUcs = db.Ucss.Create("NewUcs");
        newId = newUcs.ObjectId;
      }

      AcadAssert.That.UcsTable.Contains("NewUcs");
      AcadAssert.That.UcsTable.Contains(newId);
    }

    [AcadTest]
    public void AddUcs()
    {
      var newId = ObjectId.Null;

      using (var db = AcadDatabase.Active())
      {
        var newUcs = new UcsTableRecord() { Name = "NewUcs" };
        db.Ucss.Add(newUcs);
        newId = newUcs.ObjectId;
      }

      AcadAssert.That.UcsTable.Contains(newId);
    }
  }
}
