using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2Acad
{
  public static class LayerTableRecordExtensions
  {
    public static void Add(this LayerTableRecord layer, Entity entity)
    {
      Helpers.WriteWrap(entity, () => entity.LayerId = layer.ObjectId);
    }

    public static void AddRange(this LayerTableRecord layer, IEnumerable<Entity> entities)
    {
      foreach (var entity in entities)
      {
        Helpers.WriteWrap(entity, () => entity.LayerId = layer.ObjectId);
      }
    }
  }
}
