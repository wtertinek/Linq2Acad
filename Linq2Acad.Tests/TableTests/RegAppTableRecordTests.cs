using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  [TestClass]
  public class RegAppTableRecordTests
  {
    [TestMethod]
    [TestCategory("AcadTests")]
    public void TestCreateRegAppTableRecord()
    {
      var result = TestRunner.Test("TestCreateRegAppTableRecord", "Linq2Acad.Tests.Acad.dll");
      Assert.IsTrue(result.Passed, result.Message);
    }
    
    [TestMethod]
    [TestCategory("AcadTests")]
    public void TestAddRegAppTableRecord()
    {
      var result = TestRunner.Test("TestAddRegAppTableRecord", "Linq2Acad.Tests.Acad.dll");
      Assert.IsTrue(result.Passed, result.Message);
    }
  }
}
