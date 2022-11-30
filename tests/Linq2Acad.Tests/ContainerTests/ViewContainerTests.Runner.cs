using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linq2Acad.Tests
{
  [TestClass]
  [DebuggerStepThrough]
  public class ViewContainerTests_
  {
    [TestMethod]
    [TestCategory("Container Tests")]
    public void TestCreateView()
    {
      var result = AcadTestRunner.TestRunner.RunTest(typeof(ViewContainerTests), "TestCreateView");
      
      if (!result.Passed)
      {
        result.DebugPrintFullOutput("TestCreateView");
        Assert.Fail(result.Message);
      }
    }
    
    [TestMethod]
    [TestCategory("Container Tests")]
    public void TestAddView()
    {
      var result = AcadTestRunner.TestRunner.RunTest(typeof(ViewContainerTests), "TestAddView");

      if (!result.Passed)
      {
        result.DebugPrintFullOutput("TestAddView");
        Assert.Fail(result.Message);
      }
    }
  }
}
