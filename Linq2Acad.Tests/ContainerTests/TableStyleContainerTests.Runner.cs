using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linq2Acad.Tests
{
  [TestClass]
  public partial class TableStyleContainerTests
  {
    [TestMethod]
    [TestCategory("AcadTest")]
    public void TestCreateTableStyle()
    {
      var result = AcadTestRunner.TestRunner.RunTest<TableStyleContainerTests>("CreateTableStyle");
      Assert.IsTrue(result.Passed, result.Message);
    }
  }
}
