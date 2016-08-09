using System;
using System.Linq;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  public partial class MlineStyleContainerTests
  {
    [AcadTest("CreateMlineStyle")]
    public void CreateMlineStyle()
    {
      var newId = ObjectId.Null;

      using (var db = AcadDatabase.Active())
      {
        var newMlineStyle = db.MlineStyles.Create("NewMlineStyle");
        newId = newMlineStyle.ObjectId;
      }

      Assert.That.MlineStyleDictionary.Contains("NewMlineStyle");
      Assert.That.MlineStyleDictionary.Contains(newId);
    }
  }
}
