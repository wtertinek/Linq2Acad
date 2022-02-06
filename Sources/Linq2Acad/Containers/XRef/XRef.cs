using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;

namespace Linq2Acad
{
  /// <summary>
  /// This class represents an XRef.
  /// </summary>
  public class XRef
  {
    internal XRef(BlockTableRecord block, Database database)
    {
      Block = block;
      Database = database;
      Status = new XRefInfo(Block.XrefStatus);
    }

    internal Database Database { get; }

    /// <summary>
    /// The XRef BlockTableRecord.
    /// </summary>
    public BlockTableRecord Block { get; }

    /// <summary>
    /// Gets or sets the block name of the XRef.
    /// </summary>
    public string BlockName
    {
      get => Block.Name;
      set => Block.Name = value;
    }

    /// <summary>
    /// Gets or sets the file path of the XRef.
    /// </summary>
    public string FilePath
    {
      get => Block.PathName;
      set => Block.PathName = value;
    }

    /// <summary>
    /// Gets the status of the XRef.
    /// </summary>
    public XRefInfo Status { get; }

    /// <summary>
    /// True, if the XRef is from an attached reference.
    /// </summary>
    public bool IsFromAttachReference
      => !Block.IsFromOverlayReference;

    /// <summary>
    /// True, if the XRef is from an overlay reference.
    /// </summary>
    public bool IsFromOverlayReference
      => Block.IsFromOverlayReference;

    /// <summary>
    /// Binds the XRef.
    /// </summary>
    public void Bind()
      => Bind(false);

    /// <summary>
    /// Binds the XRef.
    /// </summary>
    /// <param name="insertSymbolNamesWithoutPrefixes">If set to true, the SymbolTableRecord names will be changed from the XRef naming convention to normal insert block names.</param>
    public void Bind(bool insertSymbolNamesWithoutPrefixes)
    {
      using (var idCollection = new ObjectIdCollection(new[] { Block.ObjectId }))
      {
        Database.BindXrefs(idCollection, !insertSymbolNamesWithoutPrefixes);
      }
    }

    /// <summary>
    /// Detaches the XRef.
    /// </summary>
    public void Detach()
      => Database.DetachXref(Block.ObjectId);

    /// <summary>
    /// Reloads the XRef.
    /// </summary>
    public void Reload()
    {
      using (var idCollection = new ObjectIdCollection(new[] { Block.ObjectId }))
      {
        Database.ReloadXrefs(idCollection);
      }
    }

    /// <summary>
    /// Unloads the XRef.
    /// </summary>
    public void Unload()
    {
      using (var idCollection = new ObjectIdCollection(new[] { Block.ObjectId }))
      {
        Database.UnloadXrefs(idCollection);
      }
    }
  }
}
