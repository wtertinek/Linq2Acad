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
      var result = AcadTestRunner.TestRunner.RunTest(typeof(ViewportContainerTests).Assembly.Location, "ViewportContainerTests", "CreateViewport");
      Assert.IsTrue(result.Passed, result.Message);
    }
    
    [TestMethod]
    [TestCategory("AcadTest")]
    public void TestAddViewport()
    {
      var result = AcadTestRunner.TestRunner.RunTest(typeof(ViewportContainerTests).Assembly.Location, "ViewportContainerTests", "AddViewport");
      Assert.IsTrue(result.Passed, result.Message);
    }
  }
}
