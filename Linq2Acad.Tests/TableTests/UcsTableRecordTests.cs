using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  [TestClass]
  public class UcsTableRecordTests
  {
    [TestMethod]
    [TestCategory("AcadTests")]
    public void TestCreateUcsTableRecord()
    {
      var result = TestRunner.Test("TestCreateUcsTableRecord", "Linq2Acad.Tests.Acad.dll");
      Assert.IsTrue(result.Passed, result.Message);
    }
    
    [TestMethod]
    [TestCategory("AcadTests")]
    public void TestAddUcsTableRecord()
    {
      var result = TestRunner.Test("TestAddUcsTableRecord", "Linq2Acad.Tests.Acad.dll");
      Assert.IsTrue(result.Passed, result.Message);
    }
  }
}
