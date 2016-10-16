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
    {
      try
      {
        return ContainsInternal(name);
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e);
      }
    }

    protected abstract bool ContainsInternal(string name);

    protected abstract T CreateNew();

    protected virtual void SetName(T item, string name) { }

    protected abstract void AddRangeInternal(IEnumerable<T> items, IEnumerable<string> names);

    protected virtual T CreateInternal(string name)
    {
      var item = CreateNew();
      AddRangeInternal(new[] { item }, new[] { name });
      SetName(item, name);
      return item;
    }
  }
}
