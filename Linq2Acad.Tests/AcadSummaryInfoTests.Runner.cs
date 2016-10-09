using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linq2Acad.Tests
{
  public partial class AcadSummaryInfoTests
  {
    [TestClass]
    public partial class BlockContainerTests
    {
      [TestMethod]
      [TestCategory("AcadTest")]
      public void TestSetCustomProperties()
      {
        var result = AcadTestRunner.TestRunner.RunTest<AcadSummaryInfoTests>("SetCustomProperties");
        Assert.IsTrue(result.Passed, result.Message);
      }


      [TestMethod]
      [TestCategory("AcadTest")]
      public void TestSetAuthor()
      {
        var result = AcadTestRunner.TestRunner.RunTest<AcadSummaryInfoTests>("SetAuthor");
        Assert.IsTrue(result.Passed, result.Message);
      }

      [TestMethod]
      [TestCategory("AcadTest")]
      public void TestSetComments()
      {
        var result = AcadTestRunner.TestRunner.RunTest<AcadSummaryInfoTests>("SetComments");
        Assert.IsTrue(result.Passed, result.Message);
      }

      [TestMethod]
      [TestCategory("AcadTest")]
      public void TestSetHyperlinkBase()
      {
        var result = AcadTestRunner.TestRunner.RunTest<AcadSummaryInfoTests>("SetHyperlinkBase");
        Assert.IsTrue(result.Passed, result.Message);
      }

      [TestMethod]
      [TestCategory("AcadTest")]
      public void TestSetKeywords()
      {
        var result = AcadTestRunner.TestRunner.RunTest<AcadSummaryInfoTests>("SetKeywords");
        Assert.IsTrue(result.Passed, result.Message);
      }

      [TestMethod]
      [TestCategory("AcadTest")]
      public void TestSetLastSavedBy()
      {
        var result = AcadTestRunner.TestRunner.RunTest<AcadSummaryInfoTests>("SetLastSavedBy");
        Assert.IsTrue(result.Passed, result.Message);
      }

      [TestMethod]
      [TestCategory("AcadTest")]
      public void TestSetRevisionNumber()
      {
        var result = AcadTestRunner.TestRunner.RunTest<AcadSummaryInfoTests>("SetRevisionNumber");
        Assert.IsTrue(result.Passed, result.Message);
      }

      [TestMethod]
      [TestCategory("AcadTest")]
      public void TestSetSubject()
      {
        var result = AcadTestRunner.TestRunner.RunTest<AcadSummaryInfoTests>("SetSubject");
        Assert.IsTrue(result.Passed, result.Message);
      }

      [TestMethod]
      [TestCategory("AcadTest")]
      public void TestSetTitle()
      {
        var result = AcadTestRunner.TestRunner.RunTest<AcadSummaryInfoTests>("SetTitle");
        Assert.IsTrue(result.Passed, result.Message);
      }
    }
  }
}
