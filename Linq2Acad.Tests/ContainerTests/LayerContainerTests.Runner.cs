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
    [TestCategory("AutoCAD Tests")]
    public void TestCreateLayer()
    {
      var result = AcadTestRunner.TestRunner.RunTest<LayerContainerTests>("TestCreateLayer");
      if (!result.Passed) Assert.Fail(result.Message);
    }
    
    [TestMethod]
    [TestCategory("AutoCAD Tests")]
    public void TestAddLayer()
    {
      var result = AcadTestRunner.TestRunner.RunTest<LayerContainerTests>("TestAddLayer");
      if (!result.Passed) Assert.Fail(result.Message);
    }
  }
}
