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
  /// A container class that provides access to the elements of the Group ditionary.
  /// </summary>
  public sealed class GroupContainer : DBDictionaryEnumerable<Group>
  {
    internal GroupContainer(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID, g => g.Name, () => nameof(Group.Name))
    {
    }

    /// <summary>
    /// Creates a new element.
    /// </summary>
    /// <param name="name">The unique name of the element.</param>
    /// <param name="entities">The entities to be added to the Group.</param>
    public Group Create(string name, IEnumerable<Entity> entities)
    {
      Require.IsValidSymbolName(name, nameof(name));
      Require.NameDoesNotExist<Group>(Contains(name), name);

      var group = AddInternal(new Group(), name);

      if (entities.Any())
      {
        using (var idCollection = new ObjectIdCollection(entities.Select(e => e.ObjectId)
                                                                  .ToArray()))
        {
          group.Append(idCollection);
        }
      }

      return group;
    }
  }
}
