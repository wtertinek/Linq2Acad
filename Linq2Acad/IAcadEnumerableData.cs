using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2Acad
{
  interface IAcadEnumerableData
  {
    Lazy<Transaction> Transaction { get; }

    IEnumerable<ObjectId> IDs { get; }

    ObjectId ContainerID { get; }

    int Count { get; }
  }
}
