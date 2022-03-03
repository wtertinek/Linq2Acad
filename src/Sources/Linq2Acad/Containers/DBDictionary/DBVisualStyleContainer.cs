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
  /// A container class that provides access to the elements of the DBVisualStyle dictionary. In addition to the standard LINQ operations this class provides methods to create, add and import DBVisualStyles.
  /// </summary>
  public sealed class DBVisualStyleContainer : DBDictionaryEnumerable<DBVisualStyle>
  {
    internal DBVisualStyleContainer(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID, s => s.Name, () => nameof(DetailViewStyle.Name))
    {
    }
  }
}
