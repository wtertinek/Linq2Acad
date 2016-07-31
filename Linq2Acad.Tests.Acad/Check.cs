using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;

namespace Linq2Acad.Tests
{
  public static class Check
  {
    public static bool TableIDs(Database database, Func<IEnumerable<ObjectId>, bool> assert)
    {
      using (var tr = database.TransactionManager.StartTransaction())
      {
        var table = (SymbolTable)tr.GetObject(database.BlockTableId, OpenMode.ForRead);
        return assert(table.Cast<ObjectId>());
      }
    }

    public static bool Table(Database database, Func<SymbolTable, bool> assert)
    {
      return DbObject<SymbolTable>(database, assert);
    }

    public static bool DictionaryIDs(Database database, Func<IEnumerable<ObjectId>, bool> assert)
    {
      using (var tr = database.TransactionManager.StartTransaction())
      {
        var dict = (DBDictionary)tr.GetObject(database.BlockTableId, OpenMode.ForRead);
        return assert(dict.Cast<DBDictionaryEntry>()
                          .Select(e => e.m_value));
      }
    }

    public static bool Dictionary(Database database, Func<DBDictionary, bool> assert)
    {
      return DbObject<DBDictionary>(database, assert);
    }

    private static bool DbObject<T>(Database database, Func<T, bool> assert) where T : DBObject
    {
      using (var tr = database.TransactionManager.StartTransaction())
      {
        return assert((T)tr.GetObject(database.BlockTableId, OpenMode.ForRead));
      }
    }
  }
}
