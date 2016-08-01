using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linq2Acad.Tests
{
  [TestClass]
  public partial class TextStyleContainerTests
  {
    [TestMethod]
    [TestCategory("AcadTest")]
    public void TestCreateTextStyle()
    {
      var result = AcadTestRunner.TestRunner.Test(typeof(TextStyleContainerTests).Assembly.Location, "TextStyleContainerTests", "TestCreateTextStyle");
      Assert.IsTrue(result.Passed, result.Message);
    }
    
    [TestMethod]
    [TestCategory("AcadTest")]
    public void TestAddTextStyle()
    {
      var result = AcadTestRunner.TestRunner.Test(typeof(TextStyleContainerTests).Assembly.Location, "TextStyleContainerTests", "TestAddTextStyle");
      Assert.IsTrue(result.Passed, result.Message);
    }
  }
}
