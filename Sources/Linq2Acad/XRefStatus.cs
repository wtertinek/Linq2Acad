using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;

namespace Linq2Acad
{
  public class XRefInfo
  {
    private readonly XrefStatus status;

    internal XRefInfo(XrefStatus status)
    {
      this.status = status;
    }

    public bool FileNotFound => (status & XrefStatus.FileNotFound) == XrefStatus.FileNotFound;

    public bool IsResolved => (status & XrefStatus.Resolved) == XrefStatus.Resolved;

    public bool IsLoaded => !((status & XrefStatus.Unloaded) == XrefStatus.Unloaded);

    public bool IsReferenced => !((status & XrefStatus.Unreferenced) == XrefStatus.Unreferenced);
  }
}
