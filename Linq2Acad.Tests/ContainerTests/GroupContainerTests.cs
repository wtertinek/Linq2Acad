using System;
using System.Linq;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  [AcadTestClass("GroupContainerTests")]
  public partial class GroupContainerTests
  {
    [AcadTestMethod("TestCreateGroup")]
    public void CreateGroup()
    {
      using (var db = AcadDatabase.Active())
      {
        var newGroup = db.Groups.Create("NewGroup");

        Assert.Dictionary(db.Database, db.Database.GroupDictionaryId, dict => dict.Contains("NewGroup"),
                          "Group dictionary does not contain an element with name 'NewGroup'");
        Assert.DictionaryIDs(db.Database, db.Database.GroupDictionaryId, ids => ids.Any(id => id == newGroup.ObjectId),
                             "Group dictionary does not contain the newly created element");
      }
    }
  }
}
