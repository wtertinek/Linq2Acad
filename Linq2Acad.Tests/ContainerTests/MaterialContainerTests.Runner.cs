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
      var result = AcadTestRunner.TestRunner.Test(typeof(MaterialContainerTests).Assembly.Location, "MaterialContainerTests", "TestCreateMaterial");
      Assert.IsTrue(result.Passed, result.Message);
    }
  }
}
