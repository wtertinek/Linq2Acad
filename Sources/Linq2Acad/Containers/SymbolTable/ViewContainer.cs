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
  /// A container class that provides access to the elements of the View table.
  /// </summary>
  public sealed class ViewContainer : UniqueNameSymbolTableEnumerable<ViewTableRecord>
  {
    /// <summary>
    /// Creates a new instance of ViewContainer.
    /// </summary>
    /// <param name="database">The drawing database to use.</param>
    /// <param name="transaction">The transaction to use.</param>
    internal ViewContainer(Database database, Transaction transaction)
      : base(database, transaction, database.ViewTableId)
    {
    }
  }
}
