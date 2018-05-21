using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace Linq2Acad
{
  #region Base class ElementEnumerable

  public abstract class ElementEnumerable<T, TId> : IEnumerable<T>
  {
    [DebuggerStepThrough]
    protected ElementEnumerable()
    {
    }

    public abstract IEnumerator<T> GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }

    public abstract ElementEnumerable<T, TId> Concat(T element);

    public abstract ElementEnumerable<T, TId> Concat(IEnumerable<T> second);

    public abstract bool Contains(T value);

    public abstract int Count();

    public abstract ElementEnumerable<T, TId> Distinct();

    public abstract T ElementAt(int index);

    public abstract T ElementAtOrDefault(int index);

    public abstract ElementEnumerable<T, TId> Except(T element);

    public abstract ElementEnumerable<T, TId> Except(IEnumerable<T> second);

    public abstract ElementEnumerable<T, TId> Intersect(T element);

    public abstract ElementEnumerable<T, TId> Intersect(IEnumerable<T> second);

    public abstract T Last();

    public abstract T LastOrDefault();

    public abstract long LongCount();

    public abstract ElementEnumerable<TResult, TId> OfType<TResult>() where TResult : T;

    public abstract ElementEnumerable<T, TId> Reverse();

    public abstract bool SequenceEqual(T element);

    public abstract bool SequenceEqual(IEnumerable<T> second);

    public abstract ElementEnumerable<T, TId> Skip(int count);

    public abstract ElementEnumerable<T, TId> Take(int count);

    public abstract ElementEnumerable<T, TId> Union(T element);

    public abstract ElementEnumerable<T, TId> Union(IEnumerable<T> second);
  }

  #endregion

  #region Class LazyElementEnumerable

  public class LazyElementEnumerable<T, TId, TConstraint> : ElementEnumerable<T, TId> where T : TConstraint
  {
    private IDataProvider<TId, TConstraint> dataProvider;

    public LazyElementEnumerable(IdEnumerable<TId> ids, IDataProvider<TId, TConstraint> dataProvider)
    {
      IDs = ids;
      this.dataProvider = dataProvider;
    }

    internal IdEnumerable<TId> IDs { get; private set; }
    
    public override sealed IEnumerator<T> GetEnumerator()
    {
      return IDs.Select(id => dataProvider.GetElement<T>(id))
                .GetEnumerator();
    }

    public override sealed ElementEnumerable<T, TId> Concat(T element)
    {
      return Concat(new[] { element });
    }

    public override sealed ElementEnumerable<T, TId> Concat(IEnumerable<T> second)
    {
      if (second is LazyElementEnumerable<T, TId, TConstraint>)
      {
        return new LazyElementEnumerable<T, TId, TConstraint>(IDs.Concat((second as LazyElementEnumerable<T, TId, TConstraint>).IDs), dataProvider);
      }
      else
      {
        return new MaterializedElementEnumerable<T, TId>(IDs.Select(id => dataProvider.GetElement<T>(id))
                                                            .Concat(second));
      }
    }

    public override bool Contains(T value)
    {
      return IDs.Contains(dataProvider.GetId(value));
    }

    public override int Count()
    {
      return IDs.Count();
    }

    public override sealed ElementEnumerable<T, TId> Distinct()
    {
      return new LazyElementEnumerable<T, TId, TConstraint>(new MaterializedIdEnumerable<TId>(IDs.Distinct()), dataProvider);
    }

    public override sealed T ElementAt(int index)
    {
      return dataProvider.GetElement<T>(IDs.ElementAt(index));
    }

    public override sealed T ElementAtOrDefault(int index)
    {
      var id = IDs.ElementAtOrDefault(index);

      if (Object.Equals(id, default(TId)))
      {
        return default(T);
      }
      else
      {
        return dataProvider.GetElement<T>(id);
      }
    }

    public override sealed ElementEnumerable<T, TId> Except(T element)
    {
      return Except(new[] { element });
    }

    public override sealed ElementEnumerable<T, TId> Except(IEnumerable<T> second)
    {
      if (second is LazyElementEnumerable<T, TId, TConstraint>)
      {
        return new LazyElementEnumerable<T, TId, TConstraint>(new MaterializedIdEnumerable<TId>(IDs.Except((second as LazyElementEnumerable<T, TId, TConstraint>).IDs)), dataProvider);
      }
      else
      {
        return new MaterializedElementEnumerable<T, TId>(IDs.Select(id => dataProvider.GetElement<T>(id))
                                                            .Except(second));
      }
    }

    public override sealed ElementEnumerable<T, TId> Intersect(T element)
    {
      return Intersect(new[] { element });
    }

    public override sealed ElementEnumerable<T, TId> Intersect(IEnumerable<T> second)
    {
      if (second is LazyElementEnumerable<T, TId, TConstraint>)
      {
        return new LazyElementEnumerable<T, TId, TConstraint>(new MaterializedIdEnumerable<TId>(IDs.Intersect((second as LazyElementEnumerable<T, TId, TConstraint>).IDs)), dataProvider);
      }
      else
      {
        var set = new HashSet<TId>(IDs);
        return new MaterializedElementEnumerable<T, TId>(second.Where(e => set.Remove(dataProvider.GetId(e))));
      }
    }

    public override sealed T Last()
    {
      return dataProvider.GetElement<T>(IDs.Last());
    }

    public override sealed T LastOrDefault()
    {
      var id = IDs.LastOrDefault();

      if (Object.Equals(id, default(TId)))
      {
        return default(T);
      }
      else
      {
        return dataProvider.GetElement<T>(id);
      }
    }

    public override sealed long LongCount()
    {
      return IDs.LongCount();
    }

    public override ElementEnumerable<TResult, TId> OfType<TResult>()
    {
      return new LazyElementEnumerable<TResult, TId, TConstraint>(new MaterializedIdEnumerable<TId>(dataProvider.Filter<TResult>(IDs)), dataProvider);
    }

    public override sealed ElementEnumerable<T, TId> Reverse()
    {
      return new LazyElementEnumerable<T, TId, TConstraint>(new MaterializedIdEnumerable<TId>(IDs.Reverse()), dataProvider);
    }

    public override sealed bool SequenceEqual(T element)
    {
      return SequenceEqual(new[] { element });
    }

    public override sealed bool SequenceEqual(IEnumerable<T> second)
    {
      if (second is LazyElementEnumerable<T, TId, TConstraint>)
      {
        return IDs.SequenceEqual((second as LazyElementEnumerable<T, TId, TConstraint>).IDs);
      }
      else
      {
        return IDs.SequenceEqual(second.Select(e => dataProvider.GetId(e)));
      }
    }

    public override sealed ElementEnumerable<T, TId> Skip(int count)
    {
      return new LazyElementEnumerable<T, TId, TConstraint>(new MaterializedIdEnumerable<TId>(IDs.Skip(count)), dataProvider);
    }

    public override sealed ElementEnumerable<T, TId> Take(int count)
    {
      return new LazyElementEnumerable<T, TId, TConstraint>(new MaterializedIdEnumerable<TId>(IDs.Take(count)), dataProvider);
    }

    public override sealed ElementEnumerable<T, TId> Union(T element)
    {
      return Union(new[] { element });
    }

    public override sealed ElementEnumerable<T, TId> Union(IEnumerable<T> second)
    {
      if (second is LazyElementEnumerable<T, TId, TConstraint>)
      {
        return new LazyElementEnumerable<T, TId, TConstraint>(new MaterializedIdEnumerable<TId>(IDs.Union((second as LazyElementEnumerable<T, TId, TConstraint>).IDs)), dataProvider);
      }
      else
      {
        return new MaterializedElementEnumerable<T, TId>(UnionIterator(second));
      }
    }

    private IEnumerable<T> UnionIterator(IEnumerable<T> second)
    {
      var set = new HashSet<TId>();

      foreach (var element in second)
      {
        set.Add(dataProvider.GetId(element));
        yield return element;
      }

      foreach (var id in IDs.Where(id => set.Add(id)))
      {
        yield return dataProvider.GetElement<T>(id);
      }
    }
  }

  #endregion

  #region Class MaterializedElementEnumerable

  class MaterializedElementEnumerable<T, TId> : ElementEnumerable<T, TId>
  {
    private IEnumerable<T> elements;

    public MaterializedElementEnumerable(IEnumerable<T> elements)
    {
      this.elements = elements;
    }

    public override sealed IEnumerator<T> GetEnumerator()
    {
      return elements.GetEnumerator();
    }

    public override sealed ElementEnumerable<T, TId> Concat(T element)
    {
      return Concat(new[] { element });
    }

    public override sealed ElementEnumerable<T, TId> Concat(IEnumerable<T> second)
    {
      return new MaterializedElementEnumerable<T, TId>(elements.Concat(second));
    }

    public override sealed bool Contains(T value)
    {
      return elements.Contains(value);
    }

    public override sealed int Count()
    {
      return elements.Count();
    }

    public override sealed ElementEnumerable<T, TId> Distinct()
    {
      return new MaterializedElementEnumerable<T, TId>(elements.Distinct());
    }

    public override sealed T ElementAt(int index)
    {
      return elements.ElementAt(index);
    }

    public override sealed T ElementAtOrDefault(int index)
    {
      return elements.ElementAtOrDefault(index);
    }

    public override sealed ElementEnumerable<T, TId> Except(T element)
    {
      return Except(new[] { element });
    }

    public override sealed ElementEnumerable<T, TId> Except(IEnumerable<T> second)
    {
      return new MaterializedElementEnumerable<T, TId>(elements.Except(second));
    }

    public override sealed ElementEnumerable<T, TId> Intersect(T element)
    {
      return Intersect(new[] { element });
    }

    public override sealed ElementEnumerable<T, TId> Intersect(IEnumerable<T> second)
    {
      return new MaterializedElementEnumerable<T, TId>(elements.Intersect(second));
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

    public override ElementEnumerable<TResult, TId> OfType<TResult>()
    {
      return new MaterializedElementEnumerable<TResult, TId>(elements.OfType<TResult>());
    }

    public override sealed ElementEnumerable<T, TId> Reverse()
    {
      return new MaterializedElementEnumerable<T, TId>(elements.Reverse());
    }

    public override sealed bool SequenceEqual(T element)
    {
      return SequenceEqual(new[] { element });
    }

    public override sealed bool SequenceEqual(IEnumerable<T> second)
    {
      return elements.SequenceEqual(second);
    }

    public override sealed ElementEnumerable<T, TId> Skip(int count)
    {
      return new MaterializedElementEnumerable<T, TId>(elements.Skip(count));
    }

    public override sealed ElementEnumerable<T, TId> Take(int count)
    {
      return new MaterializedElementEnumerable<T, TId>(elements.Take(count));
    }

    public override sealed ElementEnumerable<T, TId> Union(T element)
    {
      return Union(new[] { element });
    }

    public override sealed ElementEnumerable<T, TId> Union(IEnumerable<T> second)
    {
      return new MaterializedElementEnumerable<T, TId>(elements.Union(second));
    }
  }

  #endregion

  public interface IDataProvider<TId, TConstraint>
  {
    T GetElement<T>(TId id) where T : TConstraint;

    TId GetId<T>(T element) where T : TConstraint;

    IEnumerable<TId> Filter<T>(IEnumerable<TId> ids) where T : TConstraint;
  }
}
