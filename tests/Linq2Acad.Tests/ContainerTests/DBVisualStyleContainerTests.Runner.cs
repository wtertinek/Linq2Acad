using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linq2Acad.Tests
{
  [TestClass]
  [DebuggerStepThrough]
  public class DBVisualStyleContainerTests_
  {
    [TestMethod]
    [TestCategory("Container Tests")]
    public void TestCreateDBVisualStyle()
    {
      var result = AcadTestRunner.TestRunner.RunTest(typeof(DBVisualStyleContainerTests), "TestCreateDBVisualStyle");

      if (!result.Passed)
      {
        result.DebugPrintFullOutput("TestCreateDBVisualStyle");
        Assert.Fail(result.Message);
      }
    }
  }
}
