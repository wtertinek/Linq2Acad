using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linq2Acad.Tests
{
  [TestClass]
  [DebuggerStepThrough]
  public class DetailViewStyleContainerTests_
  {
    [TestMethod]
    [TestCategory("AutoCAD Tests")]
    public void TestCreateDetailViewStyle()
    {
      var result = AcadTestRunner.TestRunner.RunTest<DetailViewStyleContainerTests>("TestCreateDetailViewStyle");
      if (!result.Passed) Assert.Fail(result.Message);
    }
  }
}
