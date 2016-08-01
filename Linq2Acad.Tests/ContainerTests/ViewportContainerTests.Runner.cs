using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linq2Acad.Tests
{
  [TestClass]
  public partial class ViewportContainerTests
  {
    [TestMethod]
    [TestCategory("AcadTest")]
    public void TestCreateViewport()
    {
      var result = AcadTestRunner.TestRunner.Test(typeof(ViewportContainerTests).Assembly.Location, "ViewportContainerTests", "TestCreateViewport");
      Assert.IsTrue(result.Passed, result.Message);
    }
    
    [TestMethod]
    [TestCategory("AcadTest")]
    public void TestAddViewport()
    {
      var result = AcadTestRunner.TestRunner.Test(typeof(ViewportContainerTests).Assembly.Location, "ViewportContainerTests", "TestAddViewport");
      Assert.IsTrue(result.Passed, result.Message);
    }
  }
}
