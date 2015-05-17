using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2Acad
{
  abstract class EnumerableBase<T> : IEnumerable<T>, IAcadEnumerable where T : DBObject
  {
    protected Lazy<Transaction> transaction;

    protected EnumerableBase(Lazy<Transaction> transaction, ObjectId containerID)
    {
      this.transaction = transaction;
      ContainerID = containerID;
    }

    public ObjectId ContainerID { get; private set; }

    public abstract int Count { get; }

    public abstract IEnumerator<T> GetEnumerator();

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }
  }
}
