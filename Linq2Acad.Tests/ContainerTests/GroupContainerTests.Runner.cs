using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linq2Acad.Tests
{
  [TestClass]
  [DebuggerStepThrough]
  public partial class GroupContainerTests
  {
    [TestMethod]
    [TestCategory("AcadTest")]
    public void TestCreateGroup()
    {
      var result = AcadTestRunner.TestRunner.RunTest<GroupContainerTests>("CreateGroup");
      if (!result.Passed) Assert.Fail(result.Message);
    }
  }
}
