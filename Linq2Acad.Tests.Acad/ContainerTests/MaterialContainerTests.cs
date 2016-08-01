using System;
using System.Linq;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  public class MaterialContainerTests
  {
    [CommandMethod("TestCreateMaterial")]
    public void TestCreateMaterial()
    {
      var notifier = new Notification("TestCreateMaterial");

      try
      {
        using (var db = AcadDatabase.Active())
        {
          var newMaterial = db.Materials.Create("NewMaterial");

          var ok = Check.Dictionary(db.Database, db.Database.MaterialDictionaryId, dict => dict.Contains("NewMaterial"));
          if (!ok) { notifier.TestFailed("Material dictionary does not contain an element with name 'NewMaterial'"); return; }

          ok = Check.DictionaryIDs(db.Database, db.Database.MaterialDictionaryId, ids => ids.Any(id => id == newMaterial.ObjectId));
          if (!ok) { notifier.TestFailed("Material dictionary does not contain the newly created element"); return; }
        }
      }
      catch (System.Exception e)
      {
        notifier.TestFailed(e);
      }

      notifier.TestPassed();
    }
  }
}
