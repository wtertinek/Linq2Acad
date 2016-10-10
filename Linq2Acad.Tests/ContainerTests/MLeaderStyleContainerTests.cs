using System;
using System.Linq;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  public partial class MLeaderStyleContainerTests
  {
    [AcadTest("CreateMLeaderStyle")]
    public void CreateMLeaderStyle()
    {
      var newId = ObjectId.Null;

      using (var db = AcadDatabase.Active())
      {
        var newMLeaderStyle = db.MLeaderStyles.Create("NewMLeaderStyle");
        newId = newMLeaderStyle.ObjectId;
      }

      AcadAssert.That.MLeaderStyleDictionary.Contains("NewMLeaderStyle");
      AcadAssert.That.MLeaderStyleDictionary.Contains(newId);
    }
  }
}
