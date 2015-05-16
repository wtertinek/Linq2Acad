using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2Acad
{
  public static class EntitiesExtensions
  {
    public static ObjectId Add(this IEnumerable<Entity> source, Entity item)
    {
      return Add(source, item, false);
    }

    public static ObjectId Add(this IEnumerable<Entity> source, Entity item, bool noDatabaseDefaults)
    {
      Helpers.CheckTransaction();
      return Add(source, new [] { item }, noDatabaseDefaults).First();
    }

    public static IEnumerable<ObjectId> Add(this IEnumerable<Entity> source, IEnumerable<Entity> items)
    {
      return Add(source, items, false);
    }

    public static IEnumerable<ObjectId> Add(this IEnumerable<Entity> source, IEnumerable<Entity> items, bool noDatabaseDefaults)
    {
      Helpers.CheckTransaction();

      if (source is IAcadEnumerableData)
      {
        var data = (IAcadEnumerableData)source;

        var btr = (BlockTableRecord)L2ADatabase.Transaction.Value.GetObject(data.ContainerID, OpenMode.ForWrite);

        foreach (var item in items)
        {
          if (!noDatabaseDefaults)
          {
            item.SetDatabaseDefaults();
          }

          var id = btr.AppendEntity(item);
          L2ADatabase.Transaction.Value.AddNewlyCreatedDBObject(item, true);
          yield return id;
        }
      }
      else
      {
        throw new InvalidOperationException();
      }
    }
  }
}
