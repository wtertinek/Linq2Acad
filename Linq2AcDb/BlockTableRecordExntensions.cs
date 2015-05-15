using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2AcDb
{
  public static class BlockTableRecordExntensions
  {
    public static IEnumerable<Entity> Items(this BlockTableRecord source)
    {
      Helpers.CheckTransaction();
      return AcdbEnumerable<Entity>.Create(ActiveDatabase.Transaction, source.Id);
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
      return AcdbEnumerable<T>.Create(ActiveDatabase.Transaction, source.Id, true);
    }

    public static void Add<T>(this BlockTableRecord source, T item, bool setDatabaseDefaults = false) where T : Entity
    {
      Helpers.CheckTransaction();
      Add(source, item, setDatabaseDefaults);
    }

    public static void Add<T>(this BlockTableRecord source, IEnumerable<T> items, bool setDatabaseDefaults = false) where T : Entity
    {
      Helpers.CheckTransaction();
      items.ForEach(i => AddItem(source, i, setDatabaseDefaults));
    }

    private static void AddItem(BlockTableRecord btr, Entity item, bool setDatabaseDefaults)
    {
      if (setDatabaseDefaults)
      {
        item.SetDatabaseDefaults();
      }

      ActiveDatabase.Transaction.Value.AddNewlyCreatedDBObject(item, true);
      btr.AppendEntity(item);
    }
  }
}
