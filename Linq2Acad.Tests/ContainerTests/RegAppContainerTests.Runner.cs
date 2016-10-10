using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linq2Acad.Tests
{
  [TestClass]
  [DebuggerStepThrough]
  public partial class RegAppContainerTests
  {
    [TestMethod]
    [TestCategory("AcadTest")]
    public void TestCreateRegApp()
    {
      var result = AcadTestRunner.TestRunner.RunTest<RegAppContainerTests>("CreateRegApp");
      if (!result.Passed) Assert.Fail(result.Message);
    }
    
    [TestMethod]
    [TestCategory("AcadTest")]
    public void TestAddRegApp()
    {
      var result = AcadTestRunner.TestRunner.RunTest<RegAppContainerTests>("AddRegApp");
      if (!result.Passed) Assert.Fail(result.Message);
    }
  }
}
