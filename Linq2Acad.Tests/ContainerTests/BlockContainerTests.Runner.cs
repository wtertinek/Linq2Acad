using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linq2Acad.Tests
{
  [TestClass]
  [DebuggerStepThrough]
  public class BlockContainerTests_
  {
    [TestMethod]
    [TestCategory("AutoCAD Tests")]
    public void TestCreateBlock()
    {
      var result = AcadTestRunner.TestRunner.RunTest<BlockContainerTests>("TestCreateBlock");
      if (!result.Passed) Assert.Fail(result.Message);
    }
    
    [TestMethod]
    [TestCategory("AutoCAD Tests")]
    public void TestAddBlock()
    {
      var result = AcadTestRunner.TestRunner.RunTest<BlockContainerTests>("TestAddBlock");
      if (!result.Passed) Assert.Fail(result.Message);
    }
  }
}
