using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linq2Acad.Tests
{
  [TestClass]
  public partial class UcsContainerTests
  {
    [TestMethod]
    [TestCategory("AcadTest")]
    public void TestCreateUcs()
    {
      var result = AcadTestRunner.TestRunner.RunTest(typeof(UcsContainerTests).Assembly.Location, "UcsContainerTests", "CreateUcs");
      Assert.IsTrue(result.Passed, result.Message);
    }
    
    [TestMethod]
    [TestCategory("AcadTest")]
    public void TestAddUcs()
    {
      var result = AcadTestRunner.TestRunner.RunTest(typeof(UcsContainerTests).Assembly.Location, "UcsContainerTests", "AddUcs");
      Assert.IsTrue(result.Passed, result.Message);
    }
  }
}
