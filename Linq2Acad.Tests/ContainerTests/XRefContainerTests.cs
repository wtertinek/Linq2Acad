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
      // TODO: Get this file from assembly resources
      var xRefFilePath = @"C:\Temp\Test.dwg";

      using (var db = AcadDatabase.Active())
      {
        Assert.AreEqual(0, db.XRefs.Count());

        var xRef = db.XRefs.Attach(xRefFilePath);
        Assert.AreEqual("XRefBlock", xRef.BlockName);
        Assert.AreEqual(xRefFilePath, xRef.FilePath);
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
