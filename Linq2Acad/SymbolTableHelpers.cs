using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2Acad
{
  static class SymbolTableHelpers
  {
    public static bool IsValidName(string name, bool allowVerticalBar)
    {
      try
      {
        SymbolUtilityServices.ValidateSymbolName(name, allowVerticalBar);
        return true;
      }
      catch
      {
        return false;
      }
    }

    public static TRecord GetItem<TRecord, TTable>(this IEnumerable<TRecord> source, Func<TTable, ObjectId> getID) where TRecord : SymbolTableRecord
                                                                                                                   where TTable : SymbolTable
    {
      Helpers.CheckTransaction();

      if (source is IAcadEnumerableData)
      {
        var data = source as IAcadEnumerableData;
        var table = (TTable)L2ADatabase.Transaction.Value.GetObject(data.ContainerID, OpenMode.ForRead);
        return (TRecord)L2ADatabase.Transaction.Value.GetObject(getID(table), OpenMode.ForRead);
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
        var table = (TTable)L2ADatabase.Transaction.Value.GetObject(data.ContainerID, OpenMode.ForRead);
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
      return AddRange<TRecord, TTable>(source, new[] { item }).First();
    }

    public static IEnumerable<ObjectId> AddRange<TRecord, TTable>(this IEnumerable<TRecord> source, IEnumerable<TRecord> items) where TRecord : SymbolTableRecord
                                                                                                                                where TTable : SymbolTable
    {
      Helpers.CheckTransaction();

      if (source is IAcadEnumerableData)
      {
        var data = source as IAcadEnumerableData;
        var table = (TTable)L2ADatabase.Transaction.Value.GetObject(data.ContainerID, OpenMode.ForWrite);

        foreach (var item in items)
        {
          var id = table.Add(item);
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
