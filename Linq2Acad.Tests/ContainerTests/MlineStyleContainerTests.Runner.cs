using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linq2Acad.Tests
{
  [TestClass]
  [DebuggerStepThrough]
  public partial class MlineStyleContainerTests
  {
    [TestMethod]
    [TestCategory("AcadTest")]
    public void TestCreateMlineStyle()
    {
      var result = AcadTestRunner.TestRunner.RunTest<MlineStyleContainerTests>("CreateMlineStyle");
      if (!result.Passed) Assert.Fail(result.Message);
    }
  }
}
