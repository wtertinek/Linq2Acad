using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;

namespace Linq2Acad.Tests
{
  public static class Assert
  {
    public static bool Table<T>(Database database, Func<T, bool> assert) where T : SymbolTable
    {
      using (var tr = database.TransactionManager.StartTransaction())
      {
        return assert((T)tr.GetObject(database.BlockTableId, OpenMode.ForRead));
      }
    }

    public static bool Dictionary(Database database, Func<DBDictionary, bool> assert)
    {
      using (var tr = database.TransactionManager.StartTransaction())
      {
        return assert((DBDictionary)tr.GetObject(database.BlockTableId, OpenMode.ForRead));
      }
    }
  }
}
