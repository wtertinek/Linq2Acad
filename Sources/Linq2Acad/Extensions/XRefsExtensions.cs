using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;

namespace Linq2Acad
{
  public static class XRefsExtensions
  {
    public static void Bind(this IEnumerable<XRef> xrefs)
    {
      Require.ParameterNotNull(xrefs, nameof(xrefs));

      xrefs.Bind(false);
    }

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

    public static void Detach(this IEnumerable<XRef> xrefs)
    {
      Require.ParameterNotNull(xrefs, nameof(xrefs));

      foreach (var xref in xrefs)
      {
        xref.Database.DetachXref(xref.Block.ObjectId);
      }
    }

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
