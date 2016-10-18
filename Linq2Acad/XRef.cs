using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;

namespace Linq2Acad
{
  public class XRef
  {
    private Database database;

    internal XRef(BlockTableRecord block, Database database)
    {
      Block = block;
      this.database = database;
    }

    public BlockTableRecord Block { get; private set; }

    public string BlockName
    {
      get
      {
        try
        {
          return Block.Name;
        }
        catch (Exception e)
        {
          throw Error.AutoCadException(e);
        }
      }
    }

    public string FilePath
    {
      get
      {
        try
        {
          return Block.PathName;
        }
        catch (Exception e)
        {
          throw Error.AutoCadException(e);
        }
      }
    }

    public bool IsFromAttachReference
    {
      get
      {
        try
        {
          return !Block.IsFromOverlayReference;
        }
        catch (Exception e)
        {
          throw Error.AutoCadException(e);
        }
      }
    }

    public bool IsFromOverlayReference
    {
      get
      {
        try
        {
          return Block.IsFromOverlayReference;
        }
        catch (Exception e)
        {
          throw Error.AutoCadException(e);
        }
      }
    }

    public bool IsFileNotFound
    {
      get
      {
        try
        {
          return (Block.XrefStatus & XrefStatus.FileNotFound) == XrefStatus.FileNotFound;
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
          return (Block.XrefStatus & XrefStatus.Resolved) == XrefStatus.Resolved;
        }
        catch (Exception e)
        {
          throw Error.AutoCadException(e);
        }
      }
    }

    public bool IsUnloaded
    {
      get
      {
        try
        {
          return (Block.XrefStatus & XrefStatus.Unloaded) == XrefStatus.Unloaded;
        }
        catch (Exception e)
        {
          throw Error.AutoCadException(e);
        }
      }
    }

    public bool IsUnreferenced
    {
      get
      {
        try
        {
          return (Block.XrefStatus & XrefStatus.Unreferenced) == XrefStatus.Unreferenced;
        }
        catch (Exception e)
        {
          throw Error.AutoCadException(e);
        }
      }
    }

    public bool IsUnresolved
    {
      get
      {
        try
        {
          return (Block.XrefStatus & XrefStatus.Unresolved) == XrefStatus.Unresolved;
        }
        catch (Exception e)
        {
          throw Error.AutoCadException(e);
        }
      }
    }

    public void Bind(bool insertBind)
    {
      try
      {
        database.BindXrefs(new ObjectIdCollection(new [] { Block.ObjectId }), insertBind);
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e);
      }
    }

    public void Detach()
    {
      try
      {
        database.DetachXref(Block.ObjectId);
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e);
      }
    }

    public void Reload()
    {
      try
      {
        database.ReloadXrefs(new ObjectIdCollection(new[] { Block.ObjectId }));
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e);
      }
    }

    public void Unload()
    {
      try
      {
        database.UnloadXrefs(new ObjectIdCollection(new[] { Block.ObjectId }));
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e);
      }
    }
  }
}
