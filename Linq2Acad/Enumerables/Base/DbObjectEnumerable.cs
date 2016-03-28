using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Autodesk.AutoCAD.DatabaseServices;

namespace Linq2Acad
{
  public class DbObjectEnumerable<T> : IEnumerable<T> where T : DBObject
  {
    private Transaction transaction;

    internal DbObjectEnumerable(Transaction transaction, IEnumerable<ObjectId> ids)
    {
      this.transaction = transaction;
      IDs = ids;
    }

    internal IEnumerable<ObjectId> IDs { get; private set; }

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public IEnumerator<T> GetEnumerator()
    {
      return IDs.Select(id => (T)transaction.GetObject(id, OpenMode.ForRead))
                .GetEnumerator();
    }

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public IEnumerable<T> Concat(IEnumerable<T> second)
    {
      if (second is DbObjectEnumerable<T>)
      {
        return new DbObjectEnumerable<T>(transaction, IDs.Concat((second as DbObjectEnumerable<T>).IDs));
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

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public virtual bool Contains(T value)
    {
      return IDs.Contains(value.ObjectId);
    }

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public virtual int Count()
    {
      return IDs.Count();
    }

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public IEnumerable<T> Distinct()
    {
      return new DbObjectEnumerable<T>(transaction, IDs.Distinct());
    }

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public T ElementAt(int index)
    {
      return (T)transaction.GetObject(IDs.ElementAt(index), OpenMode.ForRead);
    }

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
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

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public IEnumerable<T> Except(IEnumerable<T> second)
    {
      if (second is DbObjectEnumerable<T>)
      {
        return new DbObjectEnumerable<T>(transaction, IDs.Except((second as DbObjectEnumerable<T>).IDs));
      }
      else
      {
        return new DbObjectEnumerable<T>(transaction, IDs.Except(second.Select(e => e.ObjectId)));
      }
    }

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public IEnumerable<T> Intersect(IEnumerable<T> second)
    {
      if (second is DbObjectEnumerable<T>)
      {
        return new DbObjectEnumerable<T>(transaction, IDs.Intersect((second as DbObjectEnumerable<T>).IDs));
      }
      else
      {
        var set = new HashSet<ObjectId>(IDs);
        return second.Where(e => set.Remove(e.ObjectId));
      }
    }

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public T Last()
    {
      return (T)transaction.GetObject(IDs.Last(), OpenMode.ForRead);
    }

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
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

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public long LongCount()
    {
      return IDs.LongCount();
    }

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public IEnumerable<TResult> OfType<TResult>() where TResult : T
    {
      // TODO: What is TResult's RXClass if it is a derived type?
      var rxType = Autodesk.AutoCAD.Runtime.RXClass.GetClass(typeof(TResult));
      return new DbObjectEnumerable<TResult>(transaction, IDs.Where(id => id.ObjectClass.IsDerivedFrom(rxType)));
    }

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public IEnumerable<T> Reverse()
    {
      return new DbObjectEnumerable<T>(transaction, IDs.Reverse());
    }

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public bool SequenceEqual(IEnumerable<T> second)
    {
      if (second is DbObjectEnumerable<T>)
      {
        return IDs.SequenceEqual((second as DbObjectEnumerable<T>).IDs);
      }
      else
      {
        return IDs.SequenceEqual(second.Select(e => e.ObjectId));
      }
    }

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public IEnumerable<T> Skip(int count)
    {
      return new DbObjectEnumerable<T>(transaction, IDs.Skip(count));
    }

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public IEnumerable<T> Take(int count)
    {
      return new DbObjectEnumerable<T>(transaction, IDs.Take(count));
    }

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public IEnumerable<T> Union(IEnumerable<T> second)
    {
      if (second is DbObjectEnumerable<T>)
      {
        return new DbObjectEnumerable<T>(transaction, IDs.Union((second as DbObjectEnumerable<T>).IDs));
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
  }
}
