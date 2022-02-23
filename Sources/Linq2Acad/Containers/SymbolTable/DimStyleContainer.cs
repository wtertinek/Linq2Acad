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
  /// A container class that provides access to the elements of the DimStyle table. In addition to the standard LINQ operations this class provides methods to create, add and import DimStyleTableRecords.
  /// </summary>
  public sealed class DimStyleContainer : UniqueNameSymbolTableEnumerable<DimStyleTableRecord>
  {
    internal DimStyleContainer(Database database, Transaction transaction)
      : base(database, transaction, database.DimStyleTableId)
    {
    }
  }
}
