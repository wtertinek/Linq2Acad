using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linq2Acad.Tests
{
  [TestClass]
  [DebuggerStepThrough]
  public class DimStyleContainerTests_
  {
    [TestMethod]
    [TestCategory("AutoCAD Tests")]
    public void TestCreateDimStyle()
    {
      var result = AcadTestRunner.TestRunner.RunTest<DimStyleContainerTests>("TestCreateDimStyle");
      if (!result.Passed) Assert.Fail(result.Message);
    }
    
    [TestMethod]
    [TestCategory("AutoCAD Tests")]
    public void TestAddDimStyle()
    {
      var result = AcadTestRunner.TestRunner.RunTest<DimStyleContainerTests>("TestAddDimStyle");
      if (!result.Passed) Assert.Fail(result.Message);
    }
  }
}
