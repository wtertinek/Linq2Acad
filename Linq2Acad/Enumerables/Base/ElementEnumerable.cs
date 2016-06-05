using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Autodesk.AutoCAD.DatabaseServices;

namespace Linq2Acad
{
  public class LazyElementEnumerable<T, TId> : ElementEnumerableBase<T, TId>
  {
    private Func<TId, T> getElement;
    private Func<T, TId> getID;

    public LazyElementEnumerable(IdEnumerableBase<TId> ids, Func<TId, T> getElement, Func<T, TId> getID)
    {
      this.getElement = getElement;
      this.getID = getID;
      IDs = ids;
    }

    internal IdEnumerableBase<TId> IDs { get; private set; }

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public override sealed IEnumerator<T> GetEnumerator()
    {
      return IDs.Select(id => getElement(id))
                .GetEnumerator();
    }

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public override sealed ElementEnumerableBase<T, TId> Concat(IEnumerable<T> second)
    {
      if (second is LazyElementEnumerable<T, TId>)
      {
        return new LazyElementEnumerable<T, TId>(IDs.Concat((second as LazyElementEnumerable<T, TId>).IDs), getElement, getID);
      }
      else
      {
        return new ElementEnumerable<T, TId>(IDs.Select(id => getElement(id))
                                                .Concat(second));
      }
    }

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public override bool Contains(T value)
    {
      return IDs.Contains(getID(value));
    }

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public override int Count()
    {
      return IDs.Count();
    }

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public override sealed ElementEnumerableBase<T, TId> Distinct()
    {
      return new LazyElementEnumerable<T, TId>(IDs.Distinct(), getElement, getID);
    }

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public override sealed T ElementAt(int index)
    {
      return getElement(IDs.ElementAt(index));
    }

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public override sealed T ElementAtOrDefault(int index)
    {
      var id = IDs.ElementAtOrDefault(index);

      if (Object.Equals(id, default(TId)))
      {
        return default(T);
      }
      else
      {
        return getElement(id);
      }
    }

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public override sealed ElementEnumerableBase<T, TId> Except(IEnumerable<T> second)
    {
      if (second is LazyElementEnumerable<T, TId>)
      {
        return new LazyElementEnumerable<T, TId>(IDs.Except((second as LazyElementEnumerable<T, TId>).IDs), getElement, getID);
      }
      else
      {
        return new ElementEnumerable<T, TId>(IDs.Select(id => getElement(id))
                                                .Except(second));
      }
    }

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public override sealed ElementEnumerableBase<T, TId> Intersect(IEnumerable<T> second)
    {
      if (second is LazyElementEnumerable<T, TId>)
      {
        return new LazyElementEnumerable<T, TId>(IDs.Intersect((second as LazyElementEnumerable<T, TId>).IDs), getElement, getID);
      }
      else
      {
        var set = new HashSet<TId>(IDs);
        return new ElementEnumerable<T, TId>(second.Where(e => set.Remove(getID(e))));
      }
    }

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public override sealed T Last()
    {
      return getElement(IDs.Last());
    }

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public override sealed T LastOrDefault()
    {
      var id = IDs.LastOrDefault();

      if (Object.Equals(id, default(TId)))
      {
        return default(T);
      }
      else
      {
        return getElement(id);
      }
    }

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public override sealed long LongCount()
    {
      return IDs.LongCount();
    }

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public ElementEnumerableBase<TResult, TId> OfType<TResult>() where TResult : T
    {
      // TODO: Implement conversion
      throw new NotImplementedException();
    }

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public override sealed ElementEnumerableBase<T, TId> Reverse()
    {
      return new LazyElementEnumerable<T, TId>(IDs.Reverse(), getElement, getID);
    }

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public override sealed bool SequenceEqual(IEnumerable<T> second)
    {
      if (second is LazyElementEnumerable<T, TId>)
      {
        return IDs.SequenceEqual((second as LazyElementEnumerable<T, TId>).IDs);
      }
      else
      {
        return IDs.SequenceEqual(second.Select(e => getID(e)));
      }
    }

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public override sealed ElementEnumerableBase<T, TId> Skip(int count)
    {
      return new LazyElementEnumerable<T, TId>(IDs.Skip(count), getElement, getID);
    }

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public override sealed ElementEnumerableBase<T, TId> Take(int count)
    {
      return new LazyElementEnumerable<T, TId>(IDs.Take(count), getElement, getID);
    }

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public override sealed ElementEnumerableBase<T, TId> Union(IEnumerable<T> second)
    {
      if (second is LazyElementEnumerable<T, TId>)
      {
        return new LazyElementEnumerable<T, TId>(IDs.Union((second as LazyElementEnumerable<T, TId>).IDs), getElement, getID);
      }
      else
      {
        return new ElementEnumerable<T, TId>(UnionIterator(second));
      }
    }

    private IEnumerable<T> UnionIterator(IEnumerable<T> second)
    {
      var set = new HashSet<TId>();

      foreach (var element in second)
      {
        set.Add(getID(element));
        yield return element;
      }

      foreach (var id in IDs.Where(id => set.Add(id)))
      {
        yield return getElement(id);
      }
    }
  }

  #region Base class ElementEnumerableBase

  public abstract class ElementEnumerableBase<T, TId> : IEnumerable<T>
  {
    protected ElementEnumerableBase()
    {
    }

    public abstract IEnumerator<T> GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }

    public abstract ElementEnumerableBase<T, TId> Concat(IEnumerable<T> second);

    public abstract bool Contains(T value);

    public abstract int Count();

    public abstract ElementEnumerableBase<T, TId> Distinct();

    public abstract T ElementAt(int index);

    public abstract T ElementAtOrDefault(int index);

    public abstract ElementEnumerableBase<T, TId> Except(IEnumerable<T> second);

    public abstract ElementEnumerableBase<T, TId> Intersect(IEnumerable<T> second);

    public abstract T Last();

    public abstract T LastOrDefault();

    public abstract long LongCount();

    public abstract ElementEnumerableBase<T, TId> Reverse();

    public abstract bool SequenceEqual(IEnumerable<T> second);

    public abstract ElementEnumerableBase<T, TId> Skip(int count);

    public abstract ElementEnumerableBase<T, TId> Take(int count);

    public abstract ElementEnumerableBase<T, TId> Union(IEnumerable<T> second);
  }

  #endregion

  #region ElementEnumerable

  class ElementEnumerable<T, TId> : ElementEnumerableBase<T, TId>
  {
    private IEnumerable<T> elements;

    public ElementEnumerable(IEnumerable<T> elements)
    {
      this.elements = elements;
    }

    public override sealed IEnumerator<T> GetEnumerator()
    {
      return elements.GetEnumerator();
    }

    public override sealed ElementEnumerableBase<T, TId> Concat(IEnumerable<T> second)
    {
      return new ElementEnumerable<T, TId>(elements.Concat(second));
    }

    public override sealed bool Contains(T value)
    {
      return elements.Contains(value);
    }

    public override sealed int Count()
    {
      return elements.Count();
    }

    public override sealed ElementEnumerableBase<T, TId> Distinct()
    {
      return new ElementEnumerable<T, TId>(elements.Distinct());
    }

    public override sealed T ElementAt(int index)
    {
      return elements.ElementAt(index);
    }

    public override sealed T ElementAtOrDefault(int index)
    {
      return elements.ElementAtOrDefault(index);
    }

    public override sealed ElementEnumerableBase<T, TId> Except(IEnumerable<T> second)
    {
      return new ElementEnumerable<T, TId>(elements.Except(second));
    }

    public override sealed ElementEnumerableBase<T, TId> Intersect(IEnumerable<T> second)
    {
      return new ElementEnumerable<T, TId>(elements.Intersect(second));
    }

    public override sealed T Last()
    {
      return elements.Last();
    }

    public override sealed T LastOrDefault()
    {
      return elements.LastOrDefault();
    }

    public override sealed long LongCount()
    {
      return elements.LongCount();
    }

    public override sealed ElementEnumerableBase<T, TId> Reverse()
    {
      return new ElementEnumerable<T, TId>(elements.Reverse());
    }

    public override sealed bool SequenceEqual(IEnumerable<T> second)
    {
      return elements.SequenceEqual(second);
    }

    public override sealed ElementEnumerableBase<T, TId> Skip(int count)
    {
      return new ElementEnumerable<T, TId>(elements.Skip(count));
    }

    public override sealed ElementEnumerableBase<T, TId> Take(int count)
    {
      return new ElementEnumerable<T, TId>(elements.Take(count));
    }

    public override sealed ElementEnumerableBase<T, TId> Union(IEnumerable<T> second)
    {
      return new ElementEnumerable<T, TId>(elements.Union(second));
    }
  }

  #endregion
}
