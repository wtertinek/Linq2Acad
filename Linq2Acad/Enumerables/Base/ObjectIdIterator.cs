using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Autodesk.AutoCAD.DatabaseServices;

namespace Linq2Acad
{
  class ObjectIdIterator<T> : IEnumerable<T> where T : DBObject
  {
    private Transaction transaction;

    public ObjectIdIterator(Transaction transaction, IEnumerable<ObjectId> ids)
    {
      this.transaction = transaction;
      IDs = ids;
    }

    public IEnumerable<ObjectId> IDs { get; private set; }

    public IEnumerable<T> Concat(IEnumerable<T> second)
    {
      if (second is ObjectIdIterator<T>)
      {
        return new ObjectIdIterator<T>(transaction, IDs.Concat((second as ObjectIdIterator<T>).IDs));
      }
      else
      {
        return ConcatIterater(second);
      }
    }

    private IEnumerable<T> ConcatIterater(IEnumerable<T> second)
    {
      foreach (var id in IDs)
      {
        yield return (T)transaction.GetObject(id, OpenMode.ForRead);
      }

      foreach (var element in second)
      {
        yield return element;
      }
    }

    public bool Contains(T value)
    {
      return IDs.Contains(value.ObjectId);
    }

    public int Count()
    {
      return IDs.Count();
    }

    public IEnumerable<T> Distinct()
    {
      return new ObjectIdIterator<T>(transaction, IDs.Distinct());
    }

    public T ElementAt(int index)
    {
      return (T)transaction.GetObject(IDs.ElementAt(index), OpenMode.ForRead);
    }

    public T ElementAtOrDefault(int index)
    {
      var id = IDs.ElementAtOrDefault(index);

      if (id.IsValid)
      {
        return (T)transaction.GetObject(id, OpenMode.ForRead);
      }
      else
      {
        return default(T);
      }
    }

    public IEnumerable<T> Except(IEnumerable<T> second)
    {
      if (second is ObjectIdIterator<T>)
      {
        return new ObjectIdIterator<T>(transaction, IDs.Except((second as ObjectIdIterator<T>).IDs));
      }
      else
      {
        return new ObjectIdIterator<T>(transaction, IDs.Except(second.Select(e => e.ObjectId)));
      }
    }

    public IEnumerable<T> Intersect(IEnumerable<T> second)
    {
      if (second is ObjectIdIterator<T>)
      {
        return new ObjectIdIterator<T>(transaction, IDs.Intersect((second as ObjectIdIterator<T>).IDs));
      }
      else
      {
        var set = new HashSet<ObjectId>(IDs);
        return second.Where(e => set.Remove(e.ObjectId));
      }
    }

    public T Last()
    {
      return (T)transaction.GetObject(IDs.Last(), OpenMode.ForRead);
    }

    public T LastOrDefault()
    {
      var id = IDs.LastOrDefault();

      if (id.IsValid)
      {
        return (T)transaction.GetObject(id, OpenMode.ForRead);
      }
      else
      {
        return default(T);
      }
    }

    public long LongCount()
    {
      return IDs.LongCount();
    }

    public IEnumerable<TResult> OfType<TResult>() where TResult : T
    {
      // TODO: What is TResult's RXClass if it is a derived type?
      var rxType = Autodesk.AutoCAD.Runtime.RXClass.GetClass(typeof(TResult));
      return new ObjectIdIterator<TResult>(transaction, IDs.Where(id => id.ObjectClass.IsDerivedFrom(rxType)));
    }

    public IEnumerable<T> Reverse()
    {
      return new ObjectIdIterator<T>(transaction, IDs.Reverse());
    }

    public bool SequenceEqual(IEnumerable<T> second)
    {
      if (second is ObjectIdIterator<T>)
      {
        return IDs.SequenceEqual((second as ObjectIdIterator<T>).IDs);
      }
      else
      {
        return IDs.SequenceEqual(second.Select(e => e.ObjectId));
      }
    }

    public IEnumerable<T> Skip(int count)
    {
      return new ObjectIdIterator<T>(transaction, IDs.Skip(count));
    }

    public IEnumerable<T> Take(int count)
    {
      return new ObjectIdIterator<T>(transaction, IDs.Take(count));
    }

    public IEnumerable<T> Union(IEnumerable<T> second)
    {
      if (second is ObjectIdIterator<T>)
      {
        return new ObjectIdIterator<T>(transaction, IDs.Union((second as ObjectIdIterator<T>).IDs));
      }
      else
      {
        return UnionIterator(second);
      }
    }

    private IEnumerable<T> UnionIterator(IEnumerable<T> second)
    {
      var set = new HashSet<ObjectId>();

      foreach (var element in second)
      {
        set.Add(element.ObjectId);
        yield return element;
      }

      foreach (var id in IDs.Where(id => set.Add(id)))
      {
        yield return (T)transaction.GetObject(id, OpenMode.ForRead);
      }
    }

    public IEnumerator<T> GetEnumerator()
    {
      return IDs.Select(id => (T)transaction.GetObject(id, OpenMode.ForRead)).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }
  }
}
