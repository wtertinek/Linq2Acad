using System;
using System.Linq;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  public partial class MlineStyleContainerTests
  {
    [AcadTest]
    public void CreateMlineStyle()
    {
      var newId = ObjectId.Null;

      using (var db = AcadDatabase.Active())
      {
        var newMlineStyle = db.MlineStyles.Create("NewMlineStyle");
        newId = newMlineStyle.ObjectId;
      }

      AcadAssert.That.MlineStyleDictionary.Contains("NewMlineStyle");
      AcadAssert.That.MlineStyleDictionary.Contains(newId);
    }
  }
}
