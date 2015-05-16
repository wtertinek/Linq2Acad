using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2AcDb
{
  static class Helpers
  {
    public static void WriteCheck<T>(T item, Action action) where T : DBObject
    {
      WriteCheck<T, object>(item, () => { action(); return null; });
    }

    public static TResult WriteCheck<T, TResult>(T item, Func<TResult> function) where T : DBObject
    {
      bool changed = false;

      if (!item.IsWriteEnabled)
      {
        changed = true;
        item.UpgradeOpen();
      }

      TResult result = function();

      if (changed)
      {
        item.DowngradeOpen();
      }

      return result;
    }

    public static void CheckTransaction()
    {
      if (ActiveDatabase.Transaction == null)
      {
        throw new InvalidOperationException("No ActiveDatabase context available");
      }
    }
  }
}
