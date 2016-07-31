using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  [TestClass]
  public class GroupContainerTests
  {
    [TestMethod]
    [TestCategory("AcadTests")]
    public void TestCreateGroup()
    {
      var result = TestRunner.Test("TestCreateGroup", "Linq2Acad.Tests.Acad.dll");
      Assert.IsTrue(result.Passed, result.Message);
    }
  }
}
