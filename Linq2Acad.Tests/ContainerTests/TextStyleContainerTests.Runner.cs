using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linq2Acad.Tests
{
  [TestClass]
  [DebuggerStepThrough]
  public partial class TextStyleContainerTests
  {
    [TestMethod]
    [TestCategory("AcadTest")]
    public void TestCreateTextStyle()
    {
      var result = AcadTestRunner.TestRunner.RunTest<TextStyleContainerTests>("CreateTextStyle");
      if (!result.Passed) Assert.Fail(result.Message);
    }
    
    [TestMethod]
    [TestCategory("AcadTest")]
    public void TestAddTextStyle()
    {
      var result = AcadTestRunner.TestRunner.RunTest<TextStyleContainerTests>("AddTextStyle");
      if (!result.Passed) Assert.Fail(result.Message);
    }
  }
}
