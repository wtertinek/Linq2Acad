using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linq2Acad.Tests
{
  [TestClass]
  [DebuggerStepThrough]
  public class MLeaderStyleContainerTests_
  {
    [TestMethod]
    [TestCategory("Container Tests")]
    public void TestCreateMLeaderStyle()
    {
      var result = AcadTestRunner.TestRunner.RunTest<MLeaderStyleContainerTests>("TestCreateMLeaderStyle");

      if (!result.Passed)
      {
        result.DebugPrintFullOutput("TestCreateMLeaderStyle");
        Assert.Fail(result.Message);
      }
    }
  }
}
