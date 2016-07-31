using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  [TestClass]
  public class ViewportContainerTests
  {
    [TestMethod]
    [TestCategory("AcadTests")]
    public void TestCreateViewport()
    {
      var result = TestRunner.Test("TestCreateViewport", "Linq2Acad.Tests.Acad.dll");
      Assert.IsTrue(result.Passed, result.Message);
    }
    
    [TestMethod]
    [TestCategory("AcadTests")]
    public void TestAddViewport()
    {
      var result = TestRunner.Test("TestAddViewport", "Linq2Acad.Tests.Acad.dll");
      Assert.IsTrue(result.Passed, result.Message);
    }
  }
}
