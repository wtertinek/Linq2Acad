using System;
using System.Linq;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  public partial class UcsContainerTests
  {
    [AcadTest("CreateUcs")]
    public void CreateUcs()
    {
      var newId = ObjectId.Null;

      using (var db = AcadDatabase.Active())
      {
        var newUcs = db.Ucss.Create("NewUcs");
        newId = newUcs.ObjectId;
      }

      Assert.That.UcsTable.Contains("NewUcs");
      Assert.That.UcsTable.Contains(newId);
    }

    [AcadTest("AddUcs")]
    public void AddUcs()
    {
      var newId = ObjectId.Null;

      using (var db = AcadDatabase.Active())
      {
        var newUcs = new UcsTableRecord() { Name = "NewUcs" };
        db.Ucss.Add(newUcs);
        newId = newUcs.ObjectId;
      }

      Assert.That.UcsTable.Contains(newId);
    }
  }
}
