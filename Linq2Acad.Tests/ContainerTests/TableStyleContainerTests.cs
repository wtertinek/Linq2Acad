using System;
using System.Linq;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  public partial class TableStyleContainerTests
  {
    [AcadTest("CreateTableStyle")]
    public void CreateTableStyle()
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
