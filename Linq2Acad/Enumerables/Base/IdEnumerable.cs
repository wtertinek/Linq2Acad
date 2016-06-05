using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2Acad
{
  public class LazyIdEnumerable<T> : IdEnumerableBase<T>
  {
    private Func<object, T> getID;

    public LazyIdEnumerable(IEnumerable<object> ids, Func<object, T> getID)
    {
      IDs = ids;
      this.getID = getID;
    }

    public override IEnumerator<T> GetEnumerator()
    {
      return IDs.Select(id => getID(id))
                .GetEnumerator();
    }

    internal IEnumerable<object> IDs { get; private set; }

    public override IdEnumerableBase<T> Concat(IEnumerable<T> second)
    {
      if (second is LazyIdEnumerable<T>)
      {
        return new LazyIdEnumerable<T>(IDs.Concat((second as LazyIdEnumerable<T>).IDs), getID);
      }
      else
      {
        return new IdEnumerable<T>(IDs.Select(id => getID(id))
                                      .Concat(second));
      }
    }

    public override bool Contains(T value)
    {
      return IDs.Select(id => getID(id))
                .Contains(value);
    }

    public override int Count()
    {
      return IDs.Count();
    }

    public override IdEnumerableBase<T> Distinct()
    {
      return new IdEnumerable<T>(IDs.Distinct()
                                         .Select(id => getID(id)));
    }

    public override T ElementAt(int index)
    {
      return getID(IDs.ElementAt(index));
    }

    public override T ElementAtOrDefault(int index)
    {
      var id = IDs.ElementAtOrDefault(index);

      if (id != null)
      {
        return getID(id);
      }
      else
      {
        return default(T);
      }
    }

    public override IdEnumerableBase<T> Except(IEnumerable<T> second)
    {
      if (second is LazyIdEnumerable<T>)
      {
        return new LazyIdEnumerable<T>(IDs.Except((second as LazyIdEnumerable<T>).IDs), getID);
      }
      else
      {
        return new IdEnumerable<T>(IDs.Select(id => getID(id))
                                      .Except(second));
      }
    }

    public override IdEnumerableBase<T> Intersect(IEnumerable<T> second)
    {
      if (second is LazyIdEnumerable<T>)
      {
        return new LazyIdEnumerable<T>(IDs.Intersect((second as LazyIdEnumerable<T>).IDs), getID);
      }
      else
      {
        var set = new HashSet<T>(IDs.Select(id => getID(id)));
        return new IdEnumerable<T>(second.Where(id => set.Remove(id)));
      }
    }

    public override T Last()
    {
      return getID(IDs.Last());
    }

    public override T LastOrDefault()
    {
      var id = IDs.LastOrDefault();

      if (id != null)
      {
        return getID(id);
      }
      else
      {
        return default(T);
      }
    }

    public override long LongCount()
    {
      return IDs.LongCount();
    }

    public override IdEnumerableBase<T> Reverse()
    {
      return new LazyIdEnumerable<T>(IDs.Reverse(), getID);
    }

    public override bool SequenceEqual(IEnumerable<T> second)
    {
      return IDs.Select(id => getID(id))
                .SequenceEqual(second);
    }

    public override IdEnumerableBase<T> Skip(int count)
    {
      return new LazyIdEnumerable<T>(IDs.Skip(count), getID);
    }

    public override IdEnumerableBase<T> Take(int count)
    {
      return new LazyIdEnumerable<T>(IDs.Take(count), getID);
    }

    public override IdEnumerableBase<T> Union(IEnumerable<T> second)
    {
      return new IdEnumerable<T>(UnionIterator(second));
    }

    private IEnumerable<T> UnionIterator(IEnumerable<T> second)
    {
      var set = new HashSet<T>();

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

  #region Base class IdEnumerableBase

  public abstract class IdEnumerableBase<T> : IEnumerable<T>
  {
    public abstract IEnumerator<T> GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }

    public abstract IdEnumerableBase<T> Concat(IEnumerable<T> second);

    public abstract bool Contains(T value);

    public abstract int Count();

    public abstract IdEnumerableBase<T> Distinct();

    public abstract T ElementAt(int index);

    public abstract T ElementAtOrDefault(int index);

    public abstract IdEnumerableBase<T> Except(IEnumerable<T> second);

    public abstract IdEnumerableBase<T> Intersect(IEnumerable<T> second);

    public abstract T Last();

    public abstract T LastOrDefault();

    public abstract long LongCount();

    public abstract IdEnumerableBase<T> Reverse();

    public abstract bool SequenceEqual(IEnumerable<T> second);

    public abstract IdEnumerableBase<T> Skip(int count);

    public abstract IdEnumerableBase<T> Take(int count);

    public abstract IdEnumerableBase<T> Union(IEnumerable<T> second);
  }

  #endregion

  #region IdEnumerable

  class IdEnumerable<T> : IdEnumerableBase<T>
  {
    private IEnumerable<T> ids;

    public IdEnumerable(IEnumerable<T> ids)
    {
      this.ids = ids;
    }

    public override IEnumerator<T> GetEnumerator()
    {
      return ids.GetEnumerator();
    }

    public override IdEnumerableBase<T> Concat(IEnumerable<T> second)
    {
      return new IdEnumerable<T>(ids.Concat(second));
    }

    public override bool Contains(T value)
    {
      return ids.Contains(value);
    }

    public override int Count()
    {
      return ids.Count();
    }

    public override IdEnumerableBase<T> Distinct()
    {
      return new IdEnumerable<T>(ids.Distinct());
    }

    public override T ElementAt(int index)
    {
      return ids.ElementAt(index);
    }

    public override T ElementAtOrDefault(int index)
    {
      return ids.ElementAtOrDefault(index);
    }

    public override IdEnumerableBase<T> Except(IEnumerable<T> second)
    {
      return new IdEnumerable<T>(ids.Except(second));
    }

    public override IdEnumerableBase<T> Intersect(IEnumerable<T> second)
    {
      return new IdEnumerable<T>(ids.Intersect(second));
    }

    public override T Last()
    {
      return ids.Last();
    }

    public override T LastOrDefault()
    {
      return ids.LastOrDefault();
    }

    public override long LongCount()
    {
      return ids.LongCount();
    }

    public override IdEnumerableBase<T> Reverse()
    {
      return new IdEnumerable<T>(ids.Reverse());
    }

    public override bool SequenceEqual(IEnumerable<T> second)
    {
      return ids.SequenceEqual(second);
    }

    public override IdEnumerableBase<T> Skip(int count)
    {
      return new IdEnumerable<T>(ids.Skip(count));
    }

    public override IdEnumerableBase<T> Take(int count)
    {
      return new IdEnumerable<T>(ids.Take(count));
    }

    public override IdEnumerableBase<T> Union(IEnumerable<T> second)
    {
      return new IdEnumerable<T>(ids.Union(second));
    }
  }

  #endregion
}
