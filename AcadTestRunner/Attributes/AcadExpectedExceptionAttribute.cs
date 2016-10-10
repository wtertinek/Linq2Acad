using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcadTestRunner
{
  [AttributeUsage(AttributeTargets.Method)]
  public class AcadExpectedExceptionAttribute : Attribute
  {
    public AcadExpectedExceptionAttribute(Type expectedException)
    {
      if (expectedException == null) throw new ArgumentNullException();
      if (expectedException.BaseType.Name != "Exception" &&
          expectedException.Name != "Exception") throw new ArgumentException();

      ExpectedException = expectedException;
    }

    internal Type ExpectedException { get; private set; }
  }
}
