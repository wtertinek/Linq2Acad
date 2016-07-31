using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  [TestClass]
  public class MaterialTests
  {
    [TestMethod]
    [TestCategory("AcadTests")]
    public void TestCreateMaterial()
    {
      var result = TestRunner.Test("TestCreateMaterial", "Linq2Acad.Tests.Acad.dll");
      Assert.IsTrue(result.Passed, result.Message);
    }
  }
}
