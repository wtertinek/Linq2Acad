using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2Acad
{
  public abstract class NameBasedEnumerable<T> : EnumerableBase<T> where T : DBObject
  {
    protected NameBasedEnumerable(Database database, Transaction transaction, ObjectId containerID)
      : base(database, transaction, containerID)
    {
    }

    public abstract bool Contains(string name);

    public abstract bool Contains(ObjectId id);

    public abstract T Element(string name);
  }
}
