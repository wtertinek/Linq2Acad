using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2Acad
{
  static class Helpers
  {
    public static void WriteCheck<T>(T item, Action action, bool keepUpgraded = false) where T : DBObject
    {
      WriteCheck<T, object>(item, () => { action(); return null; }, keepUpgraded);
    }

    public static TResult WriteCheck<T, TResult>(T item, Func<TResult> function, bool keepUpgraded = false) where T : DBObject
    {
      bool changed = false;

      if (!item.IsWriteEnabled)
      {
        changed = true;
        item.UpgradeOpen();
      }

      TResult result = function();

      if (!keepUpgraded && changed)
      {
        item.DowngradeOpen();
      }

      return result;
    }

    public static void CheckTransaction()
    {
      if (L2ADatabase.Transaction == null)
      {
        throw new InvalidOperationException("No ActiveDatabase context available");
      }
    }
  }
}
