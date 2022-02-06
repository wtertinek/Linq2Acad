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
  /// A container class that provides access to the elements of the Linetype table.
  /// </summary>
  public sealed class LinetypeContainer : UniqueNameSymbolTableEnumerable<LinetypeTableRecord>
  {
    /// <summary>
    /// Creates a new instance of LinetypeContainer.
    /// </summary>
    /// <param name="database">The drawing database to use.</param>
    /// <param name="transaction">The transaction to use.</param>
    internal LinetypeContainer(Database database, Transaction transaction)
      : base(database, transaction, database.LinetypeTableId)
    {
    }
  }
}
