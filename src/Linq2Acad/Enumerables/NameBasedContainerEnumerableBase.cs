using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;

namespace Linq2Acad
{
  public abstract class NameBasedContainerEnumerableBase<T> : ContainerEnumerableBase<T> where T : DBObject
  {
    private protected NameBasedContainerEnumerableBase(Database database, Transaction transaction,
                                                       ObjectId containerID, Func<object, ObjectId> getID)
      : base(database, transaction, containerID, getID)
    {
    }

    private protected NameBasedContainerEnumerableBase(Database database, Transaction transaction,
                                                       ObjectId containerID, Func<object, ObjectId> getID,
                                                       Func<IEnumerable<ObjectId>, IEnumerable<ObjectId>> filter)
      : base(database, transaction, containerID, getID, filter)
    {
    }

    /// <summary>
    /// Determines whether a sequence contains the element with the specified name.
    /// </summary>
    /// <param name="name">The name of the object.</param>
    /// <returns>true if the source sequence contains an element that has the specified name; otherwise, false.</returns>
    public bool Contains(string name)
    {
      Require.NotDisposed(database.IsDisposed, nameof(AcadDatabase));
      Require.TransactionNotDisposed(transaction.IsDisposed);

      return ContainsInternal(name);
    }

    protected abstract bool ContainsInternal(string name);
  }
}
