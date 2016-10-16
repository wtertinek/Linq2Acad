using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linq2Acad.Tests
{
  [TestClass]
  [DebuggerStepThrough]
  public class MaterialContainerTests_
  {
    [TestMethod]
    [TestCategory("AutoCAD Unit Tests")]
    public void TestCreateMaterial()
    {
      var result = AcadTestRunner.TestRunner.RunTest<MaterialContainerTests>("TestCreateMaterial");
      if (!result.Passed) Assert.Fail(result.Message);
    }
  }
}
