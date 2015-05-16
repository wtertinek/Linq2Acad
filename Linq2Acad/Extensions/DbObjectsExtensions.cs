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
    public static int Count<T>(this IEnumerable<T> source) where T : DBObject
    {
      if (source is IAcadEnumerable)
      {
        return ((IAcadEnumerable)source).Count;
      }
      else
      {
        return Enumerable.Count(source);
      }
    }

    public static void ForEach<T>(this IEnumerable<T> items, Action<T> action) where T : DBObject
    {
      Helpers.CheckTransaction();

      foreach (var item in items)
      {
        Helpers.WriteCheck(item, () => action(item));
        action(item);
      }
    }

    public static IEnumerable<T> OfType<T>(this IEnumerable<DBObject> source) where T : DBObject
    {
      Helpers.CheckTransaction();

      if (source is IAcadEnumerable)
      {
        var acadEnumerable = (IAcadEnumerable)source;
        return new TableEnumerable<T>(L2ADatabase.Transaction, acadEnumerable.ContainerID, true);
      }
      else
      {
        return Filter<T>(source);
      }
    }

    private static IEnumerable<T> Filter<T>(IEnumerable<DBObject> source) where T : DBObject
    {
      foreach (var item in source)
      {
        if (item is T)
        {
          yield return (T)item;
        }
      }
    }

    public static IEnumerable<T> UpgradeOpen<T>(this IEnumerable<T> source) where T : DBObject
    {
      Helpers.CheckTransaction();

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
      Helpers.CheckTransaction();

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
