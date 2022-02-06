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
  /// A container class that provides access to the elements of the TextStyle table.
  /// </summary>
  public sealed class TextStyleContainer : UniqueNameSymbolTableEnumerable<TextStyleTableRecord>
  {
    /// <summary>
    /// Creates a new instance of TextStyleContainer.
    /// </summary>
    /// <param name="database">The drawing database to use.</param>
    /// <param name="transaction">The transaction to use.</param>
    internal TextStyleContainer(Database database, Transaction transaction)
      : base(database, transaction, database.TextStyleTableId)
    {
    }
  }
}
