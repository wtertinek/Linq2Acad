using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  [TestClass]
  public class RegAppContainerTests
  {
    [TestMethod]
    [TestCategory("AcadTests")]
    public void TestCreateRegApp()
    {
      var result = TestRunner.Test("TestCreateRegApp", "Linq2Acad.Tests.Acad.dll");
      Assert.IsTrue(result.Passed, result.Message);
    }
    
    [TestMethod]
    [TestCategory("AcadTests")]
    public void TestAddRegApp()
    {
      var result = TestRunner.Test("TestAddRegApp", "Linq2Acad.Tests.Acad.dll");
      Assert.IsTrue(result.Passed, result.Message);
    }
  }
}
