using System;
using System.Linq;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  public partial class GroupContainerTests
  {
    [AcadTest("CreateGroup")]
    public void CreateGroup()
    {
      var newId = ObjectId.Null;

      using (var db = AcadDatabase.Active())
      {
        var newGroup = db.Groups.Create("NewGroup");
        newId = newGroup.ObjectId;
      }

      Assert.That.GroupDictionary.Contains("NewGroup");
      Assert.That.GroupDictionary.Contains(newId);
    }
  }
}
