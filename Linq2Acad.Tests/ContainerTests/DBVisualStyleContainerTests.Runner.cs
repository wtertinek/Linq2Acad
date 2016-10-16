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
    [TestCategory("AutoCAD Unit Tests")]
    public void TestCreateDBVisualStyle()
    {
      var result = AcadTestRunner.TestRunner.RunTest<DBVisualStyleContainerTests>("TestCreateDBVisualStyle");
      if (!result.Passed) Assert.Fail(result.Message);
    }
  }
}
