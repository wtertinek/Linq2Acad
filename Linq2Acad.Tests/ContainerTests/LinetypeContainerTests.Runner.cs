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
    [TestCategory("AutoCAD Unit Tests")]
    public void TestCreateLinetype()
    {
      var result = AcadTestRunner.TestRunner.RunTest<LinetypeContainerTests>("TestCreateLinetype");
      if (!result.Passed) Assert.Fail(result.Message);
    }
    
    [TestMethod]
    [TestCategory("AutoCAD Unit Tests")]
    public void TestAddLinetype()
    {
      var result = AcadTestRunner.TestRunner.RunTest<LinetypeContainerTests>("TestAddLinetype");
      if (!result.Passed) Assert.Fail(result.Message);
    }
  }
}
