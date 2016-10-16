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
    [TestCategory("AutoCAD Unit Tests")]
    public void TestCreateMLeaderStyle()
    {
      var result = AcadTestRunner.TestRunner.RunTest<MLeaderStyleContainerTests>("TestCreateMLeaderStyle");
      if (!result.Passed) Assert.Fail(result.Message);
    }
  }
}
