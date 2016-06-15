using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;

namespace Linq2Acad
{
  [TestClass]
  public class IdEnumerableTests
  {
    [TestMethod]
    public void TestContains()
    {
      var enumerated = new List<object>();
      var enumerable = new IdEnumerable<int>(new IntegerEnumerable(0, 3), id => { enumerated.Add(id); return (int)id; });
      var result = enumerable.Contains(1);

      Assert.AreEqual(2, enumerated.Count);
      Assert.IsTrue(result);
    }

    [TestMethod]
    public void TestCount()
    {
      var enumerated = new List<object>();
      var enumerable = new IdEnumerable<int>(new IntegerEnumerable(0, 3), id => { enumerated.Add(id); return (int)id; });
      var result = enumerable.Count();

      Assert.AreEqual(0, enumerated.Count);
      Assert.AreEqual(3, result);
    }

    [TestMethod]
    public void TestElementAt()
    {
      var enumerated = new List<object>();
      var enumerable = new IdEnumerable<int>(new IntegerEnumerable(0, 3), id => { enumerated.Add(id); return (int)id; });
      var result = enumerable.ElementAt(1);

      Assert.AreEqual(1, enumerated.Count);
      Assert.AreEqual(1, result);
    }

    [TestMethod]
    public void TestElementAtOrDefault()
    {
      var enumerated = new List<object>();
      var enumerable = new IdEnumerable<int>(new IntegerEnumerable(0, 3), id => { enumerated.Add(id); return (int)id; });
      var result = enumerable.ElementAtOrDefault(1);

      Assert.AreEqual(1, enumerated.Count);
      Assert.AreEqual(1, result);
    }

    [TestMethod]
    public void TestLast()
    {
      var enumerated = new List<object>();
      var enumerable = new IdEnumerable<int>(new IntegerEnumerable(0, 3), id => { enumerated.Add(id); return (int)id; });
      var result = enumerable.Last();

      Assert.AreEqual(1, enumerated.Count);
      Assert.AreEqual(2, result);
    }

    [TestMethod]
    public void TestLastOrDefault()
    {
      var enumerated = new List<object>();
      var enumerable = new IdEnumerable<int>(new IntegerEnumerable(0, 3), id => { enumerated.Add(id); return (int)id; });
      var result = enumerable.LastOrDefault();

      Assert.AreEqual(1, enumerated.Count);
      Assert.AreEqual(2, result);
    }

    [TestMethod]
    public void TestLongCount()
    {
      var enumerated = new List<object>();
      var enumerable = new IdEnumerable<int>(new IntegerEnumerable(0, 3), id => { enumerated.Add(id); return (int)id; });
      var result = enumerable.LongCount();

      Assert.AreEqual(0, enumerated.Count);
      Assert.AreEqual(3, result);
    }

    [TestMethod]
    public void TestSequenceEqual()
    {
      var enumerated = new List<object>();
      var enumerable = new IdEnumerable<int>(new IntegerEnumerable(0, 3), id => { enumerated.Add(id); return (int)id; });
      var result = enumerable.SequenceEqual(new [] { 0, 1, 2 });

      Assert.AreEqual(3, enumerated.Count);
      Assert.IsTrue(result);
    }

    [TestMethod]
    public void TestSkip()
    {
      var enumerated = new List<object>();
      var enumerable = new IdEnumerable<int>(new IntegerEnumerable(0, 3), id => { enumerated.Add(id); return (int)id; });
      var result = enumerable.Skip(2)
                             .ToArray();

      Assert.AreEqual(1, enumerated.Count);
      Assert.AreEqual(1, result.Length);
      Assert.AreEqual(2, result[0]);
    }

    [TestMethod]
    public void TestTake()
    {
      var enumerated = new List<object>();
      var enumerable = new IdEnumerable<int>(new IntegerEnumerable(0, 3), id => { enumerated.Add(id); return (int)id; });
      var result = enumerable.Take(2)
                             .ToArray();

      Assert.AreEqual(2, enumerated.Count);
      Assert.AreEqual(2, result.Length);
      Assert.AreEqual(0, result[0]);
      Assert.AreEqual(1, result[1]);
    }

    #region IntegerEnumerable

    public class IntegerEnumerable : IEnumerable
    {
      private int startIndex;
      private int count;

      [DebuggerStepThrough]
      public IntegerEnumerable(int startIndex, int count)
      {
        this.startIndex = startIndex;
        this.count = count;
      }

      public IEnumerator GetEnumerator()
      {
        return new IntegerEnumerator(startIndex, count);
      }

      #region IntegerEnumerator

      private class IntegerEnumerator : IEnumerator
      {
        private int startIndex;
        private int count;
        private int current;

        [DebuggerStepThrough]
        public IntegerEnumerator(int startIndex, int count)
        {
          this.startIndex = startIndex;
          this.count = count;
          this.current = startIndex - 1;
        }

        public object Current
        {
          get { return current; }
        }

        public bool MoveNext()
        {
          if (current < (startIndex + count - 1))
          {
            current++;
            return true;
          }
          else
          {
            return false;
          }
        }

        public void Reset()
        {
          current = startIndex - 1;
        }
      }

      #endregion

    }

    #endregion
  }
}
