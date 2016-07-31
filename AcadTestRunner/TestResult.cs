using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcadTestRunner
{
  public class TestResult
  {
    private TestResult()
    {
      Passed = true;
      Message = "";
    }

    private TestResult(string message)
    {
      Passed = false;
      Message = message;
    }
    public bool Passed { get; private set; }

    public string Message { get; private set; }

    internal static TestResult TestPassed()
    {
      return new TestResult();
    }

    internal static TestResult TestFailed(string message)
    {
      return new TestResult(message);
    }
  }
}
