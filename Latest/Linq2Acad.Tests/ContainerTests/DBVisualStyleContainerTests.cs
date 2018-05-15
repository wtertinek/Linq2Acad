using System;
using System.Linq;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  public class DBVisualStyleContainerTests
  {
    [AcadTest]
    public void TestCreateDBVisualStyle()
    {
      var newId = ObjectId.Null;

      using (var db = AcadDatabase.Active())
      {
        var newDBVisualStyle = db.DBVisualStyles.Create("NewDBVisualStyle");
        newId = newDBVisualStyle.ObjectId;
      }

      AcadAssert.That.DBVisualStyleDictionary.Contains("NewDBVisualStyle");
      AcadAssert.That.DBVisualStyleDictionary.Contains(newId);
    }
  }
}
