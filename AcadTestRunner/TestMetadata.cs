using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AcadTestRunner
{
  public class TestMetadata
  {
    public TestMetadata(string assemblyPath, string className, string methodName)
    {
      Type = Assembly.LoadFrom(assemblyPath)
                     .GetTypes()
                     .FirstOrDefault(t => t.Name == className);

      if (Type != null)
      {
        var info = Type.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                        .Select(m => new { Method = m, AcadTestAttribute = m.GetCustomAttributes(typeof(AcadTestAttribute), false).FirstOrDefault() })
                        .FirstOrDefault(m => m.Method.Name == methodName &&
                                             m.AcadTestAttribute != null);

        if (info != null)
        {
          Method = info.Method;
          AcadTestAttribute = info.AcadTestAttribute as AcadTestAttribute;
          var expectedExceptionAttribute = Method.GetCustomAttribute(typeof(AcadExpectedExceptionAttribute), false);

          if (expectedExceptionAttribute != null)
          {
            ExpectedException = (expectedExceptionAttribute as AcadExpectedExceptionAttribute).ExpectedException;
          }
        }
      }
    }

    public Type Type { get; private set; }

    public bool HasPublicConstructor
    {
      get
      {
        return Type.GetConstructors()
                   .Any(c => c.IsPublic &&
                             c.GetParameters().Count() == 0);
      }
    }

    public MethodInfo Method { get; private set; }

    public AcadTestAttribute AcadTestAttribute { get; private set; }

    public Type ExpectedException { get; private set; }
  }
}
