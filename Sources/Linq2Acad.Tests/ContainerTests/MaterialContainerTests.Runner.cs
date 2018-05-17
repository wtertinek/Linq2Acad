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
    [TestCategory("Container Tests")]
    public void TestCreateMaterial()
    {
      var result = AcadTestRunner.TestRunner.RunTest(typeof(MaterialContainerTests), "TestCreateMaterial");

      if (!result.Passed)
      {
        result.DebugPrintFullOutput("TestCreateMaterial");
        Assert.Fail(result.Message);
      }
    }
  }
}
