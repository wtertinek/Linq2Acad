using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linq2Acad.Tests
{
  [TestClass]
  [DebuggerStepThrough]
  public class TableStyleContainerTests_
  {
    [TestMethod]
    [TestCategory("AutoCAD Unit Tests")]
    public void TestCreateTableStyle()
    {
      var result = AcadTestRunner.TestRunner.RunTest<TableStyleContainerTests>("TestCreateTableStyle");
      if (!result.Passed) Assert.Fail(result.Message);
    }
  }
}
