using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linq2Acad.Tests
{
  [TestClass]
  public partial class DBVisualStyleContainerTests
  {
    [TestMethod]
    [TestCategory("AcadTest")]
    public void TestCreateDBVisualStyle()
    {
      var result = AcadTestRunner.TestRunner.RunTest<DBVisualStyleContainerTests>("CreateDBVisualStyle");
      Assert.IsTrue(result.Passed, result.Message);
    }
  }
}
