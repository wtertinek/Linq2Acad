using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2AcDb
{
  interface IAcadEnumerableData
  {
    bool IsEnumerating { get; }

    IEnumerable<ObjectId> IDs { get; }

    Lazy<Transaction> Transaction { get; }
  }
}
