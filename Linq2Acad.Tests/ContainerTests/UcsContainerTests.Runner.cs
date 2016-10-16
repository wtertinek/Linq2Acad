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
    [TestCategory("AutoCAD Unit Tests")]
    public void TestCreateUcs()
    {
      var result = AcadTestRunner.TestRunner.RunTest<UcsContainerTests>("TestCreateUcs");
      if (!result.Passed) Assert.Fail(result.Message);
    }
    
    [TestMethod]
    [TestCategory("AutoCAD Unit Tests")]
    public void TestAddUcs()
    {
      var result = AcadTestRunner.TestRunner.RunTest<UcsContainerTests>("TestAddUcs");
      if (!result.Passed) Assert.Fail(result.Message);
    }
  }
}
