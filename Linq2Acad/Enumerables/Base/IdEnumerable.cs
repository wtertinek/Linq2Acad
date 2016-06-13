using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2Acad
{
  #region Base class IdEnumerable

  public abstract class IdEnumerable<T> : IEnumerable<T>
  {
    protected IdEnumerable()
    {
    }

    public abstract IEnumerator<T> GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }

    public abstract IdEnumerable<T> Concat(IEnumerable<T> second);

    public abstract bool Contains(T value);

    public abstract int Count();

    public abstract IdEnumerable<T> Distinct();

    public abstract T ElementAt(int index);

    public abstract T ElementAtOrDefault(int index);

    public abstract IdEnumerable<T> Except(IEnumerable<T> second);

    public abstract IdEnumerable<T> Intersect(IEnumerable<T> second);

    public abstract T Last();

    public abstract T LastOrDefault();

    public abstract long LongCount();

    public abstract IdEnumerable<T> Reverse();

    public abstract bool SequenceEqual(IEnumerable<T> second);

    public abstract IdEnumerable<T> Skip(int count);

    public abstract IdEnumerable<T> Take(int count);

    public abstract IdEnumerable<T> Union(IEnumerable<T> second);
  }

  #endregion

  #region Class LazyIdEnumerable

  public class LazyIdEnumerable<T> : IdEnumerable<T>
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

    public override IdEnumerable<T> Concat(IEnumerable<T> second)
    {
      if (second is LazyIdEnumerable<T>)
      {
        return new LazyIdEnumerable<T>(IDs.Concat((second as LazyIdEnumerable<T>).IDs), getID);
      }
      else
      {
        return new MaterializedIdEnumerable<T>(IDs.Select(id => getID(id))
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

    public override IdEnumerable<T> Distinct()
    {
      return new MaterializedIdEnumerable<T>(IDs.Distinct()
                                                .Select(id => getID(id)));
    }

    public override T ElementAt(int index)
    {
      return getID(IDs.ElementAt(index));
    }

    public override T ElementAtOrDefault(int index)
    {
      var id = IDs.ElementAtOrDefault(index);

      if (Object.Equals(id, default(T)))
      {
        return default(T);
      }
      else
      {
        return getID(id);
      }
    }

    public override IdEnumerable<T> Except(IEnumerable<T> second)
    {
      if (second is LazyIdEnumerable<T>)
      {
        return new LazyIdEnumerable<T>(IDs.Except((second as LazyIdEnumerable<T>).IDs), getID);
      }
      else
      {
        return new MaterializedIdEnumerable<T>(IDs.Select(id => getID(id))
                                                  .Except(second));
      }
    }

    public override IdEnumerable<T> Intersect(IEnumerable<T> second)
    {
      if (second is LazyIdEnumerable<T>)
      {
        return new LazyIdEnumerable<T>(IDs.Intersect((second as LazyIdEnumerable<T>).IDs), getID);
      }
      else
      {
        var set = new HashSet<T>(IDs.Select(id => getID(id)));
        return new MaterializedIdEnumerable<T>(second.Where(id => set.Remove(id)));
      }
    }

    public override T Last()
    {
      return getID(IDs.Last());
    }

    public override T LastOrDefault()
    {
      var id = IDs.LastOrDefault();

      if (Object.Equals(id, default(T)))
      {
        return default(T);
      }
      else
      {
        return getID(id);
      }
    }

    public override long LongCount()
    {
      return IDs.LongCount();
    }

    public override IdEnumerable<T> Reverse()
    {
      return new LazyIdEnumerable<T>(IDs.Reverse(), getID);
    }

    public override bool SequenceEqual(IEnumerable<T> second)
    {
      return IDs.Select(id => getID(id))
                .SequenceEqual(second);
    }

    public override IdEnumerable<T> Skip(int count)
    {
      return new LazyIdEnumerable<T>(IDs.Skip(count), getID);
    }

    public override IdEnumerable<T> Take(int count)
    {
      return new LazyIdEnumerable<T>(IDs.Take(count), getID);
    }

    public override IdEnumerable<T> Union(IEnumerable<T> second)
    {
      return new MaterializedIdEnumerable<T>(UnionIterator(second));
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

  #endregion

  #region Class MaterializedIdEnumerable

  class MaterializedIdEnumerable<T> : IdEnumerable<T>
  {
    private IEnumerable<T> ids;

    public MaterializedIdEnumerable(IEnumerable<T> ids)
    {
      this.ids = ids;
    }

    public override IEnumerator<T> GetEnumerator()
    {
      return ids.GetEnumerator();
    }

    public override IdEnumerable<T> Concat(IEnumerable<T> second)
    {
      return new MaterializedIdEnumerable<T>(ids.Concat(second));
    }

    public override bool Contains(T value)
    {
      return ids.Contains(value);
    }

    public override int Count()
    {
      return ids.Count();
    }

    public override IdEnumerable<T> Distinct()
    {
      return new MaterializedIdEnumerable<T>(ids.Distinct());
    }

    public override T ElementAt(int index)
    {
      return ids.ElementAt(index);
    }

    public override T ElementAtOrDefault(int index)
    {
      return ids.ElementAtOrDefault(index);
    }

    public override IdEnumerable<T> Except(IEnumerable<T> second)
    {
      return new MaterializedIdEnumerable<T>(ids.Except(second));
    }

    public override IdEnumerable<T> Intersect(IEnumerable<T> second)
    {
      return new MaterializedIdEnumerable<T>(ids.Intersect(second));
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

    public override IdEnumerable<T> Reverse()
    {
      return new MaterializedIdEnumerable<T>(ids.Reverse());
    }

    public override bool SequenceEqual(IEnumerable<T> second)
    {
      return ids.SequenceEqual(second);
    }

    public override IdEnumerable<T> Skip(int count)
    {
      return new MaterializedIdEnumerable<T>(ids.Skip(count));
    }

    public override IdEnumerable<T> Take(int count)
    {
      return new MaterializedIdEnumerable<T>(ids.Take(count));
    }

    public override IdEnumerable<T> Union(IEnumerable<T> second)
    {
      return new MaterializedIdEnumerable<T>(ids.Union(second));
    }
  }

  #endregion
}
