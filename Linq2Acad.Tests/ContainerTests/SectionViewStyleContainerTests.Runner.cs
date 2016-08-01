using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linq2Acad.Tests
{
  [TestClass]
  public partial class SectionViewStyleContainerTests
  {
    [TestMethod]
    [TestCategory("AcadTest")]
    public void TestCreateSectionViewStyle()
    {
      var result = AcadTestRunner.TestRunner.Test(typeof(SectionViewStyleContainerTests).Assembly.Location, "SectionViewStyleContainerTests", "TestCreateSectionViewStyle");
      Assert.IsTrue(result.Passed, result.Message);
    }
  }
}
