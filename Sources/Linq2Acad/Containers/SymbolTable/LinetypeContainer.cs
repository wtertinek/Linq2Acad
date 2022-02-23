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
  /// A container class that provides access to the elements of the Linetype table. In addition to the standard LINQ operations this class provides methods to create, add and import LinetypeTableRecords.
  /// </summary>
  public sealed class LinetypeContainer : UniqueNameSymbolTableEnumerable<LinetypeTableRecord>
  {
    internal LinetypeContainer(Database database, Transaction transaction)
      : base(database, transaction, database.LinetypeTableId)
    {
    }
  }
}
