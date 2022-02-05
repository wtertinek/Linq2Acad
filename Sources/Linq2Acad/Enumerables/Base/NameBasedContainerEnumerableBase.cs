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
    protected NameBasedContainerEnumerableBase(Database database, Transaction transaction,
                                               ObjectId containerID, Func<object, ObjectId> getID)
      : base(database, transaction, containerID, getID)
    {
    }

    protected NameBasedContainerEnumerableBase(Database database, Transaction transaction,
                                               ObjectId containerID, Func<object, ObjectId> getID,
                                               Func<IEnumerable<ObjectId>, IEnumerable<ObjectId>> filter)
      : base(database, transaction, containerID, getID, filter)
    {
    }

    public bool Contains(string name)
      => ContainsInternal(name);

    protected abstract bool ContainsInternal(string name);
  }
}
