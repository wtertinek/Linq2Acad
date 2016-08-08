using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcadTestRunner
{
  public class Notification
  {
    private const string Passed = "PASSED";
    private const string Failed = "FAILED";
    private string prefix;

    public Notification(string testName)
    {
      prefix = "AcadTestRunner - " + testName + " - ";
    }

    internal void WriteMessage(string message)
    {
      Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage(Environment.NewLine + prefix + message);
    }

    internal void TestPassed()
    {
      WriteMessage(Passed);
    }

    public void TestFailed(string message)
    {
      TestFailed(message, null);
    }

    public void TestFailed(string message, string stackTrace)
    {
      WriteMessage(Failed + " - " + message);
    }

    public void TestFailed(Exception e)
    {
      var message = new StringBuilder();
      message.Append(e.Message);

      while (e.InnerException != null)
      {
        e = e.InnerException;
        message.Append(" -> ");
        message.Append(e.Message);
      }

      WriteMessage(Failed + " - " + message.ToString());
    }

    public static string GetPassedMessage(string testName)
    {
      return GetMessage(testName, Passed);
    }

    public static string GetFailedMessage(string testName)
    {
      return GetMessage(testName, Failed);
    }

    public static string GetMessage(string testName, string message)
    {
      var notification = new Notification(testName);
      return notification.prefix + message;
    }
  }
}
