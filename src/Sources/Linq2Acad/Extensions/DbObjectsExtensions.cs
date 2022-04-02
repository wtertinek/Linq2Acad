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
    /// Performs the specified action on each element of the System.Collections.Generic.IEnumerable&lt;DBObject&gt;.
    /// </summary>
    /// <typeparam name="T">The type of elements in this System.Collections.Generic.IEnumerable&lt;DBObject&gt;.</typeparam>
    /// <param name="elements">The System.Collections.Generic.IEnumerable&lt;DBObject&gt; instance.</param>
    /// <param name="action">The action to execute.</param>
    public static void ForEach<T>(this IEnumerable<T> elements, Action<T> action) where T : DBObject
    {
      Require.ParameterNotNull(elements, nameof(elements));
      Require.ParameterNotNull(action, nameof(action));

      foreach (var element in elements)
      {
        action(element);
      }
    }

    /// <summary>
    /// Upgrades the objects to open OpenMode.ForWrite
    /// </summary>
    /// <typeparam name="T">The type of elements in this System.Collections.Generic.IEnumerable&lt;DBObject&gt;.</typeparam>
    /// <param name="source">The System.Collections.Generic.IEnumerable&lt;DBObject&gt; instance.</param>
    /// <returns>The given elements in OpenMode.ForWrite.</returns>
    public static IEnumerable<T> UpgradeOpen<T>(this IEnumerable<T> source) where T : DBObject
    {
      Require.ParameterNotNull(source, nameof(source));

      foreach (var element in source)
      {
        if (!element.IsWriteEnabled)
        {
          element.UpgradeOpen();
        }

        yield return element;
      }
    }

    /// <summary>
    /// Downgrades the objects from being open OpenMode.ForWrite, to being OpenMode.ForRead.
    /// </summary>
    /// <typeparam name="T">The type of elements in this System.Collections.Generic.IEnumerable&lt;DBObject&gt;.</typeparam>
    /// <param name="source">The System.Collections.Generic.IEnumerable&lt;DBObject&gt; instance.</param>
    /// <returns>The given elements in OpenMode.ForWrite.</returns>
    public static IEnumerable<T> DowngradeOpen<T>(this IEnumerable<T> source) where T : DBObject
    {
      Require.ParameterNotNull(source, nameof(source));

      foreach (var element in source)
      {
        if (!element.IsReadEnabled)
        {
          element.DowngradeOpen();
        }

        yield return element;
      }
    }
  }
}
