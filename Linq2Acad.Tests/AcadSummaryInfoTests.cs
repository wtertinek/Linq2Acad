using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AcadTestRunner;
using MsTestAssert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Linq2Acad.Tests
{
  public partial class AcadSummaryInfoTests
  {
    [AcadTest("SetCustomProperties")]
    public void SetCustomProperties()
    {
      using (var db = AcadDatabase.Active())
      {
        db.SummaryInfo.CustomProperties["CustomData1"] = 42;
      }

      using (var db = AcadDatabase.Active())
      {
        MsTestAssert.IsTrue(db.SummaryInfo.CustomProperties.ContainsKey("CustomData1"));
        MsTestAssert.AreEqual(42, db.SummaryInfo.CustomProperties["CustomData1"]);
      }
    }

    [AcadTest("SetAuthor")]
    public void SetAuthor()
    {
      using (var db = AcadDatabase.Active())
      {
        db.SummaryInfo.Author = "jdoe";
      }

      using (var db = AcadDatabase.Active())
      {
        MsTestAssert.AreEqual("jdoe", db.SummaryInfo.Author);
      }
    }

    [AcadTest("SetComments")]
    public void SetComments()
    {
      using (var db = AcadDatabase.Active())
      {
        db.SummaryInfo.Comments = "This is a comment";
      }

      using (var db = AcadDatabase.Active())
      {
        MsTestAssert.AreEqual("This is a comment", db.SummaryInfo.Comments);
      }
    }

    [AcadTest("SetHyperlinkBase")]
    public void SetHyperlinkBase()
    {
      using (var db = AcadDatabase.Active())
      {
        db.SummaryInfo.HyperlinkBase = "https://www.github.com";
      }

      using (var db = AcadDatabase.Active())
      {
        MsTestAssert.AreEqual("https://www.github.com", db.SummaryInfo.HyperlinkBase);
      }
    }

    [AcadTest("SetKeywords")]
    public void SetKeywords()
    {
      using (var db = AcadDatabase.Active())
      {
        db.SummaryInfo.Keywords = "ACAD";
      }

      using (var db = AcadDatabase.Active())
      {
        MsTestAssert.AreEqual("ACAD", db.SummaryInfo.Keywords);
      }
    }

    [AcadTest("SetLastSavedBy")]
    public void SetLastSavedBy()
    {
      using (var db = AcadDatabase.Active())
      {
        db.SummaryInfo.LastSavedBy = "jdoe";
      }

      using (var db = AcadDatabase.Active())
      {
        MsTestAssert.AreEqual("jdoe", db.SummaryInfo.LastSavedBy);
      }
    }

    [AcadTest("SetRevisionNumber")]
    public void SetRevisionNumber()
    {
      using (var db = AcadDatabase.Active())
      {
        db.SummaryInfo.RevisionNumber = "42";
      }

      using (var db = AcadDatabase.Active())
      {
        MsTestAssert.AreEqual("42", db.SummaryInfo.RevisionNumber);
      }
    }

    [AcadTest("SetSubject")]
    public void SetSubject()
    {
      using (var db = AcadDatabase.Active())
      {
        db.SummaryInfo.Subject = "Subject";
      }

      using (var db = AcadDatabase.Active())
      {
        MsTestAssert.AreEqual("Subject", db.SummaryInfo.Subject);
      }
    }

    [AcadTest("SetTitle")]
    public void SetTitle()
    {
      using (var db = AcadDatabase.Active())
      {
        db.SummaryInfo.Title = "Drawing 23";
      }

      using (var db = AcadDatabase.Active())
      {
        MsTestAssert.AreEqual("Drawing 23", db.SummaryInfo.Title);
      }
    }
  }
}
