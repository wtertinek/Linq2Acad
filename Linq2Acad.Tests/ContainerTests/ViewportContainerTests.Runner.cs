using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linq2Acad.Tests
{
  [TestClass]
  [DebuggerStepThrough]
  public class ViewportContainerTests_
  {
    [TestMethod]
    [TestCategory("AutoCAD Unit Tests")]
    public void TestCreateViewport()
    {
      var result = AcadTestRunner.TestRunner.RunTest<ViewportContainerTests>("TestCreateViewport");
      if (!result.Passed) Assert.Fail(result.Message);
    }
    
    [TestMethod]
    [TestCategory("AutoCAD Unit Tests")]
    public void TestAddViewport()
    {
      var result = AcadTestRunner.TestRunner.RunTest<ViewportContainerTests>("TestAddViewport");
      if (!result.Passed) Assert.Fail(result.Message);
    }
  }
}
