using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;
using System.Collections;

namespace Linq2Acad
{
  public static class DatabaseExtensions
  {
    public static T GetObject<T>(this Database db, ObjectId id) where T : DBObject
    {
      Helpers.CheckTransaction();
      return (T)L2ADatabase.Transaction.Value.GetObject(id, OpenMode.ForRead);
    }

    public static IEnumerable<T> GetObjects<T>(this Database db, IEnumerable<ObjectId> ids) where T : DBObject
    {
      Helpers.CheckTransaction();

      foreach (var id in ids)
      {
        yield return (T)L2ADatabase.Transaction.Value.GetObject(id, OpenMode.ForRead);
      }
    }
  }
}
