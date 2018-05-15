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
    [TestCategory("Container Tests")]
    public void TestAttachXRef()
    {
      var result = AcadTestRunner.TestRunner.RunTest(typeof(XRefContainerTests), "TestAttachXRef");
      if (!result.Passed) Assert.Fail(result.Message);
    }

    [TestMethod]
    [TestCategory("Container Tests")]
    public void TestOverlayXRef()
    {
      var result = AcadTestRunner.TestRunner.RunTest(typeof(XRefContainerTests), "TestOverlayXRef");
      if (!result.Passed) Assert.Fail(result.Message);
    }

    [TestMethod]
    [TestCategory("Container Tests")]
    public void TestDetachXRef()
    {
      var result = AcadTestRunner.TestRunner.RunTest(typeof(XRefContainerTests), "TestDetachXRef");
      if (!result.Passed) Assert.Fail(result.Message);
    }
  }
}
