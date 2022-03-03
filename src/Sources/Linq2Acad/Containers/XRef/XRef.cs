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
    private readonly Transaction transaction;
    private readonly ObjectId blockTableRecordId;

    internal XRef(Database database, Transaction transaction, ObjectId blockTableRecordId)
    {
      Database = database;
      this.transaction = transaction;
      this.blockTableRecordId = blockTableRecordId;
    }

    internal Database Database { get; }

    /// <summary>
    /// Gets a read-only version of the XRef's BlockTableRecord.
    /// </summary>
    public BlockTableRecord Block
      => (BlockTableRecord)transaction.GetObject(blockTableRecordId, OpenMode.ForRead);

    /// <summary>
    /// Gets the XRef's block name.
    /// </summary>
    public string BlockName
      => Block.Name;

    /// <summary>
    /// Gets the XRef's file path.
    /// </summary>
    public string FilePath
      => Block.PathName;

    /// <summary>
    /// True, if the XRef is not found.
    /// </summary>
    public bool FileNotFound
      => (Block.XrefStatus & XrefStatus.FileNotFound) == XrefStatus.FileNotFound;

    /// <summary>
    /// True, if the XRef is resolved.
    /// </summary>
    public bool IsResolved
      => (Block.XrefStatus & XrefStatus.Resolved) == XrefStatus.Resolved;

    /// <summary>
    /// True, if the XRef is loaded.
    /// </summary>
    public bool IsLoaded
      => !((Block.XrefStatus & XrefStatus.Unloaded) == XrefStatus.Unloaded);

    /// <summary>
    /// True, if the XRef is referenced.
    /// </summary>
    public bool IsReferenced
      => !((Block.XrefStatus & XrefStatus.Unreferenced) == XrefStatus.Unreferenced);

    /// <summary>
    /// True, if the XRef is from an attach reference.
    /// </summary>
    public bool IsFromAttachReference
      => !Block.IsFromOverlayReference;

    /// <summary>
    /// True, if the XRef is from an overlay reference.
    /// </summary>
    public bool IsFromOverlayReference
      => Block.IsFromOverlayReference;

    /// <summary>
    /// Updates the XRef's block name.
    /// </summary>
    /// <param name="blockName">The name to set.</param>
    public void UpdateBlockName(string blockName)
    {
      var block = (BlockTableRecord)transaction.GetObject(blockTableRecordId, OpenMode.ForWrite);
      block.Name = blockName;
    }

    /// <summary>
    /// Updates the XRef's file path.
    /// </summary>
    /// <param name="filePath">The file path to set.</param>
    public void UpdateFilePath(string filePath)
    {
      var block = (BlockTableRecord)transaction.GetObject(blockTableRecordId, OpenMode.ForWrite);
      block.PathName = filePath;
    }

    /// <summary>
    /// Binds the XRef.
    /// </summary>
    /// <param name="insertSymbolNamesWithoutPrefixes">If set to true, the SymbolTableRecord names will be changed from the XRef naming convention to normal insert block names.</param>
    public void Bind(bool insertSymbolNamesWithoutPrefixes = false)
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
