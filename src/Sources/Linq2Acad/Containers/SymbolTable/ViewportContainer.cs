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
  /// A container class that provides access to the elements of the Viewport table. In addition to the standard LINQ operations this class provides methods to create, add and import ViewportTableRecords.
  /// </summary>
  public sealed class ViewportContainer : NonUniqueNameSymbolTableEnumerable<ViewportTableRecord>
  {
    internal ViewportContainer(Database database, Transaction transaction)
      : base(database, transaction, database.ViewportTableId)
    {
    }

    /// <summary>
    /// Returns the current Viewport or null, if there is no current Viewport.
    /// </summary>
    public ViewportTableRecord Current
    {
      get
      {
        Require.NotDisposed(database.IsDisposed, nameof(AcadDatabase));
        Require.TransactionNotDisposed(transaction.IsDisposed);

        return database.CurrentViewportTableRecordId.IsValid
                 ? (ViewportTableRecord)transaction.GetObject(database.CurrentViewportTableRecordId, OpenMode.ForRead)
                 : null;

      }
    }
  }
}
