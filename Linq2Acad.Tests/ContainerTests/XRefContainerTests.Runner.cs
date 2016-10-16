using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linq2Acad.Tests
{
  [TestClass]
  [DebuggerStepThrough]
  public class XRefContainerTests_
  {
    [TestMethod]
    [TestCategory("AutoCAD Unit Tests")]
    public void TestAttachAndDetachXRef()
    {
      var result = AcadTestRunner.TestRunner.RunTest<BlockContainerTests>("TestCreateBlock");
      if (!result.Passed) Assert.Fail(result.Message);
    }
  }
}
