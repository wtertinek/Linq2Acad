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
      var result = AcadTestRunner.TestRunner.RunTest<ViewContainerTests>("CreateView");
      Assert.IsTrue(result.Passed, result.Message);
    }
    
    [TestMethod]
    [TestCategory("AcadTest")]
    public void TestAddView()
    {
      var result = AcadTestRunner.TestRunner.RunTest<ViewContainerTests>("AddView");
      Assert.IsTrue(result.Passed, result.Message);
    }
  }
}
