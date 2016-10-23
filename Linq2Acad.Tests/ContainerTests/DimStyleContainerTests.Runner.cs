using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linq2Acad.Tests
{
  [TestClass]
  [DebuggerStepThrough]
  public class DimStyleContainerTests_
  {
    [TestMethod]
    [TestCategory("Container Tests")]
    public void TestCreateDimStyle()
    {
      var result = AcadTestRunner.TestRunner.RunTest(typeof(DimStyleContainerTests), "TestCreateDimStyle");
      
      if (!result.Passed)
      {
        result.DebugPrintFullOutput("TestCreateDimStyle");
        Assert.Fail(result.Message);
      }
    }
    
    [TestMethod]
    [TestCategory("Container Tests")]
    public void TestAddDimStyle()
    {
      var result = AcadTestRunner.TestRunner.RunTest(typeof(DimStyleContainerTests), "TestAddDimStyle");

      if (!result.Passed)
      {
        result.DebugPrintFullOutput("TestAddDimStyle");
        Assert.Fail(result.Message);
      }
    }
  }
}
