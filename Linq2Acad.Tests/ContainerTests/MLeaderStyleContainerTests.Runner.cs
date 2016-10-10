using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linq2Acad.Tests
{
  [TestClass]
  [DebuggerStepThrough]
  public partial class MLeaderStyleContainerTests
  {
    [TestMethod]
    [TestCategory("AcadTest")]
    public void TestCreateMLeaderStyle()
    {
      var result = AcadTestRunner.TestRunner.RunTest<MLeaderStyleContainerTests>("CreateMLeaderStyle");
      if (!result.Passed) Assert.Fail(result.Message);
    }
  }
}
