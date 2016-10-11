using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linq2Acad.Tests
{
  [TestClass]
  [DebuggerStepThrough]
  public class TextStyleContainerTests_
  {
    [TestMethod]
    [TestCategory("AutoCAD Tests")]
    public void TestCreateTextStyle()
    {
      var result = AcadTestRunner.TestRunner.RunTest<TextStyleContainerTests>("TestCreateTextStyle");
      if (!result.Passed) Assert.Fail(result.Message);
    }
    
    [TestMethod]
    [TestCategory("AutoCAD Tests")]
    public void TestAddTextStyle()
    {
      var result = AcadTestRunner.TestRunner.RunTest<TextStyleContainerTests>("TestAddTextStyle");
      if (!result.Passed) Assert.Fail(result.Message);
    }
  }
}
