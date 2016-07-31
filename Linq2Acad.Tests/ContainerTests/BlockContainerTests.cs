using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  [TestClass]
  public class BlockContainerTests
  {
    [TestMethod]
    [TestCategory("AcadTests")]
    public void TestCreateBlock()
    {
      var result = TestRunner.Test("TestCreateBlock", "Linq2Acad.Tests.Acad.dll");
      Assert.IsTrue(result.Passed, result.Message);
    }
    
    [TestMethod]
    [TestCategory("AcadTests")]
    public void TestAddBlock()
    {
      var result = TestRunner.Test("TestAddBlock", "Linq2Acad.Tests.Acad.dll");
      Assert.IsTrue(result.Passed, result.Message);
    }
  }
}
