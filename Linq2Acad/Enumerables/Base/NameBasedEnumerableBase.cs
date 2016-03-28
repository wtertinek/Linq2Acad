using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;

namespace Linq2Acad
{
  public abstract class NameBasedEnumerableBase<T> : ContainerEnumerableBase<T> where T : DBObject
  {
    protected NameBasedEnumerableBase(Database database, Transaction transaction,
                                      ObjectId containerID, Func<object, ObjectId> getID)
      : base(database, transaction, containerID, getID)
    {
    }

    public abstract bool Contains(string name);

    public abstract T Element(string name);
  }
}
