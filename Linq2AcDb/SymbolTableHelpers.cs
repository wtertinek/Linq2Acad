using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2AcDb
{
  static class SymbolTableHelpers
  {
    public static TRecord GetItem<TRecord, TTable>(this IEnumerable<TRecord> source, Func<TTable, ObjectId> getItem) where TRecord : SymbolTableRecord
                                                                                                                     where TTable : SymbolTable
    {
      Helpers.CheckTransaction();

      if (source is IAcadEnumerableData)
      {
        var data = source as IAcadEnumerableData;
        var table = (TTable)ActiveDatabase.Transaction.Value.GetObject(data.ContainerID, OpenMode.ForRead);
        return (TRecord)ActiveDatabase.Transaction.Value.GetObject(getItem(table), OpenMode.ForRead);
      }
      else
      {
        throw new InvalidOperationException();
      }
    }

    public static bool Contains<TRecord, TTable>(this IEnumerable<TRecord> source, Func<TTable, bool> has) where TRecord : SymbolTableRecord
                                                                                                           where TTable : SymbolTable
    {
      Helpers.CheckTransaction();

      if (source is IAcadEnumerableData)
      {
        var data = source as IAcadEnumerableData;
        var table = (TTable)ActiveDatabase.Transaction.Value.GetObject(data.ContainerID, OpenMode.ForWrite);
        return has(table);
      }
      else
      {
        throw new InvalidOperationException();
      }
    }

    public static ObjectId Add<TRecord, TTable>(this IEnumerable<TRecord> source, TRecord item) where TRecord : SymbolTableRecord
                                                                                                where TTable : SymbolTable
    {
      return Add<TRecord, TTable>(source, new[] { item }).First();
    }

    public static IEnumerable<ObjectId> Add<TRecord, TTable>(this IEnumerable<TRecord> source, IEnumerable<TRecord> items) where TRecord : SymbolTableRecord
                                                                                                                           where TTable : SymbolTable
    {
      Helpers.CheckTransaction();

      if (source is IAcadEnumerableData)
      {
        var data = source as IAcadEnumerableData;
        var table = (TTable)ActiveDatabase.Transaction.Value.GetObject(data.ContainerID, OpenMode.ForWrite);

        foreach (var item in items)
        {
          var id = table.Add(item);
          ActiveDatabase.Transaction.Value.AddNewlyCreatedDBObject(item, true);
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
