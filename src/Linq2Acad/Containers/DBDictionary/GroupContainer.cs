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
    /// A container class that provides access to the elements of the Group dictionary In addition to the standard LINQ operations this class provides methods to create, add and import Groups.
    /// </summary>
    public sealed class GroupContainer : DBDictionaryEnumerable<Group>
    {
        internal GroupContainer(Database database, Transaction transaction, ObjectId containerID)
          : base(database, transaction, containerID, g => g.Name, () => nameof(Group.Name))
        {
        }

        /// <summary>
        /// Creates a new Group with the specified name and adds the Entities to it.
        /// </summary>
        /// <param name="name">The unique name of the element.</param>
        /// <param name="entities">The entities to be added to the Group.</param>
        public Group Create(string name, IEnumerable<Entity> entities)
        {
            Require.NotDisposed(database.IsDisposed, nameof(AcadDatabase));
            Require.TransactionNotDisposed(transaction.IsDisposed);
            Require.IsValidSymbolName(name, nameof(name));
            Require.NameDoesNotExist<Group>(Contains(name), name);

            var group = AddInternal(new Group(), name);
            group.Append(entities);

            return group;
        }
        /// <summary>
        /// Creates a new Group with the specified name and adds the object ids to it.
        /// <example>        
        /// <code>
        /// Group group5 = db.Groups.Create("Group5", ids);        
        /// </code>
        /// results in a group5 with ids.
        /// </example>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="ids"></param>
        /// <returns> Group </returns>
        public Group Create(string name, IEnumerable<ObjectId> ids)
        {
            Require.NotDisposed(database.IsDisposed, nameof(AcadDatabase));
            Require.TransactionNotDisposed(transaction.IsDisposed);
            Require.IsValidSymbolName(name, nameof(name));
            Require.NameDoesNotExist<Group>(Contains(name), name);

            var group = AddInternal(new Group(), name);
            group.Append(ids);

            return group;
        }
    }
}
