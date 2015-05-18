using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2Acad
{
  public class EntityEnumerable : EnumerableBase<Entity>
  {
    public EntityEnumerable(Lazy<Transaction> transaction, ObjectId containerID)
      : base(transaction, containerID)
    {
    }

    protected override ObjectId GetObjectID(object iteratorItem)
    {
      return (ObjectId)iteratorItem;
    }

    public override int Count()
    {
      var enumerator = ((IEnumerable)transaction.Value.GetObject(ContainerID, OpenMode.ForRead)).GetEnumerator();

      var count = 0;

      while (enumerator.MoveNext())
      {
        count++;
      }

      return count;
    }

    public ObjectId Add(Entity item)
    {
      return Add(item, false);
    }

    public ObjectId Add(Entity item, bool noDatabaseDefaults)
    {
      Helpers.CheckTransaction();
      return Add(new[] { item }, noDatabaseDefaults).First();
    }

    public IEnumerable<ObjectId> Add(IEnumerable<Entity> items)
    {
      return Add(items, false);
    }

    public IEnumerable<ObjectId> Add(IEnumerable<Entity> items, bool noDatabaseDefaults)
    {
      Helpers.CheckTransaction();

      var btr = (BlockTableRecord)L2ADatabase.Transaction.Value.GetObject(ContainerID, OpenMode.ForWrite);

      return items.Select(i =>
                         {
                           if (!noDatabaseDefaults)
                           {
                             i.SetDatabaseDefaults();
                           }

                           var id = btr.AppendEntity(i);
                           L2ADatabase.Transaction.Value.AddNewlyCreatedDBObject(i, true);
                           return id;
                         }).ToArray();
    }
  }
}
