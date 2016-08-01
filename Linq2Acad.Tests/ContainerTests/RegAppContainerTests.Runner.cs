using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linq2Acad.Tests
{
  [TestClass]
  public partial class RegAppContainerTests
  {
    [TestMethod]
    [TestCategory("AcadTest")]
    public void TestCreateRegApp()
    {
      var result = AcadTestRunner.TestRunner.Test(typeof(RegAppContainerTests).Assembly.Location, "RegAppContainerTests", "TestCreateRegApp");
      Assert.IsTrue(result.Passed, result.Message);
    }
    
    [TestMethod]
    [TestCategory("AcadTest")]
    public void TestAddRegApp()
    {
      var result = AcadTestRunner.TestRunner.Test(typeof(RegAppContainerTests).Assembly.Location, "RegAppContainerTests", "TestAddRegApp");
      Assert.IsTrue(result.Passed, result.Message);
    }
  }
}
