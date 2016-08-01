using System;
using System.Linq;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  [AcadTestClass("MLeaderStyleContainerTests")]
  public partial class MLeaderStyleContainerTests
  {
    [AcadTestMethod("TestCreateMLeaderStyle")]
    public void CreateMLeaderStyle()
    {
      using (var db = AcadDatabase.Active())
      {
        var newMLeaderStyle = db.MLeaderStyles.Create("NewMLeaderStyle");

        Assert.Dictionary(db.Database, db.Database.MLeaderStyleDictionaryId, dict => dict.Contains("NewMLeaderStyle"),
                          "MLeaderStyle dictionary does not contain an element with name 'NewMLeaderStyle'");
        Assert.DictionaryIDs(db.Database, db.Database.MLeaderStyleDictionaryId, ids => ids.Any(id => id == newMLeaderStyle.ObjectId),
                             "MLeaderStyle dictionary does not contain the newly created element");
      }
    }
  }
}
