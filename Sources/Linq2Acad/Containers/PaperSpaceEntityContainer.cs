using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;

namespace Linq2Acad
{
  /// <summary>
  /// A container class that provides access to the Entities of a paper space layout. In addition to the standard LINQ operations this class provides methods to add, import and clear Entities.
  /// </summary>
  public class PaperSpaceEntityContainer : EntityContainer
  {
    internal PaperSpaceEntityContainer(Database database, Transaction transaction, ObjectId containerID, string name)
      : base(database, transaction, containerID)
      => Name = name;

    internal string Name { get; }
  }
}
