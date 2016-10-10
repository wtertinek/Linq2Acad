using System;
using System.Linq;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  public partial class DetailViewStyleContainerTests
  {
    [AcadTest]
    public void CreateDetailViewStyle()
    {
      var newId = ObjectId.Null;

      using (var db = AcadDatabase.Active())
      {
        var newDetailViewStyle = db.DetailViewStyles.Create("NewDetailViewStyle");
        newId = newDetailViewStyle.ObjectId;
      }

      AcadAssert.That.DetailViewStyleDictionary.Contains("NewDetailViewStyle");
      AcadAssert.That.DetailViewStyleDictionary.Contains(newId);
    }
  }
}
