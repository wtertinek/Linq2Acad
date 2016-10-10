using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linq2Acad.Tests
{
  [TestClass]
  [DebuggerStepThrough]
  public partial class LinetypeContainerTests
  {
    [TestMethod]
    [TestCategory("AcadTest")]
    public void TestCreateLinetype()
    {
      var result = AcadTestRunner.TestRunner.RunTest<LinetypeContainerTests>("CreateLinetype");
      if (!result.Passed) Assert.Fail(result.Message);
    }
    
    [TestMethod]
    [TestCategory("AcadTest")]
    public void TestAddLinetype()
    {
      var result = AcadTestRunner.TestRunner.RunTest<LinetypeContainerTests>("AddLinetype");
      if (!result.Passed) Assert.Fail(result.Message);
    }
  }
}
