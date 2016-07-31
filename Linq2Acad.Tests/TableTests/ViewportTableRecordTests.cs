using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  [TestClass]
  public class ViewportTableRecordTests
  {
    [TestMethod]
    [TestCategory("AcadTests")]
    public void TestCreateViewportTableRecord()
    {
      var result = TestRunner.Test("TestCreateViewportTableRecord", "Linq2Acad.Tests.Acad.dll");
      Assert.IsTrue(result.Passed, result.Message);
    }
    
    [TestMethod]
    [TestCategory("AcadTests")]
    public void TestAddViewportTableRecord()
    {
      var result = TestRunner.Test("TestAddViewportTableRecord", "Linq2Acad.Tests.Acad.dll");
      Assert.IsTrue(result.Passed, result.Message);
    }
  }
}
