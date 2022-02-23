using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;

namespace Linq2Acad
{
  /// <summary>
  /// A container class that provides access to the XRef elements.
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
        yield return new XRef((BlockTableRecord)transaction.GetObject(block.ObjectId, OpenMode.ForRead), database);
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
    /// <returns>A new instance of XRef.</returns>
    /// <exception cref="System.ArgumentNullException">Thrown when parameter <i>fileName</i> is null.</exception>
    public XRef Attach(string fileName)
    {
      Require.ParameterNotNull(fileName, nameof(fileName));
      Require.FileExists(fileName, nameof(fileName));

      var baseName = System.IO.Path.GetFileNameWithoutExtension(fileName);
      Require.IsValidSymbolName(baseName, nameof(fileName));

      return AttachInternal(fileName, GetBlockName(baseName));
    }

    /// <summary>
    /// Attaches the XRef at the given file location.
    /// </summary>
    /// <param name="fileName">The file name of the XRef.</param>
    /// <param name="blockName">The XRef's block name.</param>
    /// <returns>A new instance of XRef.</returns>
    /// <exception cref="System.ArgumentNullException">Thrown when parameters <i>file name</i> or <i>block name</i> is null.</exception>
    public XRef Attach(string fileName, string blockName)
    {
      Require.ParameterNotNull(fileName, nameof(fileName));
      Require.FileExists(fileName, nameof(fileName));
      Require.ParameterNotNull(blockName, nameof(blockName));
      Require.IsValidSymbolName(blockName, nameof(blockName));
      Require.NameDoesNotExist<XRef>(new XRefBlockContainer(database, transaction).Contains(blockName), blockName);

      return AttachInternal(fileName, blockName);
    }

    /// <summary>
    /// Attaches the XRef at the given file location.
    /// </summary>
    /// <param name="fileName">The file name of the XRef.</param>
    /// <param name="blockName">The XRef's block name.</param>
    /// <returns>A new instance of XRef.</returns>
    private XRef AttachInternal(string fileName, string blockName)
    {
      var id = database.AttachXref(fileName, blockName);
      return new XRef((BlockTableRecord)transaction.GetObject(id, OpenMode.ForRead), database);
    }

    /// <summary>
    /// Overlays the XRef at the given file location.
    /// </summary>
    /// <param name="fileName">The file name of the XRef.</param>
    /// <returns>A new instance of XRef.</returns>
    /// <exception cref="System.ArgumentNullException">Thrown when parameter <i>fileName</i> is null.</exception>
    public XRef Overlay(string fileName)
    {
      Require.ParameterNotNull(fileName, nameof(fileName));
      Require.FileExists(fileName, nameof(fileName));

      var baseName = System.IO.Path.GetFileNameWithoutExtension(fileName);
      Require.IsValidSymbolName(baseName, nameof(fileName));

      return OverlayInternal(fileName, GetBlockName(baseName));
    }

    /// <summary>
    /// Overlays the XRef at the given file location.
    /// </summary>
    /// <param name="fileName">The file name of the XRef.</param>
    /// <param name="blockName">The XRef's block name.</param>
    /// <returns>A new instance of XRef.</returns>
    /// <exception cref="System.ArgumentNullException">Thrown when parameters <i>file name</i> or <i>block name</i> is null.</exception>
    public XRef Overlay(string fileName, string blockName)
    {
      Require.ParameterNotNull(fileName, nameof(fileName));
      Require.FileExists(fileName, nameof(fileName));

      Require.IsValidSymbolName(blockName, nameof(blockName));
      Require.NameDoesNotExist<XRef>(new XRefBlockContainer(database, transaction).Contains(blockName), blockName);

      return OverlayInternal(fileName, blockName);
    }

    /// <summary>
    /// Overlays the XRef at the given file location.
    /// </summary>
    /// <param name="fileName">The file name of the XRef.</param>
    /// <param name="blockName">The XRef's block name.</param>
    /// <returns>A new instance of XRef.</returns>
    private XRef OverlayInternal(string fileName, string blockName)
    {
      var id = database.OverlayXref(fileName, blockName);
      return new XRef((BlockTableRecord)transaction.GetObject(id, OpenMode.ForRead), database);
    }

    /// <summary>
    /// Resolves existing XRefs in the working database.
    /// </summary>
    public void Resolve()
    {
      database.ResolveXrefs(true, false);
    }

    /// <summary>
    /// Resolves existing XRefs in the working database.
    /// </summary>
    /// <param name="onlyNewlyAdded">True, if only newly added XRefs should be processed.</param>
    public void Resolve(bool onlyNewlyAdded)
    {
      database.ResolveXrefs(true, onlyNewlyAdded);
    }

    /// <summary>
    /// Adds an index in case a block with <i>baseName</i> already exists.
    /// </summary>
    /// <param name="baseName">The base name.</param>
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
