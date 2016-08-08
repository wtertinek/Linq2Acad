using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linq2Acad.Tests
{
  [TestClass]
  public partial class GroupContainerTests
  {
    [TestMethod]
    [TestCategory("AcadTest")]
    public void TestCreateGroup()
    {
      var result = AcadTestRunner.TestRunner.RunTest(typeof(GroupContainerTests).Assembly.Location, "GroupContainerTests", "CreateGroup");
      Assert.IsTrue(result.Passed, result.Message);
    }
  }
}
