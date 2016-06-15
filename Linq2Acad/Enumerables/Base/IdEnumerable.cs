using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2Acad
{
  // This code is based on the implementation of System.Linq.Enumerable
  // http://referencesource.microsoft.com/#system.core/System/Linq/Enumerable.cs
  public class IdEnumerable<T> : IEnumerable<T>
  {
    private IEnumerable ids;
    private Func<object, T> getID;

    public IdEnumerable(IEnumerable ids, Func<object, T> getID)
    {
      this.ids = ids;
      this.getID = getID;
    }

    public IEnumerator<T> GetEnumerator()
    {
      var enumerator = ids.GetEnumerator();

      while (enumerator.MoveNext())
      {
        yield return getID(enumerator.Current);
      }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }

    // TODO: Implement Concat()
    //public IEnumerable<T> Concat(IEnumerable<T> second)
    //{
    //  throw new NotImplementedException();
    //}

    public int Count()
    {
      var count = 0;

      var enumerator = ids.GetEnumerator();

      while (enumerator.MoveNext())
      {
        count++;
      }

      return count;
    }

    public T ElementAt(int index)
    {
      var enumerator = ids.GetEnumerator();

      while (true)
      {
        if (!enumerator.MoveNext())
        {
          throw new ArgumentOutOfRangeException("index");
        }

        if (index == 0)
        {
          return getID(enumerator.Current);
        }

        index--;
      }
    }

    public T ElementAtOrDefault(int index)
    {
      var enumerator = ids.GetEnumerator();

      while (true)
      {
        if (!enumerator.MoveNext())
        {
          break;
        }

        if (index == 0)
        {
          return getID(enumerator.Current);
        }

        index--;
      }

      return default(T);
    }

    public T Last()
    {
      var enumerator = ids.GetEnumerator();

      if (enumerator.MoveNext())
      {
        object result;

        do
        {
          result = enumerator.Current;
        }
        while (enumerator.MoveNext());

        return getID(result);
      }
      else
      {
        throw new InvalidOperationException("Sequence contains no elements");
      }
    }

    public T LastOrDefault()
    {
      var enumerator = ids.GetEnumerator();

      if (enumerator.MoveNext())
      {
        object result;

        do
        {
          result = enumerator.Current;
        }
        while (enumerator.MoveNext());

        return getID(result);
      }
      else
      {
        return default(T);
      }
    }

    public long LongCount()
    {
      long count = 0;

      var enumerator = ids.GetEnumerator();

      while (enumerator.MoveNext())
      {
        count++;
      }

      return count;
    }

    // TODO: Implement Reverse()
    //public IEnumerable<T> Reverse()
    //{
    //  throw new NotImplementedException();
    //}

    public IEnumerable<T> Skip(int count)
    {
      var enumerable = ids.GetEnumerator();

      while (count > 0 && enumerable.MoveNext())
      {
        count--;
      }

      if (count <= 0)
      {
        while (enumerable.MoveNext())
        {
          yield return getID(enumerable.Current);
        }
      }
    }

    public IEnumerable<T> Take(int count)
    {
      var enumerable = ids.GetEnumerator();

      while (enumerable.MoveNext())
      {
        yield return getID(enumerable.Current);

        if (--count == 0)
        {
          break;
        }
      }
    }
  }
}
