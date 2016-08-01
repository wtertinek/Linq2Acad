using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcadTestRunner
{
  public class AssertFailedException : Exception
  {
    public AssertFailedException(string message)
      : base(message)
    {
    }
  }
}
