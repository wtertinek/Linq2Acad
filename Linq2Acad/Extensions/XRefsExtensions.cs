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
      xrefs.Bind(false);
    }

    public static void Bind(this IEnumerable<XRef> xrefs, bool insertSymbolNamesWithoutPrefixes)
    {
      try
      {
        var ids = xrefs.Select(xr => xr.Block.ObjectId)
                       .ToArray();

        if (ids.Any())
        {
          xrefs.First().Database.BindXrefs(new ObjectIdCollection(ids), !insertSymbolNamesWithoutPrefixes);
        }
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e);
      }
    }

    public static void Detach(this IEnumerable<XRef> xrefs)
    {
      try
      {
        foreach (var xref in xrefs)
        {
          xref.Database.DetachXref(xref.Block.ObjectId);
        }
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e);
      }
    }

    public static void Reload(this IEnumerable<XRef> xrefs)
    {
      try
      {
        var ids = xrefs.Select(xr => xr.Block.ObjectId)
                       .ToArray();

        if (ids.Any())
        {
          xrefs.First().Database.ReloadXrefs(new ObjectIdCollection(ids));
        }
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e);
      }
    }

    public static void Unload(this IEnumerable<XRef> xrefs)
    {
      try
      {
        var ids = xrefs.Select(xr => xr.Block.ObjectId)
                       .ToArray();

        if (ids.Any())
        {
          xrefs.First().Database.UnloadXrefs(new ObjectIdCollection(ids));
        }

      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e);
      }
    }
  }
}
