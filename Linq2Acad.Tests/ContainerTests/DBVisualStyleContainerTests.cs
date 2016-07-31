using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  [TestClass]
  public class DBVisualStyleContainerTests
  {
    [TestMethod]
    [TestCategory("AcadTests")]
    public void TestCreateDBVisualStyle()
    {
      var result = TestRunner.Test("TestCreateDBVisualStyle", "Linq2Acad.Tests.Acad.dll");
      Assert.IsTrue(result.Passed, result.Message);
    }
  }
}
