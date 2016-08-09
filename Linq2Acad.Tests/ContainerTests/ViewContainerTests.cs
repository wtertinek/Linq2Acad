using System;
using System.Linq;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  public partial class ViewContainerTests
  {
    [AcadTest("CreateView")]
    public void CreateView()
    {
      var newId = ObjectId.Null;

      using (var db = AcadDatabase.Active())
      {
        var newView = db.Views.Create("NewView");
        newId = newView.ObjectId;
      }

      Assert.That.ViewTable.Contains("NewView");
      Assert.That.ViewTable.Contains(newId);
    }

    [AcadTest("AddView")]
    public void AddView()
    {
      var newId = ObjectId.Null;

      using (var db = AcadDatabase.Active())
      {
        var newView = new ViewTableRecord() { Name = "NewView" };
        db.Views.Add(newView);
        newId = newView.ObjectId;
      }

      Assert.That.ViewTable.Contains(newId);
    }
  }
}
