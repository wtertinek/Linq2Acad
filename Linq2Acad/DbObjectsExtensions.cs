using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2Acad
{
  public static class DbObjectsExtensions
  {
    public static void ForEach<T>(this IEnumerable<T> items, Action<T> action) where T : DBObject
    {
      foreach (var item in items)
      {
        Helpers.WriteCheck(item, () => action(item));
      }
    }

    public static IEnumerable<T> UpgradeOpen<T>(this IEnumerable<T> source) where T : DBObject
    {
      foreach (var item in source)
      {
        if (!item.IsWriteEnabled)
        {
          item.UpgradeOpen();
        }

        yield return item;
      }
    }

    public static IEnumerable<T> DowngradeOpen<T>(this IEnumerable<T> source) where T : DBObject
    {
      foreach (var item in source)
      {
        if (!item.IsReadEnabled)
        {
          item.DowngradeOpen();
        }

        yield return item;
      }
    }
  }
}
