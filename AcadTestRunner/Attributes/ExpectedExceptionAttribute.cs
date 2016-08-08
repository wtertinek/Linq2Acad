using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcadTestRunner
{
  [AttributeUsage(AttributeTargets.Method)]
  public class ExpectedExceptionAttribute : Attribute
  {
    public ExpectedExceptionAttribute(Type expectedException)
    {
      if (expectedException == null) throw new ArgumentNullException();
      if (expectedException.BaseType.Name != "Exception" &&
          expectedException.Name != "Exception") throw new ArgumentException();

      ExpectedException = expectedException;
    }

    internal Type ExpectedException { get; private set; }
  }
}
