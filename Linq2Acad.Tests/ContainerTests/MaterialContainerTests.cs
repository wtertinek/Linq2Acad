using System;
using System.Linq;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  [AcadTestClass("MaterialContainerTests")]
  public partial class MaterialContainerTests
  {
    [AcadTestMethod("TestCreateMaterial")]
    public void CreateMaterial()
    {
      using (var db = AcadDatabase.Active())
      {
        var newMaterial = db.Materials.Create("NewMaterial");

        Assert.Dictionary(db.Database, db.Database.MaterialDictionaryId, dict => dict.Contains("NewMaterial"),
                          "Material dictionary does not contain an element with name 'NewMaterial'");
        Assert.DictionaryIDs(db.Database, db.Database.MaterialDictionaryId, ids => ids.Any(id => id == newMaterial.ObjectId),
                             "Material dictionary does not contain the newly created element");
      }
    }
  }
}
