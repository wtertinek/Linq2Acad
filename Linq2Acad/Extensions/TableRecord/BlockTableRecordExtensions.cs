using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2Acad
{
  public static class BlockTableRecordExntensions
  {
    public static IEnumerable<Entity> Items(this BlockTableRecord source)
    {
      Helpers.CheckTransaction();
      return AcdbEnumerable<Entity>.Create(L2ADatabase.Transaction, source.Id);
    }

    public static IEnumerable<ObjectId> Cast(this BlockTableRecord source)
    {
      foreach (var id in source)
      {
        yield return id;
      }
    }

    public static IEnumerable<T> OfType<T>(this BlockTableRecord source) where T : Entity
    {
      Helpers.CheckTransaction();
      return AcdbEnumerable<T>.Create(L2ADatabase.Transaction, source.Id, true);
    }

    public static ObjectId Add<T>(this BlockTableRecord source, T item) where T : Entity
    {
      return Add<T>(source, item, false);
    }

    public static ObjectId Add<T>(this BlockTableRecord source, T item, bool noDatabaseDefaults) where T : Entity
    {
      Helpers.CheckTransaction();
      return Helpers.WriteCheck(source, () => AddItem(source, item, noDatabaseDefaults));
    }

    public static IEnumerable<ObjectId> AddRange<T>(this BlockTableRecord source, IEnumerable<T> items) where T : Entity
    {
      return AddRange<T>(source, items, false);
    }

    public static IEnumerable<ObjectId> AddRange<T>(this BlockTableRecord source, IEnumerable<T> items, bool noDatabaseDefaults) where T : Entity
    {
      Helpers.CheckTransaction();
      return Helpers.WriteCheck(source, () => items.Select(i => AddItem(source, i, noDatabaseDefaults))
                                                   .ToArray());
    }

    private static ObjectId AddItem(BlockTableRecord btr, Entity item, bool noDatabaseDefaults)
    {
      if (!noDatabaseDefaults)
      {
        item.SetDatabaseDefaults();
      }

      var id = btr.AppendEntity(item);
      L2ADatabase.Transaction.Value.AddNewlyCreatedDBObject(item, true);
      return id;
    }
  }
}
