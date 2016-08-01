using System;
using System.Linq;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  [AcadTestClass("LinetypeContainerTests")]
  public partial class LinetypeContainerTests
  {
    [AcadTestMethod("TestCreateLinetype")]
    public void CreateLinetype()
    {
      using (var db = AcadDatabase.Active())
      {
        var newLinetype = db.Linetypes.Create("NewLinetype");

        Assert.Table(db.Database, db.Database.LinetypeTableId, table => table.Has("NewLinetype"),
                     "LinetypeTable does not contain an element with name 'NewLinetype'");
        Assert.TableIDs(db.Database, db.Database.LinetypeTableId, ids => ids.Any(id => id == newLinetype.ObjectId),
                        "LinetypeTable does not contain the newly created element");
      }
    }

    [AcadTestMethod("TestAddLinetype")]
    public void AddLinetype()
    {
      using (var db = AcadDatabase.Active())
      {
        var newElement = new LinetypeTableRecord() { Name = "NewLinetype" };
        db.Linetypes.Add(newElement);
          
        Assert.Table(db.Database, db.Database.LinetypeTableId, table => table.Has("NewLinetype"),
                     "LinetypeTable does not contain an element with name 'NewLinetype'");
      }
    }
  }
}
