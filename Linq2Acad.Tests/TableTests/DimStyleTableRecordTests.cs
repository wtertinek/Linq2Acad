using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  [TestClass]
  public class DimStyleTableRecordTests
  {
    [TestMethod]
    [TestCategory("AcadTests")]
    public void TestCreateDimStyleTableRecord()
    {
      var result = TestRunner.Test("TestCreateDimStyleTableRecord", "Linq2Acad.Tests.Acad.dll");
      Assert.IsTrue(result.Passed, result.Message);
    }
    
    [TestMethod]
    [TestCategory("AcadTests")]
    public void TestAddDimStyleTableRecord()
    {
      var result = TestRunner.Test("TestAddDimStyleTableRecord", "Linq2Acad.Tests.Acad.dll");
      Assert.IsTrue(result.Passed, result.Message);
    }
  }
}
