using System;
using System.Linq;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  public partial class LinetypeContainerTests
  {
    [AcadTest("CreateLinetype")]
    public void CreateLinetype()
    {
      var newId = ObjectId.Null;

      using (var db = AcadDatabase.Active())
      {
        var newLinetype = db.Linetypes.Create("NewLinetype");
        newId = newLinetype.ObjectId;
      }

      AcadAssert.That.LinetypeTable.Contains("NewLinetype");
      AcadAssert.That.LinetypeTable.Contains(newId);
    }

    [AcadTest("AddLinetype")]
    public void AddLinetype()
    {
      var newId = ObjectId.Null;

      using (var db = AcadDatabase.Active())
      {
        var newLinetype = new LinetypeTableRecord() { Name = "NewLinetype" };
        db.Linetypes.Add(newLinetype);
        newId = newLinetype.ObjectId;
      }

      AcadAssert.That.LinetypeTable.Contains(newId);
    }
  }
}
