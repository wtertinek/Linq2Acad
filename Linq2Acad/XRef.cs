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
    private Transaction transaction;

    internal XRef(ObjectId blockId, Database database, Transaction transaction)
      : this((BlockTableRecord)transaction.GetObject(blockId, OpenMode.ForRead), database, transaction)
    {
    }

    internal XRef(BlockTableRecord block, Database database, Transaction transaction)
    {
      this.Database = database;
      this.transaction = transaction;
      Block = block;
      Status = new XRefStatus(Block);
    }

    internal Database Database { get; private set; }

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
      set
      {
        try
        {
          Block.Name = value;
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
      set
      {
        try
        {
          Block.PathName = value;
        }
        catch (Exception e)
        {
          throw Error.AutoCadException(e);
        }
      }
    }

    public XRefStatus Status { get; private set; }

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

    public void Bind()
    {
      Bind(false);
    }

    public void Bind(bool insertSymbolNamesWithoutPrefixes)
    {
      try
      {
        Database.BindXrefs(new ObjectIdCollection(new[] { Block.ObjectId }), !insertSymbolNamesWithoutPrefixes);
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
        Database.DetachXref(Block.ObjectId);
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
        Database.ReloadXrefs(new ObjectIdCollection(new[] { Block.ObjectId }));
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
        Database.UnloadXrefs(new ObjectIdCollection(new[] { Block.ObjectId }));
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e);
      }
    }
  }
}
