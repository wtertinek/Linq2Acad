using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linq2Acad.Tests
{
  [TestClass]
  [DebuggerStepThrough]
  public class PlotSettingsContainerTests_
  {
    [TestMethod]
    [TestCategory("Container Tests")]
    public void TestCreatePlotSettings()
    {
      var result = AcadTestRunner.TestRunner.RunTest(typeof(PlotSettingsContainerTests), "TestCreatePlotSettings");

      if (!result.Passed)
      {
        result.DebugPrintFullOutput("TestCreatePlotSettings");
        Assert.Fail(result.Message);
      }
    }
  }
}
