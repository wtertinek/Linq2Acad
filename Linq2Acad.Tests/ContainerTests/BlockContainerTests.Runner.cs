using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linq2Acad.Tests
{
  [TestClass]
  [DebuggerStepThrough]
  public partial class BlockContainerTests
  {
    [TestMethod]
    [TestCategory("AcadTest")]
    public void TestCreateBlock()
    {
      var result = AcadTestRunner.TestRunner.RunTest<BlockContainerTests>("CreateBlock");
      if (!result.Passed) Assert.Fail(result.Message);
    }
    
    [TestMethod]
    [TestCategory("AcadTest")]
    public void TestAddBlock()
    {
      var result = AcadTestRunner.TestRunner.RunTest<BlockContainerTests>("AddBlock");
      if (!result.Passed) Assert.Fail(result.Message);
    }
  }
}
