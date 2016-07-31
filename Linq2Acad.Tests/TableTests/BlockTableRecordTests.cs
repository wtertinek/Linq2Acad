using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  [TestClass]
  public class BlockTableRecordTests
  {
    [TestMethod]
    [TestCategory("AcadTests")]
    public void TestCreateBlockTableRecord()
    {
      var result = TestRunner.Test("TestCreateBlockTableRecord", "Linq2Acad.Tests.Acad.dll");
      Assert.IsTrue(result.Passed, result.Message);
    }
    
    [TestMethod]
    [TestCategory("AcadTests")]
    public void TestAddBlockTableRecord()
    {
      var result = TestRunner.Test("TestAddBlockTableRecord", "Linq2Acad.Tests.Acad.dll");
      Assert.IsTrue(result.Passed, result.Message);
    }
  }
}
