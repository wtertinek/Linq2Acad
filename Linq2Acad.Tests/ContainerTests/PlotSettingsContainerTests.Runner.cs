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
    [TestCategory("AutoCAD Tests")]
    public void TestCreatePlotSettings()
    {
      var result = AcadTestRunner.TestRunner.RunTest<PlotSettingsContainerTests>("TestCreatePlotSettings");
      if (!result.Passed) Assert.Fail(result.Message);
    }
  }
}
