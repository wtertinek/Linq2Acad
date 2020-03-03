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
    private readonly Transaction transaction;

    internal XRef(ObjectId blockId, Database database, Transaction transaction)
      : this((BlockTableRecord)transaction.GetObject(blockId, OpenMode.ForRead), database, transaction)
    {
    }

    internal XRef(BlockTableRecord block, Database database, Transaction transaction)
    {
      Block = block;
      Database = database;
      this.transaction = transaction;
      Status = new XRefInfo(Block.XrefStatus);
    }

    internal Database Database { get; }

    public BlockTableRecord Block { get; }

    public string BlockName
    {
      get { return Block.Name; }
      set { Block.Name = value; }    
    }

    public string FilePath
    {
      get { return Block.PathName; }
      set { Block.PathName = value; }
    }

    public XRefInfo Status { get; }

    public bool IsFromAttachReference
    {
      get { return !Block.IsFromOverlayReference; }
    }

    public bool IsFromOverlayReference
    {
      get { return Block.IsFromOverlayReference; }
    }

    public void Bind()
    {
      Bind(false);
    }

    public void Bind(bool insertSymbolNamesWithoutPrefixes)
    {
      using (var idCollection = new ObjectIdCollection(new[] { Block.ObjectId }))
      {
        Database.BindXrefs(idCollection, !insertSymbolNamesWithoutPrefixes);
      }
    }

    public void Detach()
    {
      Database.DetachXref(Block.ObjectId);
    }

    public void Reload()
    {
      using (var idCollection = new ObjectIdCollection(new[] { Block.ObjectId }))
      {
        Database.ReloadXrefs(idCollection);
      }
    }

    public void Unload()
    {
      using (var idCollection = new ObjectIdCollection(new[] { Block.ObjectId }))
      {
        Database.UnloadXrefs(idCollection);
      }
    }
  }
}
