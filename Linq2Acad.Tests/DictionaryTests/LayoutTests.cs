using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  [TestClass]
  public class LayoutTests
  {
    [TestMethod]
    [TestCategory("AcadTests")]
    public void TestCreateLayout()
    {
      var result = TestRunner.Test("TestCreateLayout", "Linq2Acad.Tests.Acad.dll");
      Assert.IsTrue(result.Passed, result.Message);
    }
  }
}
