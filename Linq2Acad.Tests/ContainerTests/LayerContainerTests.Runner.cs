using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linq2Acad.Tests
{
  [TestClass]
  [DebuggerStepThrough]
  public partial class LayerContainerTests
  {
    [TestMethod]
    [TestCategory("AcadTest")]
    public void TestCreateLayer()
    {
      var result = AcadTestRunner.TestRunner.RunTest<LayerContainerTests>("CreateLayer");
      if (!result.Passed) Assert.Fail(result.Message);
    }
    
    [TestMethod]
    [TestCategory("AcadTest")]
    public void TestAddLayer()
    {
      var result = AcadTestRunner.TestRunner.RunTest<LayerContainerTests>("AddLayer");
      if (!result.Passed) Assert.Fail(result.Message);
    }
  }
}
