using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linq2Acad.Tests
{
  [TestClass]
  [DebuggerStepThrough]
  public class AcadSummaryInfoTests_
  {
    [TestMethod]
    [TestCategory("AutoCAD Tests")]
    public void TestSetCustomProperties()
    {
      var result = AcadTestRunner.TestRunner.RunTest<AcadSummaryInfoTests>("SetCustomProperties");

      if (!result.Passed)
      {
        result.DebugPrintFullOutput("SetCustomProperties");
        Assert.Fail(result.Message);
      }
    }

    [TestMethod]
    [TestCategory("AutoCAD Tests")]
    public void TestSetAuthor()
    {
      var result = AcadTestRunner.TestRunner.RunTest<AcadSummaryInfoTests>("SetAuthor");

      if (!result.Passed)
      {
        result.DebugPrintFullOutput("SetAuthor");
        Assert.Fail(result.Message);
      }
    }

    [TestMethod]
    [TestCategory("AutoCAD Tests")]
    public void TestSetComments()
    {
      var result = AcadTestRunner.TestRunner.RunTest<AcadSummaryInfoTests>("SetComments");

      if (!result.Passed)
      {
        result.DebugPrintFullOutput("SetComments");
        Assert.Fail(result.Message);
      }
    }

    [TestMethod]
    [TestCategory("AutoCAD Tests")]
    public void TestSetHyperlinkBase()
    {
      var result = AcadTestRunner.TestRunner.RunTest<AcadSummaryInfoTests>("SetHyperlinkBase");

      if (!result.Passed)
      {
        result.DebugPrintFullOutput("SetHyperlinkBase");
        Assert.Fail(result.Message);
      }
    }

    [TestMethod]
    [TestCategory("AutoCAD Tests")]
    public void TestSetKeywords()
    {
      var result = AcadTestRunner.TestRunner.RunTest<AcadSummaryInfoTests>("SetKeywords");

      if (!result.Passed)
      {
        result.DebugPrintFullOutput("SetKeywords");
        Assert.Fail(result.Message);
      }
    }

    [TestMethod]
    [TestCategory("AutoCAD Tests")]
    public void TestSetLastSavedBy()
    {
      var result = AcadTestRunner.TestRunner.RunTest<AcadSummaryInfoTests>("SetLastSavedBy");

      if (!result.Passed)
      {
        result.DebugPrintFullOutput("SetLastSavedBy");
        Assert.Fail(result.Message);
      }
    }

    [TestMethod]
    [TestCategory("AutoCAD Tests")]
    public void TestSetRevisionNumber()
    {
      var result = AcadTestRunner.TestRunner.RunTest<AcadSummaryInfoTests>("SetRevisionNumber");

      if (!result.Passed)
      {
        result.DebugPrintFullOutput("SetRevisionNumber");
        Assert.Fail(result.Message);
      }
    }

    [TestMethod]
    [TestCategory("AutoCAD Tests")]
    public void TestSetSubject()
    {
      var result = AcadTestRunner.TestRunner.RunTest<AcadSummaryInfoTests>("SetSubject");

      if (!result.Passed)
      {
        result.DebugPrintFullOutput("SetSubject");
        Assert.Fail(result.Message);
      }
    }

    [TestMethod]
    [TestCategory("AutoCAD Tests")]
    public void TestSetTitle()
    {
      var result = AcadTestRunner.TestRunner.RunTest<AcadSummaryInfoTests>("SetTitle");

      if (!result.Passed)
      {
        result.DebugPrintFullOutput("SetTitle");
        Assert.Fail(result.Message);
      }
    }
  }
}
