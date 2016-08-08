using System;
using System.Linq;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  public partial class SectionViewStyleContainerTests
  {
    [AcadTest("CreateSectionViewStyle")]
    public void CreateSectionViewStyle()
    {
      using (var db = AcadDatabase.Active())
      {
        var newSectionViewStyle = db.SectionViewStyles.Create("NewSectionViewStyle");

        Assert.Dictionary(db.Database, db.Database.SectionViewStyleDictionaryId, dict => dict.Contains("NewSectionViewStyle"),
                          "SectionViewStyle dictionary does not contain an element with name 'NewSectionViewStyle'");
        Assert.DictionaryIDs(db.Database, db.Database.SectionViewStyleDictionaryId, ids => ids.Any(id => id == newSectionViewStyle.ObjectId),
                             "SectionViewStyle dictionary does not contain the newly created element");
      }
    }
  }
}
