using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linq2Acad.Tests
{
  [TestClass]
  [DebuggerStepThrough]
  public partial class TableStyleContainerTests
  {
    [TestMethod]
    [TestCategory("AcadTest")]
    public void TestCreateTableStyle()
    {
      var result = AcadTestRunner.TestRunner.RunTest<TableStyleContainerTests>("CreateTableStyle");
      if (!result.Passed) Assert.Fail(result.Message);
    }
  }
}
