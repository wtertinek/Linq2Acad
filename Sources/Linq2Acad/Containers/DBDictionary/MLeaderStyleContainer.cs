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
  /// A container class that provides access to the elements of the MLeaderStyle ditionary.
  /// </summary>
  public sealed class MLeaderStyleContainer : DBDictionaryEnumerable<MLeaderStyle>
  {
    internal MLeaderStyleContainer(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID)
    {
    }

    /// <summary>
    /// Creates a new MLeaderStyle element.
    /// </summary>
    /// <param name="name">The unique name of the MLeaderStyle element.</param>
    public MLeaderStyle Create(string name)
    {
      Require.IsValidSymbolName(name, nameof(name));
      Require.NameDoesNotExist<MLeaderStyle>(Contains(name), name);

      return AddInternal(new MLeaderStyle(), name);
    }

    /// <summary>
    /// Adds a newly created MLeaderStyle element.
    /// </summary>
    /// <param name="element">The MLeaderStyle to element add.</param>
    public void Add(MLeaderStyle element)
    {
      Require.ParameterNotNull(element, nameof(element));
      Require.IsValidSymbolName(element.Name, nameof(element.Name));
      Require.NameDoesNotExist<MLeaderStyle>(Contains(element.Name), element.Name);

      AddInternal(element, element.Name);
    }

    /// <summary>
    /// Adds a collection of newly created MLeaderStyle elements.
    /// </summary>
    /// <param name="elements">The MLeaderStyle elements to add.</param>
    public void AddRange(IEnumerable<MLeaderStyle> elements)
    {
      Require.ParameterNotNull(elements, nameof(elements));

      foreach (var element in elements)
      {
        Require.ParameterNotNull(element, nameof(element));
        Require.IsValidSymbolName(element.Name, nameof(element.Name));
        Require.NameDoesNotExist<MLeaderStyle>(Contains(element.Name), element.Name);
      }

      AddRangeInternal(elements.Select(i => Tuple.Create(i, i.Name)));
    }
  }
}
