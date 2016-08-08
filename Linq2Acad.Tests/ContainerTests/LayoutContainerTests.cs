using System;
using System.Linq;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  public partial class LayoutContainerTests
  {
    [AcadTest("CreateLayout")]
    public void CreateLayout()
    {
      using (var db = AcadDatabase.Active())
      {
        var newLayout = db.Layouts.Create("NewLayout");

        Assert.Dictionary(db.Database, db.Database.LayoutDictionaryId, dict => dict.Contains("NewLayout"),
                          "Layout dictionary does not contain an element with name 'NewLayout'");
        Assert.DictionaryIDs(db.Database, db.Database.LayoutDictionaryId, ids => ids.Any(id => id == newLayout.ObjectId),
                             "Layout dictionary does not contain the newly created element");
      }
    }
  }
}
