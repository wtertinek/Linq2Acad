using System;
using System.Linq;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  public partial class MaterialContainerTests
  {
    [AcadTest("CreateMaterial")]
    public void CreateMaterial()
    {
      var newId = ObjectId.Null;

      using (var db = AcadDatabase.Active())
      {
        var newMaterial = db.Materials.Create("NewMaterial");
        newId = newMaterial.ObjectId;
      }

      Assert.That.MaterialDictionary.Contains("NewMaterial");
      Assert.That.MaterialDictionary.Contains(newId);
    }
  }
}
