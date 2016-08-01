using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linq2Acad.Tests
{
  [TestClass]
  public partial class BlockContainerTests
  {
    [TestMethod]
    [TestCategory("AcadTest")]
    public void TestCreateBlock()
    {
      var result = AcadTestRunner.TestRunner.Test(typeof(BlockContainerTests).Assembly.Location, "BlockContainerTests", "TestCreateBlock");
      Assert.IsTrue(result.Passed, result.Message);
    }
    
    [TestMethod]
    [TestCategory("AcadTest")]
    public void TestAddBlock()
    {
      var result = AcadTestRunner.TestRunner.Test(typeof(BlockContainerTests).Assembly.Location, "BlockContainerTests", "TestAddBlock");
      Assert.IsTrue(result.Passed, result.Message);
    }
  }
}
