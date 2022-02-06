using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Linq2Acad
{
  /// <summary>
  /// A container class that provides access to the elements of the Viewport table.
  /// </summary>
  public sealed class ViewportContainer : NonUniqueNameSymbolTableEnumerable<ViewportTableRecord>
  {
    /// <summary>
    /// Creates a new instance of ViewportContainer.
    /// </summary>
    /// <param name="database">The drawing database to use.</param>
    /// <param name="transaction">The transaction to use.</param>
    internal ViewportContainer(Database database, Transaction transaction)
      : base(database, transaction, database.ViewportTableId)
    {
    }

    /// <summary>
    /// Returns the current Viewport or null, if there is no current Viewport.
    /// </summary>
    /// <exception cref="System.Exception">Thrown when an AutoCAD error occurs.</exception>
    public ViewportTableRecord Current
      => database.CurrentViewportTableRecordId.IsValid
           ? (ViewportTableRecord)transaction.GetObject(database.CurrentViewportTableRecordId, OpenMode.ForRead)
           : null;
  }
}
