using System;
using System.Linq;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using AcadTestRunner;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using System.IO;

namespace Linq2Acad.Tests
{
  public class XRefContainerTests
  {
    [AcadTest]
    public void TestAttachXRef()
    {
      using (var dwgFile = DwgExtractor.ExtractDwgFile("Drawing1.dwg"))
      {
        using (var db = AcadDatabase.Active())
        {
          Assert.AreEqual(0, db.XRefs.Count());

          var xRef = db.XRefs.Attach(dwgFile.FileName, "XRefBlock");
          Assert.AreEqual("XRefBlock", xRef.BlockName);
          Assert.AreEqual(dwgFile.FileName, xRef.FilePath);
          Assert.IsTrue(xRef.IsFromAttachReference);
        }

        using (var db = AcadDatabase.Active())
        {
          Assert.AreEqual(1, db.XRefs.Count());
        }
      }
    }

    [AcadTest]
    public void TestOverlayXRef()
    {
      using (var dwgFile = DwgExtractor.ExtractDwgFile("Drawing1.dwg"))
      {
        using (var db = AcadDatabase.Active())
        {
          Assert.AreEqual(0, db.XRefs.Count());

          var xRef = db.XRefs.Overlay(dwgFile.FileName, "XRefBlock");
          Assert.AreEqual("XRefBlock", xRef.BlockName);
          Assert.AreEqual(dwgFile.FileName, xRef.FilePath);
          Assert.IsTrue(xRef.IsFromOverlayReference);
        }

        using (var db = AcadDatabase.Active())
        {
          Assert.AreEqual(1, db.XRefs.Count());
        }
      }
    }

    [AcadTest]
    public void TestDetachXRef()
    {
      using (var dwgFile = DwgExtractor.ExtractDwgFile("Drawing1.dwg"))
      {
        using (var db = AcadDatabase.Active())
        {
          db.XRefs.Attach(dwgFile.FileName, "XRefBlock");
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
}
