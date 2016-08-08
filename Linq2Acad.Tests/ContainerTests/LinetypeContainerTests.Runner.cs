using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linq2Acad.Tests
{
  [TestClass]
  public partial class LinetypeContainerTests
  {
    [TestMethod]
    [TestCategory("AcadTest")]
    public void TestCreateLinetype()
    {
      var result = AcadTestRunner.TestRunner.RunTest(typeof(LinetypeContainerTests).Assembly.Location, "LinetypeContainerTests", "CreateLinetype");
      Assert.IsTrue(result.Passed, result.Message);
    }
    
    [TestMethod]
    [TestCategory("AcadTest")]
    public void TestAddLinetype()
    {
      var result = AcadTestRunner.TestRunner.RunTest(typeof(LinetypeContainerTests).Assembly.Location, "LinetypeContainerTests", "AddLinetype");
      Assert.IsTrue(result.Passed, result.Message);
    }
  }
}
