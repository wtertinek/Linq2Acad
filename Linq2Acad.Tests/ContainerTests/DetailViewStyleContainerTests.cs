using System;
using System.Linq;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  public partial class DetailViewStyleContainerTests
  {
    [AcadTest("CreateDetailViewStyle")]
    public void CreateDetailViewStyle()
    {
      using (var db = AcadDatabase.Active())
      {
        var newDetailViewStyle = db.DetailViewStyles.Create("NewDetailViewStyle");

        Assert.Dictionary(db.Database, db.Database.DetailViewStyleDictionaryId, dict => dict.Contains("NewDetailViewStyle"),
                          "DetailViewStyle dictionary does not contain an element with name 'NewDetailViewStyle'");
        Assert.DictionaryIDs(db.Database, db.Database.DetailViewStyleDictionaryId, ids => ids.Any(id => id == newDetailViewStyle.ObjectId),
                             "DetailViewStyle dictionary does not contain the newly created element");
      }
    }
  }
}
