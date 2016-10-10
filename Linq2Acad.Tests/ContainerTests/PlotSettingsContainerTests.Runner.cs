using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linq2Acad.Tests
{
  [TestClass]
  [DebuggerStepThrough]
  public partial class PlotSettingsContainerTests
  {
    [TestMethod]
    [TestCategory("AcadTest")]
    public void TestCreatePlotSettings()
    {
      var result = AcadTestRunner.TestRunner.RunTest<PlotSettingsContainerTests>("CreatePlotSettings");
      if (!result.Passed) Assert.Fail(result.Message);
    }
  }
}
