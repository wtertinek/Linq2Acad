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
        yield return new XRef(block, database);
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
    /// <param name="blockName">The XRef's block name.</param>
    /// <returns>A new instance of BlockTableRecord.</returns>
    /// <exception cref="System.ArgumentNullException">Thrown when parameters <i>file name</i> or <i>block name</i> is null.</exception>
    public XRef Attach(string fileName, string blockName)
    {
      if (fileName == null) throw Error.ArgumentNull("fileName");
      if (!System.IO.File.Exists(fileName)) throw Error.FileNotFound(fileName);

      if (blockName == null) throw Error.ArgumentNull("blockName");
      if (!Helpers.IsNameValid(blockName)) throw Error.InvalidName(blockName);
      if (xRefBlockContainer.Contains(blockName)) throw Error.ObjectExists<XRef>(blockName);

      try
      {
        var id = database.AttachXref(fileName, blockName);
        return new XRef(transaction.GetObject(id, OpenMode.ForRead) as BlockTableRecord, database);
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
    /// <param name="blockName">The XRef's block name.</param>
    /// <returns>A new instance of BlockTableRecord.</returns>
    /// <exception cref="System.ArgumentNullException">Thrown when parameters <i>file name</i> or <i>block name</i> is null.</exception>
    public XRef Overlay(string fileName, string blockName)
    {
      if (fileName == null) throw Error.ArgumentNull("fileName");
      if (!System.IO.File.Exists(fileName)) throw Error.FileNotFound(fileName);

      if (blockName == null) throw Error.ArgumentNull("blockName");
      if (!Helpers.IsNameValid(blockName)) throw Error.InvalidName(blockName);
      if (xRefBlockContainer.Contains(blockName)) throw Error.ObjectExists<XRef>(blockName);

      try
      {
        var id = database.OverlayXref(fileName, blockName);
        return new XRef(transaction.GetObject(id, OpenMode.ForRead) as BlockTableRecord, database);
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e);
      }
    }

    public void Resolve()
    {
      database.ResolveXrefs(true, false);
    }

    public void Resolve(bool onlyNewlyAdded)
    {
      database.ResolveXrefs(true, onlyNewlyAdded);
    }
  }
}
