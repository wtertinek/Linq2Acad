using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  [TestClass]
  public class DimStyleContainerTests
  {
    [TestMethod]
    [TestCategory("AcadTests")]
    public void TestCreateDimStyle()
    {
      var result = TestRunner.Test("TestCreateDimStyle", "Linq2Acad.Tests.Acad.dll");
      Assert.IsTrue(result.Passed, result.Message);
    }
    
    [TestMethod]
    [TestCategory("AcadTests")]
    public void TestAddDimStyle()
    {
      var result = TestRunner.Test("TestAddDimStyle", "Linq2Acad.Tests.Acad.dll");
      Assert.IsTrue(result.Passed, result.Message);
    }
  }
}
