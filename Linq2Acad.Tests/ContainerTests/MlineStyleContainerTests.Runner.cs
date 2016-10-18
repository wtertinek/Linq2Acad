using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linq2Acad.Tests
{
  [TestClass]
  [DebuggerStepThrough]
  public class MlineStyleContainerTests_
  {
    [TestMethod]
    [TestCategory("Container Tests")]
    public void TestCreateMlineStyle()
    {
      var result = AcadTestRunner.TestRunner.RunTest<MlineStyleContainerTests>("TestCreateMlineStyle");

      if (!result.Passed)
      {
        result.DebugPrintFullOutput("TestCreateMlineStyle");
        Assert.Fail(result.Message);
      }
    }
  }
}
