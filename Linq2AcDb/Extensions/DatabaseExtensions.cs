using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;
using System.Collections;

namespace Linq2AcDb
{
  public static class DatabaseExtensions
  {
    public static T GetObject<T>(this Database db, ObjectId id) where T : DBObject
    {
      Helpers.CheckTransaction();
      return (T)ActiveDatabase.Transaction.Value.GetObject(id, OpenMode.ForRead);
    }

    public static IEnumerable<T> GetObjects<T>(this Database db, IEnumerable<ObjectId> ids) where T : DBObject
    {
      Helpers.CheckTransaction();
      return AcdbEnumerable<T>.Create(ActiveDatabase.Transaction, ids);
    }
  }
}
