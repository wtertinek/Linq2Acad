using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;

namespace Linq2Acad
{
  public class XRefStatus
  {
    private BlockTableRecord block;

    internal XRefStatus(BlockTableRecord block)
    {
      this.block = block;
    }

    public bool FileNotFound
    {
      get
      {
        try
        {
          return (block.XrefStatus & XrefStatus.FileNotFound) == XrefStatus.FileNotFound;
        }
        catch (Exception e)
        {
          throw Error.AutoCadException(e);
        }
      }
    }

    public bool IsResolved
    {
      get
      {
        try
        {
          return (block.XrefStatus & XrefStatus.Resolved) == XrefStatus.Resolved;
        }
        catch (Exception e)
        {
          throw Error.AutoCadException(e);
        }
      }
    }

    public bool IsLoaded
    {
      get
      {
        try
        {
          return !((block.XrefStatus & XrefStatus.Unloaded) == XrefStatus.Unloaded);
        }
        catch (Exception e)
        {
          throw Error.AutoCadException(e);
        }
      }
    }

    public bool IsReferenced
    {
      get
      {
        try
        {
          return !((block.XrefStatus & XrefStatus.Unreferenced) == XrefStatus.Unreferenced);
        }
        catch (Exception e)
        {
          throw Error.AutoCadException(e);
        }
      }
    }
  }
}
