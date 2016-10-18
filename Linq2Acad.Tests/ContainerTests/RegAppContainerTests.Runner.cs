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
    [TestCategory("Container Tests")]
    public void TestCreateRegApp()
    {
      var result = AcadTestRunner.TestRunner.RunTest<RegAppContainerTests>("TestCreateRegApp");
      
      if (!result.Passed)
      {
        result.DebugPrintFullOutput("TestCreateRegApp");
        Assert.Fail(result.Message);
      }
    }
    
    [TestMethod]
    [TestCategory("Container Tests")]
    public void TestAddRegApp()
    {
      var result = AcadTestRunner.TestRunner.RunTest<RegAppContainerTests>("TestAddRegApp");

      if (!result.Passed)
      {
        result.DebugPrintFullOutput("TestAddRegApp");
        Assert.Fail(result.Message);
      }
    }
  }
}
