using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linq2Acad.Tests
{
  [TestClass]
  [DebuggerStepThrough]
  public class DetailViewStyleContainerTests_
  {
    [TestMethod]
    [TestCategory("Container Tests")]
    public void TestCreateDetailViewStyle()
    {
      var result = AcadTestRunner.TestRunner.RunTest(typeof(DetailViewStyleContainerTests), "TestCreateDetailViewStyle");

      if (!result.Passed)
      {
        result.DebugPrintFullOutput("TestCreateDetailViewStyle");
        Assert.Fail(result.Message);
      }
    }
  }
}
