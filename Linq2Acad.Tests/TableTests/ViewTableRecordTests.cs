using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  [TestClass]
  public class ViewTableRecordTests
  {
    [TestMethod]
    [TestCategory("AcadTests")]
    public void TestCreateViewTableRecord()
    {
      var result = TestRunner.Test("TestCreateViewTableRecord", "Linq2Acad.Tests.Acad.dll");
      Assert.IsTrue(result.Passed, result.Message);
    }
    
    [TestMethod]
    [TestCategory("AcadTests")]
    public void TestAddViewTableRecord()
    {
      var result = TestRunner.Test("TestAddViewTableRecord", "Linq2Acad.Tests.Acad.dll");
      Assert.IsTrue(result.Passed, result.Message);
    }
  }
}
