using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  [TestClass]
  public class TextStyleContainerTests
  {
    [TestMethod]
    [TestCategory("AcadTests")]
    public void TestCreateTextStyle()
    {
      var result = TestRunner.Test("TestCreateTextStyle", "Linq2Acad.Tests.Acad.dll");
      Assert.IsTrue(result.Passed, result.Message);
    }
    
    [TestMethod]
    [TestCategory("AcadTests")]
    public void TestAddTextStyle()
    {
      var result = TestRunner.Test("TestAddTextStyle", "Linq2Acad.Tests.Acad.dll");
      Assert.IsTrue(result.Passed, result.Message);
    }
  }
}
