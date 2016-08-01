using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linq2Acad.Tests
{
  [TestClass]
  public partial class MlineStyleContainerTests
  {
    [TestMethod]
    [TestCategory("AcadTest")]
    public void TestCreateMlineStyle()
    {
      var result = AcadTestRunner.TestRunner.Test(typeof(MlineStyleContainerTests).Assembly.Location, "MlineStyleContainerTests", "TestCreateMlineStyle");
      Assert.IsTrue(result.Passed, result.Message);
    }
  }
}
