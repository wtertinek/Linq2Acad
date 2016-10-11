using System;
using System.Linq;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  public class TableStyleContainerTests
  {
    [AcadTest]
    public void TestCreateTableStyle()
    {
      var newId = ObjectId.Null;

      using (var db = AcadDatabase.Active())
      {
        var newTableStyle = db.TableStyles.Create("NewTableStyle");
        newId = newTableStyle.ObjectId;
      }

      AcadAssert.That.TableStyleDictionary.Contains("NewTableStyle");
      AcadAssert.That.TableStyleDictionary.Contains(newId);
    }
  }
}
