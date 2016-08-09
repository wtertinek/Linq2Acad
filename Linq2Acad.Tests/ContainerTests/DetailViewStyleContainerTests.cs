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
      var newId = ObjectId.Null;

      using (var db = AcadDatabase.Active())
      {
        var newDetailViewStyle = db.DetailViewStyles.Create("NewDetailViewStyle");
        newId = newDetailViewStyle.ObjectId;
      }

      Assert.That.DetailViewStyleDictionary.Contains("NewDetailViewStyle");
      Assert.That.DetailViewStyleDictionary.Contains(newId);
    }
  }
}
