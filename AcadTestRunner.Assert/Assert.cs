using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;

namespace AcadTestRunner
{
  public static class Assert
  {
    public static bool TableIDs(Database database, ObjectId tableId, Func<IEnumerable<ObjectId>, bool> assert)
    {
      return TableIDs(database, tableId, assert, null);
    }

    public static bool TableIDs(Database database, ObjectId tableId, Func<IEnumerable<ObjectId>, bool> assert, string message)
    {
      using (var tr = database.TransactionManager.StartTransaction())
      {
        var table = (SymbolTable)tr.GetObject(tableId, OpenMode.ForRead);
        var ok = assert(table.Cast<ObjectId>());

        if (ok)
        {
          return true;
        }
        else
        {
          throw new AssertFailedException(message);
        }
      }
    }

    public static bool Table(Database database, ObjectId tableId, Func<SymbolTable, bool> assert)
    {
      return Table(database, tableId, assert, null);
    }

    public static bool Table(Database database, ObjectId tableId, Func<SymbolTable, bool> assert, string message)
    {
      return DbObject<SymbolTable>(database, tableId, assert, message);
    }

    public static bool DictionaryIDs(Database database, ObjectId dictionaryId, Func<IEnumerable<ObjectId>, bool> assert)
    {
      return DictionaryIDs(database, dictionaryId, assert, null);
    }

    public static bool DictionaryIDs(Database database, ObjectId dictionaryId, Func<IEnumerable<ObjectId>, bool> assert, string message)
    {
      using (var tr = database.TransactionManager.StartTransaction())
      {
        var dict = (DBDictionary)tr.GetObject(dictionaryId, OpenMode.ForRead);
        var ok = assert(dict.Cast<DBDictionaryEntry>()
                            .Select(e => e.m_value));

        if (ok)
        {
          return true;
        }
        else
        {
          throw new AssertFailedException(message);
        }
      }
    }

    public static bool Dictionary(Database database, ObjectId dictionaryId, Func<DBDictionary, bool> assert)
    {
      return Dictionary(database, dictionaryId, assert, null);
    }

    public static bool Dictionary(Database database, ObjectId dictionaryId, Func<DBDictionary, bool> assert, string message)
    {
      return DbObject<DBDictionary>(database, dictionaryId, assert, message);
    }

    private static bool DbObject<T>(Database database, ObjectId containerId, Func<T, bool> assert, string message) where T : DBObject
    {
      using (var tr = database.TransactionManager.StartTransaction())
      {
        var ok = assert((T)tr.GetObject(containerId, OpenMode.ForRead));

        if (ok)
        {
          return true;
        }
        else
        {
          throw new AssertFailedException(message);
        }
      }
    }
  }
}
