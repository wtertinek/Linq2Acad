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
    private Database database;
    private Transaction transaction;
    private XRefBlockContainer xRefBlockContainer;

    /// <summary>
    /// Creates a new instance of XRefContainer.
    /// </summary>
    /// <param name="database">The drawing database to use.</param>
    /// <param name="transaction">The transaction to use.</param>
    internal XRefContainer(Database database, Transaction transaction)
    {
      this.database = database;
      this.transaction = transaction;
      xRefBlockContainer = new XRefBlockContainer(database, transaction);
    }

    #region IEnumerable implementation

    public IEnumerator<XRef> GetEnumerator()
    {
      foreach (var block in xRefBlockContainer)
      {
        yield return new XRef(block, database, transaction);
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
      if (fileName == null) throw Error.ArgumentNull("fileName");
      if (!System.IO.File.Exists(fileName)) throw Error.FileNotFound(fileName);

      var baseName = System.IO.Path.GetFileNameWithoutExtension(fileName);
      if (!Helpers.IsNameValid(baseName)) throw Error.InvalidName(baseName);

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
      if (fileName == null) throw Error.ArgumentNull("fileName");
      if (!System.IO.File.Exists(fileName)) throw Error.FileNotFound(fileName);

      if (blockName == null) throw Error.ArgumentNull("blockName");
      if (!Helpers.IsNameValid(blockName)) throw Error.InvalidName(blockName);
      if (xRefBlockContainer.Contains(blockName)) throw Error.ObjectExists<XRef>(blockName);

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
      try
      {
        var id = database.AttachXref(fileName, blockName);
        return new XRef(id, database, transaction);
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e);
      }
    }

    /// <summary>
    /// Overlays the XRef at the given file location.
    /// </summary>
    /// <param name="fileName">The file name of the XRef.</param>
    /// <returns>A new instance of XRef.</returns>
    /// <exception cref="System.ArgumentNullException">Thrown when parameter <i>fileName</i> is null.</exception>
    public XRef Overlay(string fileName)
    {
      if (fileName == null) throw Error.ArgumentNull("fileName");
      if (!System.IO.File.Exists(fileName)) throw Error.FileNotFound(fileName);

      var baseName = System.IO.Path.GetFileNameWithoutExtension(fileName);
      if (!Helpers.IsNameValid(baseName)) throw Error.InvalidName(baseName);

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
      if (fileName == null) throw Error.ArgumentNull("fileName");
      if (!System.IO.File.Exists(fileName)) throw Error.FileNotFound(fileName);

      if (blockName == null) throw Error.ArgumentNull("blockName");
      if (!Helpers.IsNameValid(blockName)) throw Error.InvalidName(blockName);
      if (xRefBlockContainer.Contains(blockName)) throw Error.ObjectExists<XRef>(blockName);

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
      try
      {
        var id = database.OverlayXref(fileName, blockName);
        return new XRef(id, database, transaction);
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e);
      }
    }

    public void Resolve()
    {
      try
      {
        database.ResolveXrefs(true, false);
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e);
      }
    }

    public void Resolve(bool onlyNewlyAdded)
    {
      try
      {
        database.ResolveXrefs(true, onlyNewlyAdded);
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e);
      }
    }

    /// <summary>
    /// Adds an index in case a block with <i>baseName</i> already exists.
    /// </summary>
    /// <param name="baseName">The base name.</param>
    private string GetBlockName(string baseName)
    {
      var blockName = baseName;
      int idx = 0;

      while (xRefBlockContainer.Contains(blockName))
      {
        blockName = baseName + "_" + idx++;
      }

      return blockName;
    }
  }
}
