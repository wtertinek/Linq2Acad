using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  [TestClass]
  public class MLeaderStyleContainerTests
  {
    [TestMethod]
    [TestCategory("AcadTests")]
    public void TestCreateMLeaderStyle()
    {
      var result = TestRunner.Test("TestCreateMLeaderStyle", "Linq2Acad.Tests.Acad.dll");
      Assert.IsTrue(result.Passed, result.Message);
    }
  }
}
