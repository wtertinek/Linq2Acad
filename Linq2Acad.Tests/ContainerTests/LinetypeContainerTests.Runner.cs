using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linq2Acad.Tests
{
  [TestClass]
  [DebuggerStepThrough]
  public class LinetypeContainerTests_
  {
    [TestMethod]
    [TestCategory("Container Tests")]
    public void TestCreateLinetype()
    {
      var result = AcadTestRunner.TestRunner.RunTest<LinetypeContainerTests>("TestCreateLinetype");
      
      if (!result.Passed)
      {
        result.DebugPrintFullOutput("TestCreateLinetype");
        Assert.Fail(result.Message);
      }
    }
    
    [TestMethod]
    [TestCategory("Container Tests")]
    public void TestAddLinetype()
    {
      var result = AcadTestRunner.TestRunner.RunTest<LinetypeContainerTests>("TestAddLinetype");

      if (!result.Passed)
      {
        result.DebugPrintFullOutput("TestAddLinetype");
        Assert.Fail(result.Message);
      }
    }
  }
}
