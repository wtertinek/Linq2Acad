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
  /// A container class that provides access to the elements of the Ucs table.
  /// </summary>
  public sealed class UcsContainer : UniqueNameSymbolTableEnumerable<UcsTableRecord>
  {
    /// <summary>
    /// Creates a new instance of UcsContainer.
    /// </summary>
    /// <param name="database">The drawing database to use.</param>
    /// <param name="transaction">The transaction to use.</param>
    internal UcsContainer(Database database, Transaction transaction)
      : base(database, transaction, database.UcsTableId)
    {
    }
  }
}
