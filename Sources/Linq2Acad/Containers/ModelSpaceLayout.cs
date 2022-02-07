using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;

namespace Linq2Acad
{
  public class ModelSpaceLayout : EntityContainer
  {
    internal ModelSpaceLayout(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID)
    {
    }
  }
}
