using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linq2Acad.Tests
{
  [TestClass]
  [DebuggerStepThrough]
  public partial class SectionViewStyleContainerTests
  {
    [TestMethod]
    [TestCategory("AcadTest")]
    public void TestCreateSectionViewStyle()
    {
      var result = AcadTestRunner.TestRunner.RunTest<SectionViewStyleContainerTests>("CreateSectionViewStyle");
      if (!result.Passed) Assert.Fail(result.Message);
    }
  }
}
