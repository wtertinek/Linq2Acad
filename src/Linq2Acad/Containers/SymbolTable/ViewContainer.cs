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
  /// A container class that provides access to the elements of the View table. In addition to the standard LINQ operations this class provides methods to create, add and import ViewTableRecords.
  /// </summary>
  public sealed class ViewContainer : UniqueNameSymbolTableEnumerable<ViewTableRecord>
  {
    internal ViewContainer(Database database, Transaction transaction)
      : base(database, transaction, database.ViewTableId)
    {
    }
  }
}
