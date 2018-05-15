using System;
using System.Linq;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  public class SectionViewStyleContainerTests
  {
    [AcadTest]
    public void TestCreateSectionViewStyle()
    {
      var newId = ObjectId.Null;

      using (var db = AcadDatabase.Active())
      {
        var newSectionViewStyle = db.SectionViewStyles.Create("NewSectionViewStyle");
        newId = newSectionViewStyle.ObjectId;
      }

      AcadAssert.That.SectionViewStyleDictionary.Contains("NewSectionViewStyle");
      AcadAssert.That.SectionViewStyleDictionary.Contains(newId);
    }
  }
}
