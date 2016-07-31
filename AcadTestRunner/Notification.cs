using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcadTestRunner
{
  public class Notification
  {
    private string prefix;

    public Notification(string testName)
    {
      prefix = "AcadTestRunner - Test " + testName + ": ";
    }

    public void TestPassed()
    {
      Console.WriteLine(prefix + "PASSED");
    }

    public void TestFailed(string message)
    {
      Console.WriteLine(prefix + "FAILED -> " + message);
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

      Console.WriteLine(prefix + "FAILED" + message.ToString());
    }
  }
}
