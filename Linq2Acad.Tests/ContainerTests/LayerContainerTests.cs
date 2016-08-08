using System;
using System.Linq;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  public partial class LayerContainerTests
  {
    [AcadTest("CreateLayer")]
    public void CreateLayer()
    {
      using (var db = AcadDatabase.Active())
      {
        var newLayer = db.Layers.Create("NewLayer");

        Assert.Table(db.Database, db.Database.LayerTableId, table => table.Has("NewLayer"),
                     "LayerTable does not contain an element with name 'NewLayer'");
        Assert.TableIDs(db.Database, db.Database.LayerTableId, ids => ids.Any(id => id == newLayer.ObjectId),
                        "LayerTable does not contain the newly created element");
      }
    }

    [AcadTest("AddLayer")]
    public void AddLayer()
    {
      using (var db = AcadDatabase.Active())
      {
        var newElement = new LayerTableRecord() { Name = "NewLayer" };
        db.Layers.Add(newElement);
          
        Assert.Table(db.Database, db.Database.LayerTableId, table => table.Has("NewLayer"),
                     "LayerTable does not contain an element with name 'NewLayer'");
      }
    }
  }
}
