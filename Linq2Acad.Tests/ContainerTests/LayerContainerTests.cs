using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  [TestClass]
  public class LayerContainerTests
  {
    [TestMethod]
    [TestCategory("AcadTests")]
    public void TestCreateLayer()
    {
      var result = TestRunner.Test("TestCreateLayer", "Linq2Acad.Tests.Acad.dll");
      Assert.IsTrue(result.Passed, result.Message);
    }
    
    [TestMethod]
    [TestCategory("AcadTests")]
    public void TestAddLayer()
    {
      var result = TestRunner.Test("TestAddLayer", "Linq2Acad.Tests.Acad.dll");
      Assert.IsTrue(result.Passed, result.Message);
    }
  }
}
