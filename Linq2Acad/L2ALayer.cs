using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2Acad
{
  public class L2ALayer : LayerTableRecord
  {
    private LayerTableRecord item;
    private Transaction transaction;

    public L2ALayer(LayerTableRecord item, Transaction transaction)
    {
      this.item = item;
      this.transaction = transaction;
    }

    public void Add(IEnumerable<Entity> entities)
    {
      foreach (var entitiy in entities)
      {
        entitiy.LayerId = ObjectId;
      }
    }
  }
}
