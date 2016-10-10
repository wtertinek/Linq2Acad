using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcadTestRunner
{
  [AttributeUsage(AttributeTargets.Method)]
  public class AcadTestAttribute : Attribute
  {
    public AcadTestAttribute(string testMethodName)
      : this(testMethodName, 0)
    {
    }

    public AcadTestAttribute(string testMethodName, int invocationDelay)
    {
      TestMethodName = testMethodName;
      InvocationDelay = invocationDelay;
    }

    public string TestMethodName { get; private set; }

    public int InvocationDelay { get; private set; }
  }
}
