using System;
using System.Linq;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  public partial class DBVisualStyleContainerTests
  {
    [AcadTest("CreateDBVisualStyle")]
    public void CreateDBVisualStyle()
    {
      var newId = ObjectId.Null;

      using (var db = AcadDatabase.Active())
      {
        var newDBVisualStyle = db.DBVisualStyles.Create("NewDBVisualStyle");
        newId = newDBVisualStyle.ObjectId;
      }

      Assert.That.DBVisualStyleDictionary.Contains("NewDBVisualStyle");
      Assert.That.DBVisualStyleDictionary.Contains(newId);
    }
  }
}
