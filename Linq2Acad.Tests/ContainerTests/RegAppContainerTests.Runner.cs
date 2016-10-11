using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linq2Acad.Tests
{
  [TestClass]
  [DebuggerStepThrough]
  public class RegAppContainerTests_
  {
    [TestMethod]
    [TestCategory("AutoCAD Tests")]
    public void TestCreateRegApp()
    {
      var result = AcadTestRunner.TestRunner.RunTest<RegAppContainerTests>("TestCreateRegApp");
      if (!result.Passed) Assert.Fail(result.Message);
    }
    
    [TestMethod]
    [TestCategory("AutoCAD Tests")]
    public void TestAddRegApp()
    {
      var result = AcadTestRunner.TestRunner.RunTest<RegAppContainerTests>("TestAddRegApp");
      if (!result.Passed) Assert.Fail(result.Message);
    }
  }
}
