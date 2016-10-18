using System;
using System.Linq;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using AcadTestRunner;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linq2Acad.Tests
{
  public class XRefContainerTests
  {
    [AcadTest]
    public void TestAttachAndDetachXRef()
    {
      using (var db = AcadDatabase.Active())
      {
        Assert.AreEqual(0, db.XRefs.Count());
      }

      using (var db = AcadDatabase.Active())
      {
        var xRef = db.XRefs.Attach(@"C:\Temp\Test.dwg", "XRefBlock");
        Assert.AreEqual("XRefBlock", xRef.BlockName);
        Assert.AreEqual(@"C:\Temp\Test.dwg", xRef.FilePath);
      }

      using (var db = AcadDatabase.Active())
      {
        Assert.AreEqual(1, db.XRefs.Count());
      }

      using (var db = AcadDatabase.Active())
      {
        db.XRefs.First()
                .Detach();
      }

      using (var db = AcadDatabase.Active())
      {
        Assert.AreEqual(0, db.XRefs.Count());
      }
    }
  }
}
