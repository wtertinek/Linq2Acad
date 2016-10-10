using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linq2Acad.Tests
{
  [TestClass]
  [DebuggerStepThrough]
  public partial class MaterialContainerTests
  {
    [TestMethod]
    [TestCategory("AcadTest")]
    public void TestCreateMaterial()
    {
      var result = AcadTestRunner.TestRunner.RunTest<MaterialContainerTests>("CreateMaterial");
      if (!result.Passed) Assert.Fail(result.Message);
    }
  }
}
