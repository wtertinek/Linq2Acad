using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linq2Acad.Tests
{
  [TestClass]
  [DebuggerStepThrough]
  public class UcsContainerTests_
  {
    [TestMethod]
    [TestCategory("Container Tests")]
    public void TestCreateUcs()
    {
      var result = AcadTestRunner.TestRunner.RunTest(typeof(UcsContainerTests), "TestCreateUcs");
      
      if (!result.Passed)
      {
        result.DebugPrintFullOutput("TestCreateUcs");
        Assert.Fail(result.Message);
      }
    }
    
    [TestMethod]
    [TestCategory("Container Tests")]
    public void TestAddUcs()
    {
      var result = AcadTestRunner.TestRunner.RunTest(typeof(UcsContainerTests), "TestAddUcs");

      if (!result.Passed)
      {
        result.DebugPrintFullOutput("TestAddUcs");
        Assert.Fail(result.Message);
      }
    }
  }
}
