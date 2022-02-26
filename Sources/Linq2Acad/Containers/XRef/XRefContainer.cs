using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;

namespace Linq2Acad
{
  /// <summary>
  /// A container class that provides access to all XRef elements. In addition to the standard LINQ operations this class provides methods to attach, overlay, resolve, reload and unload XRefs.
  /// </summary>
  public class XRefContainer : IEnumerable<XRef>
  {
    private readonly Database database;
    private readonly Transaction transaction;

    internal XRefContainer(Database database, Transaction transaction)
    {
      this.database = database;
      this.transaction = transaction;
    }

    #region IEnumerable implementation

    public IEnumerator<XRef> GetEnumerator()
    {
      foreach (var block in new XRefBlockContainer(database, transaction))
      {
        yield return new XRef(database, transaction, block.ObjectId);
      }
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }

    #endregion

    /// <summary>
    /// Attaches the XRef at the given file location.
    /// </summary>
    /// <param name="fileName">The file name of the XRef.</param>
    /// <param name="blockName">The XRef's block name. If not specified, the file name is used as the XRef's block name.</param>
    /// <returns>A new instance of XRef.</returns>
    /// <exception cref="System.ArgumentNullException">Thrown when parameter <i>file name</i> is null.</exception>
    public XRef Attach(string fileName, string blockName = null)
    {
      Require.ParameterNotNull(fileName, nameof(fileName));
      Require.FileExists(fileName, nameof(fileName));

      if (blockName == null)
      {
        blockName = GetBlockName(System.IO.Path.GetFileNameWithoutExtension(fileName));
      }

      Require.IsValidSymbolName(blockName, nameof(blockName));
      Require.NameDoesNotExist<XRef>(new XRefBlockContainer(database, transaction).Contains(blockName), blockName);

      var id = database.AttachXref(fileName, blockName);
      return new XRef(database, transaction, id);
    }

    /// <summary>
    /// Overlays the XRef at the given file location.
    /// </summary>
    /// <param name="fileName">The file name of the XRef.</param>
    /// <param name="blockName">The XRef's block name. If not specified, the file name is used as the XRef's block name.</param>
    /// <returns>A new instance of XRef.</returns>
    /// <exception cref="System.ArgumentNullException">Thrown when parameter <i>file name</i> is null.</exception>
    public XRef Overlay(string fileName, string blockName = null)
    {
      Require.ParameterNotNull(fileName, nameof(fileName));
      Require.FileExists(fileName, nameof(fileName));

      if (blockName == null)
      {
        blockName = GetBlockName(System.IO.Path.GetFileNameWithoutExtension(fileName));
      }

      Require.IsValidSymbolName(blockName, nameof(blockName));
      Require.NameDoesNotExist<XRef>(new XRefBlockContainer(database, transaction).Contains(blockName), blockName);

      var id = database.OverlayXref(fileName, blockName);
      return new XRef(database, transaction, id);
    }

    /// <summary>
    /// Resolves XRefs.
    /// </summary>
    /// <param name="includeResolvedXRefs">True, if all XRefs should be resolved. By default only newly added (unresolved) XRefs are resolved.</param>
    public void Resolve(bool includeResolvedXRefs = false)
      => database.ResolveXrefs(true, !includeResolvedXRefs);

    /// <summary>
    /// Reloads all XRefs.
    /// </summary>
    public void Reload()
    {
      var xRefs = new XRefContainer(database, transaction);

      using (var idCollection = new ObjectIdCollection(xRefs.Select(x => x.Block.ObjectId).ToArray()))
      {
        database.ReloadXrefs(idCollection);
      }
    }

    /// <summary>
    /// Unloads all XRefs.
    /// </summary>
    public void Unload()
    {
      var xRefs = new XRefContainer(database, transaction);

      using (var idCollection = new ObjectIdCollection(xRefs.Select(x => x.Block.ObjectId).ToArray()))
      {
        database.UnloadXrefs(idCollection);
      }
    }

    private string GetBlockName(string baseName)
    {
      var blockName = baseName;
      int idx = 0;

      var xRefBlocks = new XRefBlockContainer(database, transaction);

      while (xRefBlocks.Contains(blockName))
      {
        blockName = baseName + "_" + idx++;
      }

      return blockName;
    }

    #region Private nested class XRefBlockContainer

    private class XRefBlockContainer : UniqueNameSymbolTableEnumerableBase<BlockTableRecord>
    {
      internal XRefBlockContainer(Database database, Transaction transaction)
        : base(database, transaction, database.BlockTableId, ids => Filter(ids, transaction))
      {
      }

      private static IEnumerable<ObjectId> Filter(IEnumerable<ObjectId> ids, Transaction transaction)
      {
        foreach (var id in ids)
        {
          var btr = (BlockTableRecord)transaction.GetObject(id, OpenMode.ForRead);

          if (btr.IsFromExternalReference)
          {
            yield return btr.ObjectId;
          }
        }
      }
    }

    #endregion
  }
}
