using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcadTestRunner
{
  [AttributeUsage(AttributeTargets.Class)]
  public class AcadTestClassAttribute : Attribute
  {
    public AcadTestClassAttribute(string testClassName)
    {
      TestClassName = testClassName;
    }

    public string TestClassName { get; private set; }
  }
}
