using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;

namespace Linq2Acad
{
  /// <summary>
  /// This class represents the status of a XRef.
  /// </summary>
  public class XRefInfo
  {
    private readonly XrefStatus status;

    internal XRefInfo(XrefStatus status)
      => this.status = status;

    /// <summary>
    /// True, if the XRef is not found.
    /// </summary>
    public bool FileNotFound
      => (status & XrefStatus.FileNotFound) == XrefStatus.FileNotFound;

    /// <summary>
    /// True, if the XRef is resolved.
    /// </summary>
    public bool IsResolved
      => (status & XrefStatus.Resolved) == XrefStatus.Resolved;

    /// <summary>
    /// True, if the XRef is loaded.
    /// </summary>
    public bool IsLoaded
      => !((status & XrefStatus.Unloaded) == XrefStatus.Unloaded);

    /// <summary>
    /// True, if the XRef is referenced.
    /// </summary>
    public bool IsReferenced
      => !((status & XrefStatus.Unreferenced) == XrefStatus.Unreferenced);
  }
}
