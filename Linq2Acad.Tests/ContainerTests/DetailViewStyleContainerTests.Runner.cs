using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linq2Acad.Tests
{
  [TestClass]
  public partial class DetailViewStyleContainerTests
  {
    [TestMethod]
    [TestCategory("AcadTest")]
    public void TestCreateDetailViewStyle()
    {
      var result = AcadTestRunner.TestRunner.RunTest(typeof(DetailViewStyleContainerTests).Assembly.Location, "DetailViewStyleContainerTests", "CreateDetailViewStyle");
      Assert.IsTrue(result.Passed, result.Message);
    }
  }
}
