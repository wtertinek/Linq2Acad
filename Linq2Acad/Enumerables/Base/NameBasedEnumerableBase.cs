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

    public abstract T Element(string name, bool forWrite);

    public abstract T ElementOrDefault(string name);

    public abstract T ElementOrDefault(string name, bool forWrite);

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
