using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  [TestClass]
  public class LayerTableRecordTests
  {
    [TestMethod]
    [TestCategory("AcadTests")]
    public void TestCreateLayerTableRecord()
    {
      var result = TestRunner.Test("TestCreateLayerTableRecord", "Linq2Acad.Tests.Acad.dll");
      Assert.IsTrue(result.Passed, result.Message);
    }
    
    [TestMethod]
    [TestCategory("AcadTests")]
    public void TestAddLayerTableRecord()
    {
      var result = TestRunner.Test("TestAddLayerTableRecord", "Linq2Acad.Tests.Acad.dll");
      Assert.IsTrue(result.Passed, result.Message);
    }
  }
}
