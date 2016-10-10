using System;
using System.Linq;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  public partial class LayerContainerTests
  {
    [AcadTest]
    public void CreateLayer()
    {
      var newId = ObjectId.Null;

      using (var db = AcadDatabase.Active())
      {
        var newLayer = db.Layers.Create("NewLayer");
        newId = newLayer.ObjectId;
      }

      AcadAssert.That.LayerTable.Contains("NewLayer");
      AcadAssert.That.LayerTable.Contains(newId);
    }

    [AcadTest]
    public void AddLayer()
    {
      var newId = ObjectId.Null;

      using (var db = AcadDatabase.Active())
      {
        var newLayer = new LayerTableRecord() { Name = "NewLayer" };
        db.Layers.Add(newLayer);
        newId = newLayer.ObjectId;
      }

      AcadAssert.That.LayerTable.Contains(newId);
    }
  }
}
