using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linq2Acad.Tests
{
  [TestClass]
  [DebuggerStepThrough]
  public class SectionViewStyleContainerTests_
  {
    [TestMethod]
    [TestCategory("AutoCAD Unit Tests")]
    public void TestCreateSectionViewStyle()
    {
      var result = AcadTestRunner.TestRunner.RunTest<SectionViewStyleContainerTests>("TestCreateSectionViewStyle");
      if (!result.Passed) Assert.Fail(result.Message);
    }
  }
}
