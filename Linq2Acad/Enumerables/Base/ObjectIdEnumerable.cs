using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;

namespace Linq2Acad
{
  class ObjectIdEnumerable : IEnumerable<ObjectId>
  {
    private Func<object, ObjectId> getID;

    public ObjectIdEnumerable(IEnumerable<object> ids, Func<object, ObjectId> getID)
    {
      IDs = ids;
      this.getID = getID;
    }

    public ObjectIdEnumerable(Transaction transaction, ObjectId containerID, Func<object, ObjectId> getID)
      : this(GetIDs(transaction, containerID), getID)
    {
    }

    private static IEnumerable<object> GetIDs(Transaction transaction, ObjectId containerID)
    {
      var enumerable = (IEnumerable)transaction.GetObject(containerID, OpenMode.ForRead);

      foreach (var item in enumerable)
      {
        yield return item;
      }
    }

    public IEnumerator<ObjectId> GetEnumerator()
    {
      return IDs.Select(id => getID(id))
                .GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }

    internal IEnumerable<object> IDs { get; private set; }

    public IEnumerable<ObjectId> Concat(IEnumerable<ObjectId> second)
    {
      if (second is ObjectIdEnumerable)
      {
        return new ObjectIdEnumerable(IDs.Concat((second as ObjectIdEnumerable).IDs), getID);
      }
      else
      {
        return ConcatIterater(second);
      }
    }

    private IEnumerable<ObjectId> ConcatIterater(IEnumerable<ObjectId> second)
    {
      foreach (var id in IDs.Select(id => getID(id)))
      {
        yield return id;
      }

      foreach (var element in second)
      {
        yield return element;
      }
    }

    public bool Contains(ObjectId value)
    {
      return IDs.Select(id => getID(id))
                .Contains(value);
    }

    public int Count()
    {
      return IDs.Count();
    }

    public IEnumerable<ObjectId> Distinct()
    {
      return IDs.Select(id => getID(id))
                .Distinct();
    }

    public ObjectId ElementAt(int index)
    {
      return getID(IDs.ElementAt(index));
    }

    public ObjectId ElementAtOrDefault(int index)
    {
      var id = IDs.ElementAtOrDefault(index);

      if (id != null)
      {
        return getID(id);
      }
      else
      {
        return default(ObjectId);
      }
    }

    public IEnumerable<ObjectId> Except(IEnumerable<ObjectId> second)
    {
      return IDs.Select(id => getID(id))
                .Except(second);
    }

    public IEnumerable<ObjectId> Intersect(IEnumerable<ObjectId> second)
    {
      var set = new HashSet<ObjectId>(IDs.Select(id => getID(id)));
      return second.Where(id => set.Remove(id));
    }

    public ObjectId Last()
    {
      return getID(IDs.Last());
    }

    public ObjectId LastOrDefault()
    {
      var id = IDs.LastOrDefault();

      if (id != null)
      {
        return getID(id);
      }
      else
      {
        return default(ObjectId);
      }
    }

    public long LongCount()
    {
      return IDs.LongCount();
    }

    public IEnumerable<ObjectId> Reverse()
    {
      return new ObjectIdEnumerable(IDs.Reverse(), getID);
    }

    public bool SequenceEqual(IEnumerable<ObjectId> second)
    {
      return IDs.Select(id => getID(id))
                .SequenceEqual(second);
    }

    public IEnumerable<ObjectId> Skip(int count)
    {
      return new ObjectIdEnumerable(IDs.Skip(count), getID);
    }

    public IEnumerable<ObjectId> Take(int count)
    {
      return new ObjectIdEnumerable(IDs.Take(count), getID);
    }

    public IEnumerable<ObjectId> Union(IEnumerable<ObjectId> second)
    {
      var set = new HashSet<ObjectId>();

      foreach (var id in IDs.Select(id => getID(id)))
      {
        set.Add(id);
        yield return id;
      }

      foreach (var id in second.Where(i => set.Add(i)))
      {
        yield return id;
      }
    }
  }
}
