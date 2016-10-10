using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linq2Acad.Tests
{
  [TestClass]
  [DebuggerStepThrough]
  public partial class ViewportContainerTests
  {
    [TestMethod]
    [TestCategory("AcadTest")]
    public void TestCreateViewport()
    {
      var result = AcadTestRunner.TestRunner.RunTest<ViewportContainerTests>("CreateViewport");
      if (!result.Passed) Assert.Fail(result.Message);
    }
    
    [TestMethod]
    [TestCategory("AcadTest")]
    public void TestAddViewport()
    {
      var result = AcadTestRunner.TestRunner.RunTest<ViewportContainerTests>("AddViewport");
      if (!result.Passed) Assert.Fail(result.Message);
    }
  }
}
