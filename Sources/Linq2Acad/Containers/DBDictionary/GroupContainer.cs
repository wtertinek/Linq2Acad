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
      : base(database, transaction, containerID)
    {
    }

    /// <summary>
    /// Creates a new Group element.
    /// </summary>
    /// <param name="name">The unique name of the Group element.</param>
    public Group Create(string name)
    {
      Require.IsValidSymbolName(name, nameof(name));
      Require.NameDoesNotExist<Group>(Contains(name), name);

      return AddInternal(new Group(), name);
    }

    /// <summary>
    /// Creates a new DBVisualStyle element.
    /// </summary>
    /// <param name="name">The unique name of the DBVisualStyle element.</param>
    /// <param name="entities">The entities to be added to the group.</param>
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

    /// <summary>
    /// Adds a newly created Group element.
    /// </summary>
    /// <param name="element">The Group element to add.</param>
    public void Add(Group element)
    {
      Require.ParameterNotNull(element, nameof(element));
      Require.IsValidSymbolName(element.Name, nameof(element.Name));
      Require.NameDoesNotExist<Group>(Contains(element.Name), element.Name);

      AddInternal(element, element.Name);
    }

    /// <summary>
    /// Adds a collection of newly created Group elements.
    /// </summary>
    /// <param name="elements">The Group elements to add.</param>
    public void AddRange(IEnumerable<Group> elements)
    {
      Require.ParameterNotNull(elements, nameof(elements));

      foreach (var element in elements)
      {
        Require.ParameterNotNull(element, nameof(element));
        Require.IsValidSymbolName(element.Name, nameof(element.Name));
        Require.NameDoesNotExist<Group>(Contains(element.Name), element.Name);
      }

      AddRangeInternal(elements.Select(i => Tuple.Create(i, i.Name)));
    }
  }
}
