using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linq2Acad.Tests
{
  [TestClass]
  [DebuggerStepThrough]
  public partial class UcsContainerTests
  {
    [TestMethod]
    [TestCategory("AcadTest")]
    public void TestCreateUcs()
    {
      var result = AcadTestRunner.TestRunner.RunTest<UcsContainerTests>("CreateUcs");
      if (!result.Passed) Assert.Fail(result.Message);
    }
    
    [TestMethod]
    [TestCategory("AcadTest")]
    public void TestAddUcs()
    {
      var result = AcadTestRunner.TestRunner.RunTest<UcsContainerTests>("AddUcs");
      if (!result.Passed) Assert.Fail(result.Message);
    }
  }
}
