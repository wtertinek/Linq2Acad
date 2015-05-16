using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2Acad
{
  interface IAcadEnumerable
  {
    ObjectId ContainerID { get; }

    int Count { get; }
  }
}
