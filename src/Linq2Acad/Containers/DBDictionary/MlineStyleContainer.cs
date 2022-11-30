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
  /// A container class that provides access to the elements of the MlineStyle dictionary. In addition to the standard LINQ operations this class provides methods to create, add and import MlineStyles.
  /// </summary>
  public sealed class MlineStyleContainer : DBDictionaryEnumerable<MlineStyle>
  {
    internal MlineStyleContainer(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID, s => s.Name, () => nameof(MlineStyle.Name))
    {
    }
  }
}
