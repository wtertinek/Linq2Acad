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
    [TestCategory("AutoCAD Unit Tests")]
    public void TestCreateLayout()
    {
      var result = AcadTestRunner.TestRunner.RunTest<LayoutContainerTests>("TestCreateLayout");
      if (!result.Passed) Assert.Fail(result.Message);
    }
  }
}
