using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  [TestClass]
  public class TextStyleTableRecordTests
  {
    [TestMethod]
    [TestCategory("AcadTests")]
    public void TestCreateTextStyleTableRecord()
    {
      var result = TestRunner.Test("TestCreateTextStyleTableRecord", "Linq2Acad.Tests.Acad.dll");
      Assert.IsTrue(result.Passed, result.Message);
    }
    
    [TestMethod]
    [TestCategory("AcadTests")]
    public void TestAddTextStyleTableRecord()
    {
      var result = TestRunner.Test("TestAddTextStyleTableRecord", "Linq2Acad.Tests.Acad.dll");
      Assert.IsTrue(result.Passed, result.Message);
    }
  }
}
