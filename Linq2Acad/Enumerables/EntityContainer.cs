using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;

namespace Linq2Acad
{
  public class EntityContainer : ContainerEnumerableBase<Entity>
  {
    public EntityContainer(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID, i => (ObjectId)i)
    {
    }

    public ObjectId ObjectId
    {
      get { return ID; }
    }

    public ObjectId Add(Entity item)
    {
      if (item == null) throw Error.ArgumentNull("item");
      
      try
      {
        return AddInternal(new[] { item }, false).First();
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e);
      }
    }

    public ObjectId Add(Entity item, bool setDatabaseDefaults)
    {
      if (item == null) throw Error.ArgumentNull("item");

      try
      {
        return AddInternal(new[] { item }, setDatabaseDefaults).First();
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e);
      }
    }

    public IEnumerable<ObjectId> Add(IEnumerable<Entity> items)
    {
      if (items == null) throw Error.ArgumentNull("items");
      
      try
      {
        return AddInternal(items, false);
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e);
      }
    }

    public IEnumerable<ObjectId> Add(IEnumerable<Entity> items, bool setDatabaseDefaults)
    {
      if (items == null) throw Error.ArgumentNull("items");
      
      try
      {
        return AddInternal(items, false);
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e);
      }
    }

    private IEnumerable<ObjectId> AddInternal(IEnumerable<Entity> items, bool setDatabaseDefaults)
    {
      var btr = (BlockTableRecord)transaction.GetObject(ID, OpenMode.ForWrite);
      return items.Select(i =>
                          {
                            if (setDatabaseDefaults)
                            {
                              i.SetDatabaseDefaults();
                            }

                            var id = btr.AppendEntity(i);
                            transaction.AddNewlyCreatedDBObject(i, true);
                            return id;
                          });
    }

    public void Clear()
    {
      try
      {
        this.ForEach(e => e.Erase());
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e);
      }
    }
  }
}
