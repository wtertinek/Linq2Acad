using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linq2Acad.Tests
{
  [TestClass]
  public partial class MLeaderStyleContainerTests
  {
    [TestMethod]
    [TestCategory("AcadTest")]
    public void TestCreateMLeaderStyle()
    {
      var result = AcadTestRunner.TestRunner.RunTest<MLeaderStyleContainerTests>("CreateMLeaderStyle");
      Assert.IsTrue(result.Passed, result.Message);
    }
  }
}
