using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2Acad
{
  public class Block : EnumerableBase<Entity>
  {
    public Block(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID)
    {
    }

    protected override ObjectId GetObjectID(object iteratorItem)
    {
      return (ObjectId)iteratorItem;
    }

    public override int Count()
    {
      return Helpers.GetCount(transaction, ContainerID);
    }

    public ObjectId Add(Entity item)
    {
      return Add(item, false);
    }

    public ObjectId Add(Entity item, bool noDatabaseDefaults)
    {
      return Add(new[] { item }, noDatabaseDefaults).First();
    }

    public IEnumerable<ObjectId> Add(IEnumerable<Entity> items)
    {
      return Add(items, false);
    }

    public IEnumerable<ObjectId> Add(IEnumerable<Entity> items, bool noDatabaseDefaults)
    {
      var btr = (BlockTableRecord)transaction.GetObject(ContainerID, OpenMode.ForWrite);

      return items.Select(i =>
                         {
                           if (!noDatabaseDefaults)
                           {
                             i.SetDatabaseDefaults();
                           }

                           var id = btr.AppendEntity(i);
                          transaction.AddNewlyCreatedDBObject(i, true);
                           return id;
                         }).ToArray();
    }
  }
}
