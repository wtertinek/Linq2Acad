using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linq2Acad.Tests
{
  [TestClass]
  [DebuggerStepThrough]
  public class LayerContainerTests_
  {
    [TestMethod]
    [TestCategory("Container Tests")]
    public void TestCreateLayer()
    {
      var result = AcadTestRunner.TestRunner.RunTest(typeof(LayerContainerTests), "TestCreateLayer");
      
      if (!result.Passed)
      {
        result.DebugPrintFullOutput("TestCreateLayer");
        Assert.Fail(result.Message);
      }
    }
    
    [TestMethod]
    [TestCategory("Container Tests")]
    public void TestAddLayer()
    {
      var result = AcadTestRunner.TestRunner.RunTest(typeof(LayerContainerTests), "TestAddLayer");

      if (!result.Passed)
      {
        result.DebugPrintFullOutput("TestAddLayer");
        Assert.Fail(result.Message);
      }
    }
  }
}
