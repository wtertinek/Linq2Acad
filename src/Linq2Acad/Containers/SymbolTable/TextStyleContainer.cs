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
  /// A container class that provides access to the elements of the TextStyle table. In addition to the standard LINQ operations this class provides methods to create, add and import TextStyleTableRecords.
  /// </summary>
  public sealed class TextStyleContainer : UniqueNameSymbolTableEnumerable<TextStyleTableRecord>
  {
    internal TextStyleContainer(Database database, Transaction transaction)
      : base(database, transaction, database.TextStyleTableId)
    {
    }
  }
}
