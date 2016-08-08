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
      var result = AcadTestRunner.TestRunner.RunTest<BlockContainerTests>("CreateBlock");
      Assert.IsTrue(result.Passed, result.Message);
    }
    
    [TestMethod]
    [TestCategory("AcadTest")]
    public void TestAddBlock()
    {
      var result = AcadTestRunner.TestRunner.RunTest<BlockContainerTests>("AddBlock");
      Assert.IsTrue(result.Passed, result.Message);
    }
  }
}
