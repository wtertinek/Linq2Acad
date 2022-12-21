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
      => GetEnumerator();

    public abstract ElementEnumerable<T, TId> Concat(T element);

    public abstract ElementEnumerable<T, TId> Concat(IEnumerable<T> second);

    public abstract bool Contains(T element);

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
    private readonly IDataProvider<TId, TConstraint> dataProvider;

    internal LazyElementEnumerable(IdEnumerable<TId> ids, IDataProvider<TId, TConstraint> dataProvider)
    {
      IDs = ids;
      this.dataProvider = dataProvider;
    }

    internal IdEnumerable<TId> IDs { get; }

    public sealed override IEnumerator<T> GetEnumerator()
      => IDs.Select(id => dataProvider.GetElement<T>(id))
            .GetEnumerator();

    public sealed override ElementEnumerable<T, TId> Concat(T element)
      => Concat(new[] { element });

    public sealed override ElementEnumerable<T, TId> Concat(IEnumerable<T> second)
      => second is LazyElementEnumerable<T, TId, TConstraint>
           ? new LazyElementEnumerable<T, TId, TConstraint>(IDs.Concat((second as LazyElementEnumerable<T, TId, TConstraint>).IDs), dataProvider)
           : (ElementEnumerable<T, TId>)new MaterializedElementEnumerable<T, TId>(IDs.Select(id => dataProvider.GetElement<T>(id))
                                                            .Concat(second));

    public override bool Contains(T element)
      => IDs.Contains(dataProvider.GetId(element));

    public override int Count()
      => IDs.Count();

    public sealed override ElementEnumerable<T, TId> Distinct()
      => new LazyElementEnumerable<T, TId, TConstraint>(new MaterializedIdEnumerable<TId>(IDs.Distinct()), dataProvider);

    public sealed override T ElementAt(int index)
      => dataProvider.GetElement<T>(IDs.ElementAt(index));

    public sealed override T ElementAtOrDefault(int index)
    {
      var id = IDs.ElementAtOrDefault(index);

      return Equals(id, default(TId))
               ? default
               : dataProvider.GetElement<T>(id);
    }

    public sealed override ElementEnumerable<T, TId> Except(T element)
      => Except(new[] { element });

    public sealed override ElementEnumerable<T, TId> Except(IEnumerable<T> second)
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

    public sealed override ElementEnumerable<T, TId> Intersect(T element)
      => Intersect(new[] { element });

    public sealed override ElementEnumerable<T, TId> Intersect(IEnumerable<T> second)
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

    public sealed override T Last()
      => dataProvider.GetElement<T>(IDs.Last());

    public sealed override T LastOrDefault()
    {
      var id = IDs.LastOrDefault();
      return Equals(id, default(TId))
               ? default
               : dataProvider.GetElement<T>(id);
    }

    public sealed override long LongCount()
      => IDs.LongCount();

    public override ElementEnumerable<TResult, TId> OfType<TResult>()
      => new LazyElementEnumerable<TResult, TId, TConstraint>(new MaterializedIdEnumerable<TId>(dataProvider.Filter<TResult>(IDs)), dataProvider);

    public sealed override ElementEnumerable<T, TId> Reverse()
      => new LazyElementEnumerable<T, TId, TConstraint>(new MaterializedIdEnumerable<TId>(IDs.Reverse()), dataProvider);

    public sealed override bool SequenceEqual(T element)
      => SequenceEqual(new[] { element });

    public sealed override bool SequenceEqual(IEnumerable<T> second)
      => second is LazyElementEnumerable<T, TId, TConstraint>
           ? IDs.SequenceEqual((second as LazyElementEnumerable<T, TId, TConstraint>).IDs)
           : IDs.SequenceEqual(second.Select(e => dataProvider.GetId(e)));

    public sealed override ElementEnumerable<T, TId> Skip(int count)
      => new LazyElementEnumerable<T, TId, TConstraint>(new MaterializedIdEnumerable<TId>(IDs.Skip(count)), dataProvider);

    public sealed override ElementEnumerable<T, TId> Take(int count)
      => new LazyElementEnumerable<T, TId, TConstraint>(new MaterializedIdEnumerable<TId>(IDs.Take(count)), dataProvider);

    public sealed override ElementEnumerable<T, TId> Union(T element)
      => Union(new[] { element });

    public sealed override ElementEnumerable<T, TId> Union(IEnumerable<T> second)
    {
      Require.ParameterNotNull(second, nameof(second));

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

  #region Internal class MaterializedElementEnumerable

  internal class MaterializedElementEnumerable<T, TId> : ElementEnumerable<T, TId>
  {
    private readonly IEnumerable<T> elements;

    public MaterializedElementEnumerable(IEnumerable<T> elements)
      => this.elements = elements;

    public sealed override IEnumerator<T> GetEnumerator()
      => elements.GetEnumerator();

    public sealed override ElementEnumerable<T, TId> Concat(T element)
      => Concat(new[] { element });

    public sealed override ElementEnumerable<T, TId> Concat(IEnumerable<T> second)
      => new MaterializedElementEnumerable<T, TId>(elements.Concat(second));

    public sealed override bool Contains(T value)
      => elements.Contains(value);

    public sealed override int Count()
      => elements.Count();

    public sealed override ElementEnumerable<T, TId> Distinct()
      => new MaterializedElementEnumerable<T, TId>(elements.Distinct());

    public sealed override T ElementAt(int index)
      => elements.ElementAt(index);

    public sealed override T ElementAtOrDefault(int index)
      => elements.ElementAtOrDefault(index);

    public sealed override ElementEnumerable<T, TId> Except(T element)
      => Except(new[] { element });

    public sealed override ElementEnumerable<T, TId> Except(IEnumerable<T> second)
      => new MaterializedElementEnumerable<T, TId>(elements.Except(second));

    public sealed override ElementEnumerable<T, TId> Intersect(T element)
      => Intersect(new[] { element });

    public sealed override ElementEnumerable<T, TId> Intersect(IEnumerable<T> second)
      => new MaterializedElementEnumerable<T, TId>(elements.Intersect(second));

    public sealed override T Last()
      => elements.Last();

    public sealed override T LastOrDefault()
      => elements.LastOrDefault();

    public sealed override long LongCount()
      => elements.LongCount();

    public override ElementEnumerable<TResult, TId> OfType<TResult>()
      => new MaterializedElementEnumerable<TResult, TId>(elements.OfType<TResult>());

    public sealed override ElementEnumerable<T, TId> Reverse()
      => new MaterializedElementEnumerable<T, TId>(elements.Reverse());

    public sealed override bool SequenceEqual(T element)
      => SequenceEqual(new[] { element });

    public sealed override bool SequenceEqual(IEnumerable<T> second)
      => elements.SequenceEqual(second);

    public sealed override ElementEnumerable<T, TId> Skip(int count)
      => new MaterializedElementEnumerable<T, TId>(elements.Skip(count));

    public sealed override ElementEnumerable<T, TId> Take(int count)
      => new MaterializedElementEnumerable<T, TId>(elements.Take(count));

    public sealed override ElementEnumerable<T, TId> Union(T element)
      => Union(new[] { element });

    public sealed override ElementEnumerable<T, TId> Union(IEnumerable<T> second)
      => new MaterializedElementEnumerable<T, TId>(elements.Union(second));
  }

  #endregion

  #region Internal interface IDataProvider<TId, TConstraint>

  internal interface IDataProvider<TId, TConstraint>
  {
    T GetElement<T>(TId id) where T : TConstraint;

    TId GetId<T>(T element) where T : TConstraint;

    IEnumerable<TId> Filter<T>(IEnumerable<TId> ids) where T : TConstraint;
  }

  #endregion
}
