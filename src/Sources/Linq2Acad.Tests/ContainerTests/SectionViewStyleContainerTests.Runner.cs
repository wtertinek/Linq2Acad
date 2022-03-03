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
    [TestCategory("Container Tests")]
    public void TestCreateSectionViewStyle()
    {
      var result = AcadTestRunner.TestRunner.RunTest(typeof(SectionViewStyleContainerTests), "TestCreateSectionViewStyle");

      if (!result.Passed)
      {
        result.DebugPrintFullOutput("TestCreateSectionViewStyle");
        Assert.Fail(result.Message);
      }
    }
  }
}
