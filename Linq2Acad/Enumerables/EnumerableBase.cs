using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2Acad
{
  public abstract class EnumerableBase<T> : IEnumerable<T> where T : DBObject
  {
    protected Lazy<Transaction> transaction;

    protected EnumerableBase(Lazy<Transaction> transaction, ObjectId containerID)
    {
      this.transaction = transaction;
      ContainerID = containerID;
    }

    public ObjectId ContainerID { get; private set; }

    public IEnumerator<T> GetEnumerator()
    {
      var enumerable = (IEnumerable)transaction.Value.GetObject(ContainerID, OpenMode.ForRead);

      foreach (var item in enumerable)
      {
        yield return (T)transaction.Value.GetObject(GetObjectID(item), OpenMode.ForRead);
      }
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }

    protected abstract ObjectId GetObjectID(object iteratorItem);

    public abstract int Count();

    public IEnumerable<TResult> OfType<TResult>() where TResult : T
    {
      var container = (IEnumerable)transaction.Value.GetObject(ContainerID, OpenMode.ForRead);
      var idEnumerator = container.GetEnumerator();
      var filterType = "AcDb" + typeof(TResult).Name;

      while (idEnumerator.MoveNext())
      {
        var id = GetObjectID(idEnumerator.Current);

        // TODO: This is not enough
        if (id.ObjectClass.Name != filterType)
        {
          continue;
        }

        yield return (TResult)transaction.Value.GetObject(id, OpenMode.ForRead);
      }
    }

    public T Item(ObjectId id)
    {
      Helpers.CheckTransaction();
      return (T)L2ADatabase.Transaction.Value.GetObject(id, OpenMode.ForRead);
    }

    public IEnumerable<T> Items(IEnumerable<ObjectId> ids)
    {
      Helpers.CheckTransaction();

      var table = (IEnumerable)L2ADatabase.Transaction.Value.GetObject(ContainerID, OpenMode.ForRead);

      foreach (var id in ids)
      {
        yield return (T)L2ADatabase.Transaction.Value.GetObject(id, OpenMode.ForRead);
      }
    }
  }

  public abstract class NameBasedEnumerable<T> : EnumerableBase<T> where T : DBObject
  {
    protected NameBasedEnumerable(Lazy<Transaction> transaction, ObjectId containerID)
      : base(transaction, containerID)
    {
    }

    public abstract bool Contains(string name);
    
    public abstract bool Contains(ObjectId id);

    public abstract T Item(string name);

    public abstract IEnumerable<T> Items(IEnumerable<string> names);
  }
}
