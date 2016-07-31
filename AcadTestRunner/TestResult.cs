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
      FullOutput = "";
    }

    private TestResult(string message, string fullOutput)
    {
      Passed = false;
      Message = message;
      FullOutput = fullOutput;
    }
    public bool Passed { get; private set; }

    public string Message { get; private set; }

    public string FullOutput { get; private set; }

    internal static TestResult TestPassed()
    {
      return new TestResult();
    }

    internal static TestResult TestFailed(string message, string fullOutput)
    {
      return new TestResult(message, fullOutput);
    }
  }
}
