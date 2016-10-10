using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linq2Acad.Tests
{
  [TestClass]
  [DebuggerStepThrough]
  public partial class LayoutContainerTests
  {
    [TestMethod]
    [TestCategory("AcadTest")]
    public void TestCreateLayout()
    {
      var result = AcadTestRunner.TestRunner.RunTest<LayoutContainerTests>("CreateLayout");
      if (!result.Passed) Assert.Fail(result.Message);
    }
  }
}
