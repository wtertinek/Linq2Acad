using System;
using System.Linq;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  public partial class DBVisualStyleContainerTests
  {
    [AcadTest("CreateDBVisualStyle")]
    public void CreateDBVisualStyle()
    {
      using (var db = AcadDatabase.Active())
      {
        var newDBVisualStyle = db.DBVisualStyles.Create("NewDBVisualStyle");

        Assert.Dictionary(db.Database, db.Database.VisualStyleDictionaryId, dict => dict.Contains("NewDBVisualStyle"),
                          "DBVisualStyle dictionary does not contain an element with name 'NewDBVisualStyle'");
        Assert.DictionaryIDs(db.Database, db.Database.VisualStyleDictionaryId, ids => ids.Any(id => id == newDBVisualStyle.ObjectId),
                             "DBVisualStyle dictionary does not contain the newly created element");
      }
    }
  }
}
