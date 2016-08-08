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
    {
      TestMethodName = testMethodName;
    }

    public string TestMethodName { get; private set; }
  }
}
