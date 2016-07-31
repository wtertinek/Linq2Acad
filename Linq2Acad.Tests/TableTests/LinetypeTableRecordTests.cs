using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  [TestClass]
  public class LinetypeTableRecordTests
  {
    [TestMethod]
    [TestCategory("AcadTests")]
    public void TestCreateLinetypeTableRecord()
    {
      var result = TestRunner.Test("TestCreateLinetypeTableRecord", "Linq2Acad.Tests.Acad.dll");
      Assert.IsTrue(result.Passed, result.Message);
    }
    
    [TestMethod]
    [TestCategory("AcadTests")]
    public void TestAddLinetypeTableRecord()
    {
      var result = TestRunner.Test("TestAddLinetypeTableRecord", "Linq2Acad.Tests.Acad.dll");
      Assert.IsTrue(result.Passed, result.Message);
    }
  }
}
