using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linq2Acad.Tests
{
  [TestClass]
  public partial class PlotSettingsContainerTests
  {
    [TestMethod]
    [TestCategory("AcadTest")]
    public void TestCreatePlotSettings()
    {
      var result = AcadTestRunner.TestRunner.RunTest(typeof(PlotSettingsContainerTests).Assembly.Location, "PlotSettingsContainerTests", "CreatePlotSettings");
      Assert.IsTrue(result.Passed, result.Message);
    }
  }
}
