using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;

namespace Linq2Acad
{
  /// <summary>
  /// Extension methods for XRefs.
  /// </summary>
  public static class XRefsExtensions
  {
    /// <summary>
    /// Binds XRefs.
    /// </summary>
    /// <param name="xrefs">The XRefs to bind.</param>
    public static void Bind(this IEnumerable<XRef> xrefs)
    {
      Require.ParameterNotNull(xrefs, nameof(xrefs));

      xrefs.Bind(false);
    }

    /// <summary>
    /// Binds XRefs.
    /// </summary>
    /// <param name="xrefs">The XRefs to bind.</param>
    /// <param name="insertSymbolNamesWithoutPrefixes">If set to true, the SymbolTableRecord names will be changed from the XRef naming convention to normal insert block names.</param>
    public static void Bind(this IEnumerable<XRef> xrefs, bool insertSymbolNamesWithoutPrefixes)
    {
      Require.ParameterNotNull(xrefs, nameof(xrefs));

      var ids = xrefs.Select(xr => xr.Block.ObjectId)
                     .ToArray();

      if (ids.Any())
      {
        using (var idCollection = new ObjectIdCollection(ids))
        {
          ids[0].Database.BindXrefs(idCollection, !insertSymbolNamesWithoutPrefixes);
        }
      }
    }

    /// <summary>
    /// Detaches XRefs.
    /// </summary>
    /// <param name="xrefs">The XRefs to detach.</param>
    public static void Detach(this IEnumerable<XRef> xrefs)
    {
      Require.ParameterNotNull(xrefs, nameof(xrefs));

      foreach (var xref in xrefs)
      {
        xref.Database.DetachXref(xref.Block.ObjectId);
      }
    }

    /// <summary>
    /// Reloads XRefs.
    /// </summary>
    /// <param name="xrefs">The XRefs to reload.</param>
    public static void Reload(this IEnumerable<XRef> xrefs)
    {
      Require.ParameterNotNull(xrefs, nameof(xrefs));

      var ids = xrefs.Select(xr => xr.Block.ObjectId)
                     .ToArray();

      if (ids.Any())
      {
        using (var idCollection = new ObjectIdCollection(ids))
        {
          ids[0].Database.ReloadXrefs(idCollection);
        }
      }
    }

    /// <summary>
    /// Unloads XRefs.
    /// </summary>
    /// <param name="xrefs">The XRefs to unload.</param>
    public static void Unload(this IEnumerable<XRef> xrefs)
    {
      Require.ParameterNotNull(xrefs, nameof(xrefs));

      var ids = xrefs.Select(xr => xr.Block.ObjectId)
                     .ToArray();

      if (ids.Any())
      {
        using (var idCollection = new ObjectIdCollection(ids))
        {
          ids[0].Database.UnloadXrefs(idCollection);
        }
      }
    }
  }
}
