using System;
using System.Linq;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  [AcadTestClass("MlineStyleContainerTests")]
  public partial class MlineStyleContainerTests
  {
    [AcadTestMethod("TestCreateMlineStyle")]
    public void CreateMlineStyle()
    {
      using (var db = AcadDatabase.Active())
      {
        var newMlineStyle = db.MlineStyles.Create("NewMlineStyle");

        Assert.Dictionary(db.Database, db.Database.MLStyleDictionaryId, dict => dict.Contains("NewMlineStyle"),
                          "MlineStyle dictionary does not contain an element with name 'NewMlineStyle'");
        Assert.DictionaryIDs(db.Database, db.Database.MLStyleDictionaryId, ids => ids.Any(id => id == newMlineStyle.ObjectId),
                             "MlineStyle dictionary does not contain the newly created element");
      }
    }
  }
}
