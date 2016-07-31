using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  [TestClass]
  public class LinetypeContainerTests
  {
    [TestMethod]
    [TestCategory("AcadTests")]
    public void TestCreateLinetype()
    {
      var result = TestRunner.Test("TestCreateLinetype", "Linq2Acad.Tests.Acad.dll");
      Assert.IsTrue(result.Passed, result.Message);
    }
    
    [TestMethod]
    [TestCategory("AcadTests")]
    public void TestAddLinetype()
    {
      var result = TestRunner.Test("TestAddLinetype", "Linq2Acad.Tests.Acad.dll");
      Assert.IsTrue(result.Passed, result.Message);
    }
  }
}
