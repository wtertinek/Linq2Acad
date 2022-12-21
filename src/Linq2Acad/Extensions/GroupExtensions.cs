using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2Acad
{
  /// <summary>
  /// Extension records for instance of Group.
  /// </summary>
  public static class GroupExtensions
  {   /// <summary>
    /// Appends ids to group. (Note, this crystalises the ids to an array internally).
    /// </summary>
    /// <param name="group"></param>
    /// <param name="ids"></param>
    public static void Append(this Group group, IEnumerable<ObjectId> ids)
    {
      Require.ParameterNotNull(group, nameof(group));
      Require.ParameterNotNull(ids, nameof(ids));

      ObjectId[] idsArray = ids.ToArray();

      if (idsArray.Length > 0)
      {
        using (ObjectIdCollection idCollection = new ObjectIdCollection(idsArray))
        {          
          group.Append(idCollection); 
        }
      }
    }

    /// <summary>
    /// Appends entities to group. (Note, this crystalises the entities to an object id array internally).
    /// </summary>
    /// <param name="group"></param>
    /// <param name="entities"></param>
    public static void Append(this Group group, IEnumerable<Entity> entities)
    {
      Require.ParameterNotNull(group, nameof(group));
      Require.ParameterNotNull(entities, nameof(entities));

      group.Append(entities.Select(e => e.ObjectId));
    }

    /// <summary>
    /// Prepends ids to group. (Note, this crystalises the ids to an array internally).
    /// </summary>
    /// <param name="group"></param>
    /// <param name="ids"></param>
    public static void Prepend(this Group group, IEnumerable<ObjectId> ids)
    {
      Require.ParameterNotNull(group, nameof(group));
      Require.ParameterNotNull(ids, nameof(ids));

      ObjectId[] idsArray = ids.ToArray();

      if (idsArray.Length > 0)
      {
        using (ObjectIdCollection idCollection = new ObjectIdCollection(idsArray))
        {          
          group.Prepend(idCollection); 
        }
      }
    }

    /// <summary>
    /// Prepends entities to group. (Note, this crystalises the entities to an object id array internally).
    /// </summary>
    /// <param name="group"></param>
    /// <param name="entities"></param>
    public static void Prepend(this Group group, IEnumerable<Entity> entities)
    {
      Require.ParameterNotNull(group, nameof(group));
      Require.ParameterNotNull(entities, nameof(entities));

      group.Prepend(entities.Select(e => e.ObjectId));
    }

    /// <summary>
    /// Appends ids to group at the index required. (Note, this crystalises the ids to an array internally).
    /// </summary>
    /// <param name="group"></param>
    /// <param name="index"></param>
    /// <param name="ids"></param>
    public static void InsertAt(this Group group, int index, IEnumerable<ObjectId> ids)
    {
      Require.ParameterNotNull(group, nameof(group));
      Require.ParameterNotNegative(index, nameof(index));
      Require.ParameterNotNull(ids, nameof(ids));

      ObjectId[] idsArray = ids.ToArray();

      if (idsArray.Any())
      {
        using (ObjectIdCollection idCollection = new ObjectIdCollection(idsArray))
        {          
          group.InsertAt(index, idCollection); 
        }
      }
    }

    /// <summary>
    /// Appends entities to group at the index required. (Note, this crystalises the entities to an ids array internally).
    /// </summary>
    /// <param name="group"></param>
    /// <param name="index"></param>
    /// <param name="entities"></param>
    public static void InsertAt(this Group group, int index, IEnumerable<Entity> entities)
    {
      Require.ParameterNotNull(group, nameof(group));
      Require.ParameterNotNegative(index, nameof(index));
      Require.ParameterNotNull(entities, nameof(entities));

      group.InsertAt(index, entities.Select(e => e.ObjectId));
    }

    /// <summary>
    /// Removes ids from group at the index required. (Note, this crystalises the ids to an array internally).
    /// </summary>
    /// <param name="group"></param>
    /// <param name="ids"></param>
    public static void Remove(this Group group, IEnumerable<ObjectId> ids)
    {
      Require.ParameterNotNull(group, nameof(group));
      Require.ParameterNotNull(ids, nameof(ids));

      ObjectId[] idsArray = ids.ToArray();

      if (idsArray.Any())
      {
        using (ObjectIdCollection idCollection = new ObjectIdCollection(idsArray))
        {          
          group.Remove(idCollection); 
        }
      }
    }

    /// <summary>
    /// Removes entities from group at the index required. (Note, this crystalises the entity to an ids array, internally).
    /// </summary>
    /// <param name="group"></param>
    /// <param name="entities"></param>
    public static void Remove(this Group group, IEnumerable<Entity> entities)
    {
      Require.ParameterNotNull(group, nameof(group));
      Require.ParameterNotNull(entities, nameof(entities));

      group.Remove(entities.Select(e => e.ObjectId));
    }

    /// <summary>
    /// Removes ids from the group (i.e. the objects whose object IDs are in the ids enumerable). All objects must be in the group and be at index locations equal to or higher than index. (Note, this crystalises the ids to an array internally).
    /// </summary>
    /// <param name="group"></param>
    /// <param name="index"></param>
    /// <param name="ids"></param>
    public static void RemoveAt(this Group group, int index, IEnumerable<ObjectId> ids)
    {
      Require.ParameterNotNull(group, nameof(group));
      Require.ParameterNotNegative(index, nameof(index));
      Require.ParameterNotNull(ids, nameof(ids));

      ObjectId[] idsArray = ids.ToArray();

      if (idsArray.Any())
      {
        using (ObjectIdCollection idCollection = new ObjectIdCollection(idsArray))
        {          
          group.RemoveAt(index, idCollection); 
        }
      }
    }

    /// <summary>
    /// Removes entities from the group. All objects must be in the group and be at index locations equal to or higher than index. (Note, this crystalises the entities into an ids array internally).
    /// </summary>
    /// <param name="group"></param>
    /// <param name="index"></param>
    /// <param name="entities"></param>
    public static void RemoveAt(this Group group, int index, IEnumerable<Entity> entities)
    {
      Require.ParameterNotNull(group, nameof(group));
      Require.ParameterNotNegative(index, nameof(index));
      Require.ParameterNotNull(entities, nameof(entities));

      group.RemoveAt(index, entities.Select(e => e.ObjectId));
    }
  }
}
