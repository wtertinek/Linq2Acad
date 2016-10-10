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
    public AcadTestAttribute()
      : this(null, 0)
    {
    }

    public AcadTestAttribute(string dwgFilePath)
      : this(dwgFilePath, 0)
    {
    }

    public AcadTestAttribute(int invocationDelayInSeconds)
      : this(null, invocationDelayInSeconds)
    {
    }

    public AcadTestAttribute(string dwgFilePath, int invocationDelayInSeconds)
    {
      DwgFilePath = dwgFilePath;
      InvocationDelay = invocationDelayInSeconds;
    }

    internal string DwgFilePath { get; private set; }

    internal int InvocationDelay { get; private set; }
  }
}
