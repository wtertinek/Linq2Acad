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
      using (var db = AcadDatabase.Active())
      {
        var newTableStyle = db.TableStyles.Create("NewTableStyle");

        Assert.Dictionary(db.Database, db.Database.TableStyleDictionaryId, dict => dict.Contains("NewTableStyle"),
                          "TableStyle dictionary does not contain an element with name 'NewTableStyle'");
        Assert.DictionaryIDs(db.Database, db.Database.TableStyleDictionaryId, ids => ids.Any(id => id == newTableStyle.ObjectId),
                             "TableStyle dictionary does not contain the newly created element");
      }
    }
  }
}
