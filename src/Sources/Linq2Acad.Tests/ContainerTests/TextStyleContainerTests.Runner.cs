using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linq2Acad.Tests
{
  [TestClass]
  [DebuggerStepThrough]
  public class TextStyleContainerTests_
  {
    [TestMethod]
    [TestCategory("Container Tests")]
    public void TestCreateTextStyle()
    {
      var result = AcadTestRunner.TestRunner.RunTest(typeof(TextStyleContainerTests), "TestCreateTextStyle");
      
      if (!result.Passed)
      {
        result.DebugPrintFullOutput("TestCreateTextStyle");
        Assert.Fail(result.Message);
      }
    }
    
    [TestMethod]
    [TestCategory("Container Tests")]
    public void TestAddTextStyle()
    {
      var result = AcadTestRunner.TestRunner.RunTest(typeof(TextStyleContainerTests), "TestAddTextStyle");

      if (!result.Passed)
      {
        result.DebugPrintFullOutput("TestAddTextStyle");
        Assert.Fail(result.Message);
      }
    }
  }
}
