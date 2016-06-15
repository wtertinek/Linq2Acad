using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace Linq2Acad
{
  [TestClass]
  public class ElementEnumerableTests
  {
    [TestMethod]
    public void TestConcatWithLazyEnumerable()
    {
      var elements = new List<Element>();
      var ids = new List<int>();
      var result = new DummyEnumerable(0, 2, e => elements.Add(e), id => ids.Add(id))
                   .Concat(new DummyEnumerable(2, 2, e => elements.Add(e), id => ids.Add(id)))
                   .ToArray();

      Assert.AreEqual(4, elements.Count);
      Assert.AreEqual(0, elements[0].ID);
      Assert.AreEqual(1, elements[1].ID);
      Assert.AreEqual(2, elements[2].ID);
      Assert.AreEqual(3, elements[3].ID);

      Assert.AreEqual(4, ids.Count);
      Assert.AreEqual(0, ids[0]);
      Assert.AreEqual(1, ids[1]);
      Assert.AreEqual(2, ids[2]);
      Assert.AreEqual(3, ids[3]);

      Assert.AreEqual(4, result.Length);
      Assert.AreEqual(0, result[0].ID);
      Assert.AreEqual(1, result[1].ID);
      Assert.AreEqual(2, result[2].ID);
      Assert.AreEqual(3, result[3].ID);
    }

    [TestMethod]
    public void TestConcatWithArray()
    {
      var elements = new List<Element>();
      var ids = new List<int>();
      var result = new DummyEnumerable(0, 2, e => elements.Add(e), id => ids.Add(id))
                   .Concat(new [] { new Element(2), new Element(3) })
                   .ToArray();

      Assert.AreEqual(2, elements.Count);
      Assert.AreEqual(0, elements[0].ID);
      Assert.AreEqual(1, elements[1].ID);

      Assert.AreEqual(2, ids.Count);
      Assert.AreEqual(0, ids[0]);
      Assert.AreEqual(1, ids[1]);

      Assert.AreEqual(4, result.Length);
      Assert.AreEqual(0, result[0].ID);
      Assert.AreEqual(1, result[1].ID);
      Assert.AreEqual(2, result[2].ID);
      Assert.AreEqual(3, result[3].ID);
    }

    [TestMethod]
    public void TestCount()
    {
      var ids = new List<int>();
      var elements = new List<Element>();
      var result = new DummyEnumerable(0, 4, e => elements.Add(e), id => ids.Add(id))
                   .Count();

      Assert.AreEqual(0, elements.Count);
      Assert.AreEqual(0, ids.Count);
      Assert.AreEqual(4, result);
    }

    [TestMethod]
    public void TestCountAfterConcatWithArray()
    {
      var ids = new List<int>();
      var elements = new List<Element>();
      var result = new DummyEnumerable(0, 2, e => elements.Add(e), id => ids.Add(id))
                   .Concat(new[] { new Element(3), new Element(4) })
                   .Count();

      Assert.AreEqual(2, elements.Count);
      Assert.AreEqual(2, ids.Count);
      Assert.AreEqual(4, result);
    }

    [TestMethod]
    public void TestCountAfterConcatWithLazyEnumerable()
    {
      var ids = new List<int>();
      var elements = new List<Element>();
      var result = new DummyEnumerable(0, 2, e => elements.Add(e), id => ids.Add(id))
                   .Concat(new DummyEnumerable(0, 2, e => elements.Add(e), id => ids.Add(id)))
                   .Count();

      Assert.AreEqual(0, elements.Count);
      Assert.AreEqual(4, ids.Count);
      Assert.AreEqual(4, result);
    }

    [TestMethod]
    public void TestLastAfterConcat()
    {
      var elements = new List<Element>();
      var ids = new List<int>();
      var result = new DummyEnumerable(0, 2, e => elements.Add(e), id => ids.Add(id))
                   .Concat(new DummyEnumerable(2, 2, e => elements.Add(e), id => ids.Add(id)))
                   .Last();

      Assert.AreEqual(1, elements.Count);
      Assert.AreEqual(3, elements[0].ID);
      Assert.AreEqual(4, ids.Count);

      Assert.AreEqual(3, result.ID);
    }

    [TestMethod]
    public void TestSequenceEquals()
    {
      var ids = new List<int>();
      var elements = new List<Element>();
      var result = new DummyEnumerable(0, 2, e => elements.Add(e), id => ids.Add(id))
                   .SequenceEqual(new DummyEnumerable(0, 2, e => elements.Add(e), id => ids.Add(id)));

      Assert.AreEqual(0, elements.Count);
      Assert.AreEqual(4, ids.Count);
      Assert.IsTrue(result);
    }

    [TestMethod]
    public void TestSkip()
    {
      var ids = new List<int>();
      var elements = new List<Element>();
      var result = new DummyEnumerable(0, 4, e => elements.Add(e), id => ids.Add(id))
                   .Skip(2)
                   .ToArray();

      Assert.AreEqual(2, elements.Count);
      Assert.AreEqual(2, ids.Count);

      Assert.AreEqual(2, result.Length);
      Assert.AreEqual(2, result[0].ID);
      Assert.AreEqual(3, result[1].ID);
    }

    [DebuggerStepThrough]
    private class DummyEnumerable : LazyElementEnumerable<Element, int, Element>
    {
      public DummyEnumerable(int startIndex, int count, Action<Element> elementAccessed, Action<int> idAccessed)
        : base(new IntegerEnumerable(startIndex, count, idAccessed), new DataProvider(elementAccessed, idAccessed))
      {
      }
    }

    #region IntegerEnumerable

    public class IntegerEnumerable : IEnumerable<int>
    {
      private int startIndex;
      private int count;
      private Action<int> accessed;

      [DebuggerStepThrough]
      public IntegerEnumerable(int startIndex, int count, Action<int> accessed)
      {
        this.startIndex = startIndex;
        this.count = count;
        this.accessed = accessed;
      }

      public IEnumerator<int> GetEnumerator()
      {
        return new IntegerEnumerator(startIndex, count, accessed);
      }

      System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
      {
        return GetEnumerator();
      }
    }

    #endregion

    #region IntegerEnumerator

    private class IntegerEnumerator : IEnumerator<int>
    {
      private int startIndex;
      private int count;
      private int current;
      private Action<int> accessed;

      [DebuggerStepThrough]
      public IntegerEnumerator(int startIndex, int count, Action<int> accessed)
      {
        this.startIndex = startIndex;
        this.count = count;
        this.accessed = accessed;
        this.current = startIndex - 1;
      }

      public int Current
      {
        get
        {
          accessed(current);
          return current;
        }
      }

      public void Dispose()
      {

      }

      object System.Collections.IEnumerator.Current
      {
        get { return Current; }
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

    #region Nested class DataProvider

    [DebuggerStepThrough]
    class DataProvider : IDataProvider<int, Element>
    {
      private Action<Element> elementAccessed;
      private Action<int> idAccessed;

      public DataProvider(Action<Element> elementAccessed, Action<int> idAccessed)
      {
        this.elementAccessed = elementAccessed;
        this.idAccessed = idAccessed;
      }

      public T GetElement<T>(int id) where T : Element
      {
        var tmp = (T)new Element(id);
        elementAccessed(tmp);
        return tmp;
      }

      public int GetId<T>(T element) where T : Element
      {
        idAccessed(element.ID);
        return element.ID;
      }

      public IEnumerable<int> Filter<T>(IEnumerable<int> ids) where T : Element
      {
        return ids;
      }
    }

    #endregion

    #region Nested class Element

    [DebuggerStepThrough]
    class Element
    {
      public Element(int id)
      {
        ID = id;
      }

      public int ID { get; private set; }
    }

    #endregion
  }
}
