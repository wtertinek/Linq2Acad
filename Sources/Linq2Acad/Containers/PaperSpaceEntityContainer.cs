using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;

namespace Linq2Acad
{
  /// <summary>
  /// Represents a container that holds all Entity objects of a paper space layout.
  /// </summary>
  public class PaperSpaceEntityContainer : EntityContainer
  {
    internal PaperSpaceEntityContainer(Database database, Transaction transaction, ObjectId containerID, string name)
      : base(database, transaction, containerID)
      => Name = name;

    internal string Name { get; }
  }
}
