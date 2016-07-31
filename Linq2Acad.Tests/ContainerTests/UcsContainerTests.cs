using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  [TestClass]
  public class UcsContainerTests
  {
    [TestMethod]
    [TestCategory("AcadTests")]
    public void TestCreateUcs()
    {
      var result = TestRunner.Test("TestCreateUcs", "Linq2Acad.Tests.Acad.dll");
      Assert.IsTrue(result.Passed, result.Message);
    }
    
    [TestMethod]
    [TestCategory("AcadTests")]
    public void TestAddUcs()
    {
      var result = TestRunner.Test("TestAddUcs", "Linq2Acad.Tests.Acad.dll");
      Assert.IsTrue(result.Passed, result.Message);
    }
  }
}
