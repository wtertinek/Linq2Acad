using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linq2Acad.Tests
{
  [TestClass]
  [DebuggerStepThrough]
  public class BlockContainerTests_
  {
    [TestMethod]
    [TestCategory("Container Tests")]
    public void TestCreateBlock()
    {
      var result = AcadTestRunner.TestRunner.RunTest(typeof(BlockContainerTests), "TestCreateBlock");
      
      if (!result.Passed)
      {
        result.DebugPrintFullOutput("TestCreateBlock");
        Assert.Fail(result.Message);
      }
    }
    
    [TestMethod]
    [TestCategory("Container Tests")]
    public void TestAddBlock()
    {
      var result = AcadTestRunner.TestRunner.RunTest(typeof(BlockContainerTests), "TestAddBlock");

      if (!result.Passed)
      {
        result.DebugPrintFullOutput("TestAddBlock");
        Assert.Fail(result.Message);
      }
    }
  }
}
