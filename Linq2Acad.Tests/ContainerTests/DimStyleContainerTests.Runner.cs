using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linq2Acad.Tests
{
  [TestClass]
  public partial class DimStyleContainerTests
  {
    [TestMethod]
    [TestCategory("AcadTest")]
    public void TestCreateDimStyle()
    {
      var result = AcadTestRunner.TestRunner.RunTest<DimStyleContainerTests>("CreateDimStyle");
      Assert.IsTrue(result.Passed, result.Message);
    }
    
    [TestMethod]
    [TestCategory("AcadTest")]
    public void TestAddDimStyle()
    {
      var result = AcadTestRunner.TestRunner.RunTest<DimStyleContainerTests>("AddDimStyle");
      Assert.IsTrue(result.Passed, result.Message);
    }
  }
}
