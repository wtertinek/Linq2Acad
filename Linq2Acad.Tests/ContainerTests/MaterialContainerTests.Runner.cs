using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linq2Acad.Tests
{
  [TestClass]
  public partial class MaterialContainerTests
  {
    [TestMethod]
    [TestCategory("AcadTest")]
    public void TestCreateMaterial()
    {
      var result = AcadTestRunner.TestRunner.RunTest<MaterialContainerTests>("CreateMaterial");
      Assert.IsTrue(result.Passed, result.Message);
    }
  }
}
