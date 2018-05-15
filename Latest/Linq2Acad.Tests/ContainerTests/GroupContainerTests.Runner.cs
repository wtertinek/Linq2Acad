using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linq2Acad.Tests
{
  [TestClass]
  [DebuggerStepThrough]
  public class GroupContainerTests_
  {
    [TestMethod]
    [TestCategory("Container Tests")]
    public void TestCreateGroup()
    {
      var result = AcadTestRunner.TestRunner.RunTest(typeof(GroupContainerTests), "TestCreateGroup");

      if (!result.Passed)
      {
        result.DebugPrintFullOutput("TestCreateGroup");
        Assert.Fail(result.Message);
      }
    }
  }
}
