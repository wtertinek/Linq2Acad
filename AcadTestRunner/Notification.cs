using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcadTestRunner
{
  public class Notification
  {
    private string testCase;

    public Notification(string testCase)
    {
      this.testCase = testCase;
    }

    public void TestPassed()
    {
      Console.WriteLine("Test result " + testCase + ": PASSED");
    }

    public void TestFailed(string message)
    {
      Console.WriteLine("Test result " + testCase + ": FAILED -> " + message);
    }

    public void TestFailed(Exception e)
    {
      var message = new StringBuilder();
      message.Append(e.Message);

      do
      {
        message.Append(" -> ");
        message.Append(e.Message);
        e = e.InnerException;
      }
      while (e != null);

      Console.WriteLine("Test result " + testCase + ": FAILED" + message.ToString());
    }
  }
}
