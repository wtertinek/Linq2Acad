using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linq2Acad.Tests
{
  [TestClass]
  public partial class ViewContainerTests
  {
    [TestMethod]
    [TestCategory("AcadTest")]
    public void TestCreateView()
    {
      var result = AcadTestRunner.TestRunner.Test(typeof(ViewContainerTests).Assembly.Location, "ViewContainerTests", "TestCreateView");
      Assert.IsTrue(result.Passed, result.Message);
    }
    
    [TestMethod]
    [TestCategory("AcadTest")]
    public void TestAddView()
    {
      var result = AcadTestRunner.TestRunner.Test(typeof(ViewContainerTests).Assembly.Location, "ViewContainerTests", "TestAddView");
      Assert.IsTrue(result.Passed, result.Message);
    }
  }
}
