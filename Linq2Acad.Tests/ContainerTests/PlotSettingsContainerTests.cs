using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  [TestClass]
  public class PlotSettingsContainerTests
  {
    [TestMethod]
    [TestCategory("AcadTests")]
    public void TestCreatePlotSettings()
    {
      var result = TestRunner.Test("TestCreatePlotSettings", "Linq2Acad.Tests.Acad.dll");
      Assert.IsTrue(result.Passed, result.Message);
    }
  }
}
