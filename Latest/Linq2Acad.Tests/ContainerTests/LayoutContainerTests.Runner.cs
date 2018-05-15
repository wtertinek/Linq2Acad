using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linq2Acad.Tests
{
  [TestClass]
  [DebuggerStepThrough]
  public class LayoutContainerTests_
  {
    [TestMethod]
    [TestCategory("Container Tests")]
    public void TestCreateLayout()
    {
      var result = AcadTestRunner.TestRunner.RunTest(typeof(LayoutContainerTests), "TestCreateLayout");

      if (!result.Passed)
      {
        result.DebugPrintFullOutput("TestCreateLayout");
        Assert.Fail(result.Message);
      }
    }
  }
}
