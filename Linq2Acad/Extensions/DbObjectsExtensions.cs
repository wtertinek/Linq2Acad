using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2Acad
{
  /// <summary>
  /// Extension methods for IEnumerable&lt;DBObject&gt;.
  /// </summary>
  public static class DbObjectsExtensions
  {
    /// <summary>
    /// Performs the specified action on each elemenet of the System.Collections.Generic.IEnumerable&lt;DBObject&gt;.
    /// </summary>
    /// <typeparam name="T">The type of elements in this System.Collections.Generic.IEnumerable&lt;DBObject&gt;.</typeparam>
    /// <param name="items">The System.Collections.Generic.IEnumerable&lt;DBObject&gt; instance.</param>
    /// <param name="action"></param>
    public static void ForEach<T>(this IEnumerable<T> items, Action<T> action) where T : DBObject
    {
      foreach (var item in items)
      {
        Helpers.WriteWrap(item, () => action(item));
      }
    }

    /// <summary>
    /// Upgrades the objects to open OpenMode.ForWrite
    /// </summary>
    /// <typeparam name="T">The type of elements in this System.Collections.Generic.IEnumerable&lt;DBObject&gt;.</typeparam>
    /// <param name="source">The System.Collections.Generic.IEnumerable&lt;DBObject&gt; instance.</param>
    /// <returns></returns>
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

    /// <summary>
    /// Downgrades the objects from being open OpenMode.ForWrite, to being OpenMode.ForRead.
    /// </summary>
    /// <typeparam name="T">The type of elements in this System.Collections.Generic.IEnumerable&lt;DBObject&gt;.</typeparam>
    /// <param name="source">The System.Collections.Generic.IEnumerable&lt;DBObject&gt; instance.</param>
    /// <returns></returns>
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
