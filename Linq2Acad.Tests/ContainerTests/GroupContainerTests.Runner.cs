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
    [TestCategory("AutoCAD Unit Tests")]
    public void TestCreateGroup()
    {
      var result = AcadTestRunner.TestRunner.RunTest<GroupContainerTests>("TestCreateGroup");
      if (!result.Passed) Assert.Fail(result.Message);
    }
  }
}
