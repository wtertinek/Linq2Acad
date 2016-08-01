using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linq2Acad.Tests
{
  [TestClass]
  public partial class LayerContainerTests
  {
    [TestMethod]
    [TestCategory("AcadTest")]
    public void TestCreateLayer()
    {
      var result = AcadTestRunner.TestRunner.Test(typeof(LayerContainerTests).Assembly.Location, "LayerContainerTests", "TestCreateLayer");
      Assert.IsTrue(result.Passed, result.Message);
    }
    
    [TestMethod]
    [TestCategory("AcadTest")]
    public void TestAddLayer()
    {
      var result = AcadTestRunner.TestRunner.Test(typeof(LayerContainerTests).Assembly.Location, "LayerContainerTests", "TestAddLayer");
      Assert.IsTrue(result.Passed, result.Message);
    }
  }
}
