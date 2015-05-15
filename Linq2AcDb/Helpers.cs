using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2AcDb
{
  class Helpers
  {
    public static void CheckTransaction()
    {
      if (ActiveDatabase.Transaction == null)
      {
        throw new InvalidOperationException("No ActiveDatabase context available");
      }
    }
  }
}
