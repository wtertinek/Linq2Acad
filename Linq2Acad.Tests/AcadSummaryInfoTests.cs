using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AcadTestRunner;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linq2Acad.Tests
{
  public partial class AcadSummaryInfoTests
  {
    [AcadTest]
    public void SetCustomProperties()
    {
      using (var db = AcadDatabase.Active())
      {
        db.SummaryInfo.CustomProperties["CustomData1"] = "42";
      }

      using (var db = AcadDatabase.Active())
      {
        Assert.IsTrue(db.SummaryInfo.CustomProperties.ContainsKey("CustomData1"));
        Assert.AreEqual("42", db.SummaryInfo.CustomProperties["CustomData1"]);
      }
    }

    [AcadTest]
    public void SetAuthor()
    {
      using (var db = AcadDatabase.Active())
      {
        db.SummaryInfo.Author = "jdoe";
      }

      using (var db = AcadDatabase.Active())
      {
        Assert.AreEqual("jdoe", db.SummaryInfo.Author);
      }
    }

    [AcadTest]
    public void SetComments()
    {
      using (var db = AcadDatabase.Active())
      {
        db.SummaryInfo.Comments = "This is a comment";
      }

      using (var db = AcadDatabase.Active())
      {
        Assert.AreEqual("This is a comment", db.SummaryInfo.Comments);
      }
    }

    [AcadTest]
    public void SetHyperlinkBase()
    {
      using (var db = AcadDatabase.Active())
      {
        db.SummaryInfo.HyperlinkBase = "https://www.github.com";
      }

      using (var db = AcadDatabase.Active())
      {
        Assert.AreEqual("https://www.github.com", db.SummaryInfo.HyperlinkBase);
      }
    }

    [AcadTest]
    public void SetKeywords()
    {
      using (var db = AcadDatabase.Active())
      {
        db.SummaryInfo.Keywords = "ACAD";
      }

      using (var db = AcadDatabase.Active())
      {
        Assert.AreEqual("ACAD", db.SummaryInfo.Keywords);
      }
    }

    [AcadTest]
    public void SetLastSavedBy()
    {
      using (var db = AcadDatabase.Active())
      {
        db.SummaryInfo.LastSavedBy = "jdoe";
      }

      using (var db = AcadDatabase.Active())
      {
        Assert.AreEqual("jdoe", db.SummaryInfo.LastSavedBy);
      }
    }

    [AcadTest]
    public void SetRevisionNumber()
    {
      using (var db = AcadDatabase.Active())
      {
        db.SummaryInfo.RevisionNumber = "42";
      }

      using (var db = AcadDatabase.Active())
      {
        Assert.AreEqual("42", db.SummaryInfo.RevisionNumber);
      }
    }

    [AcadTest]
    public void SetSubject()
    {
      using (var db = AcadDatabase.Active())
      {
        db.SummaryInfo.Subject = "Subject";
      }

      using (var db = AcadDatabase.Active())
      {
        Assert.AreEqual("Subject", db.SummaryInfo.Subject);
      }
    }

    [AcadTest]
    public void SetTitle()
    {
      using (var db = AcadDatabase.Active())
      {
        db.SummaryInfo.Title = "Drawing 23";
      }

      using (var db = AcadDatabase.Active())
      {
        Assert.AreEqual("Drawing 23", db.SummaryInfo.Title);
      }
    }
  }
}
