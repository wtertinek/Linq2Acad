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
      var ids = new List<int>();
      var elements = new List<Element>();
      var result = new DummyEnumerable(e => elements.Add(e), id => ids.Add(id), 0, 2)
                   .Concat(new DummyEnumerable(e => elements.Add(e), id => ids.Add(id), 2, 2))
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
      var ids = new List<int>();
      var elements = new List<Element>();
      var result = new DummyEnumerable(e => elements.Add(e), id => ids.Add(id), 0, 2)
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
      var result = new DummyEnumerable(e => elements.Add(e), id => ids.Add(id), 0, 4)
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
      var result = new DummyEnumerable(e => elements.Add(e), id => ids.Add(id), 0, 2)
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
      var result = new DummyEnumerable(e => elements.Add(e), id => ids.Add(id), 0, 2)
                   .Concat(new DummyEnumerable(e => elements.Add(e), id => ids.Add(id), 0, 2))
                   .Count();

      Assert.AreEqual(0, elements.Count);
      Assert.AreEqual(0, ids.Count);
      Assert.AreEqual(4, result);
    }

    [TestMethod]
    public void TestCountAfterConcatWithTwoLazyEnumerables()
    {
      var ids = new List<int>();
      var elements = new List<Element>();
      var result = new DummyEnumerable(e => elements.Add(e), id => ids.Add(id), 0, 2)
                   .Concat(new DummyEnumerable(e => elements.Add(e), id => ids.Add(id), 2, 2))
                   .Concat(new DummyEnumerable(e => elements.Add(e), id => ids.Add(id), 4, 2))
                   .Count();

      Assert.AreEqual(0, elements.Count);
      Assert.AreEqual(0, ids.Count);
      Assert.AreEqual(6, result);
    }

    [TestMethod]
    public void TestLastAfterConcat()
    {
      var ids = new List<int>();
      var elements = new List<Element>();
      var result = new DummyEnumerable(e => elements.Add(e), id => ids.Add(id), 0, 2)
                   .Concat(new DummyEnumerable(e => elements.Add(e), id => ids.Add(id), 2, 2))
                   .Last();

      Assert.AreEqual(1, elements.Count);
      Assert.AreEqual(3, elements[0].ID);
      Assert.AreEqual(1, ids.Count);

      Assert.AreEqual(3, result.ID);
    }

    [TestMethod]
    public void TestOfType()
    {
      var ids = new List<int>();
      var elements = new List<Element>();
      var result = new DummyEnumerable(e => elements.Add(e), id => ids.Add(id), 0, 3, new Element(0),
                                                                                      new DerivedElement(1),
                                                                                      new Element(2))
                   .OfType<DerivedElement>()
                   .ToArray();

      Assert.AreEqual(1, elements.Count);
      Assert.AreEqual(1, elements[0].ID);
      Assert.AreEqual(3, ids.Count);

      Assert.AreEqual(1, result.Length);
      Assert.AreEqual(1, result[0].ID);
    }

    [TestMethod]
    public void TestSequenceEquals()
    {
      var ids = new List<int>();
      var elements = new List<Element>();
      var result = new DummyEnumerable(e => elements.Add(e), id => ids.Add(id), 0, 2)
                   .SequenceEqual(new DummyEnumerable(e => elements.Add(e), id => ids.Add(id), 0, 2));

      Assert.AreEqual(0, elements.Count);
      Assert.AreEqual(4, ids.Count);
      Assert.IsTrue(result);
    }

    [TestMethod]
    public void TestSkip()
    {
      var ids = new List<int>();
      var elements = new List<Element>();
      var result = new DummyEnumerable(e => elements.Add(e), id => ids.Add(id), 0, 4)
                   .Skip(2)
                   .ToArray();

      Assert.AreEqual(2, elements.Count);
      Assert.AreEqual(2, ids.Count);

      Assert.AreEqual(2, result.Length);
      Assert.AreEqual(2, result[0].ID);
      Assert.AreEqual(3, result[1].ID);
    }

    #region Nested class DummyEnumerable

    [DebuggerStepThrough]
    private class DummyEnumerable : LazyElementEnumerable<Element, int, Element>
    {
      public DummyEnumerable(Action<Element> elementAccessed, Action<int> idAccessed, int startIndex, int count)
        : base(new LazayIdEnumerable<int>(new IntegerEnumerable(startIndex, count), id => { idAccessed((int)id); return (int)id; }), new ElementDataProvider(GetElements(startIndex, count), elementAccessed, idAccessed))
      {
      }

      public DummyEnumerable(Action<Element> elementAccessed, Action<int> idAccessed, int startIndex, int count, params Element[] elements)
        : base(new LazayIdEnumerable<int>(new IntegerEnumerable(startIndex, count), id => { idAccessed((int)id); return (int)id; }), new ElementDataProvider(elements.ToDictionary(e => e.ID, e => e), elementAccessed, idAccessed))
      {
      }

      private static Dictionary<int, Element> GetElements(int startIndex, int count)
      {
        return Enumerable.Range(startIndex, count)
                         .Select(id => new Element(id))
                         .ToDictionary(e => e.ID, e => e);
      }
    }

    #endregion

    #region Nested class ElementDataProvider

    [DebuggerStepThrough]
    class ElementDataProvider : IDataProvider<int, Element>
    {
      private Dictionary<int, Element> elements;
      private Action<Element> elementAccessed;
      private Action<int> idAccessed;

      public ElementDataProvider(Dictionary<int, Element> elements, Action<Element> elementAccessed, Action<int> idAccessed)
      {
        this.elements = elements;
        this.elementAccessed = elementAccessed;
        this.idAccessed = idAccessed;
      }

      public T GetElement<T>(int id) where T : Element
      {
        if (!elements.ContainsKey(id))
        {
          elements[id] = new Element(id);
        }

        var element = (T)elements[id];
        elementAccessed(element);
        return element;
      }

      public int GetId<T>(T element) where T : Element
      {
        idAccessed(element.ID);
        return element.ID;
      }

      public IEnumerable<int> Filter<T>(IEnumerable<int> ids) where T : Element
      {
        return ids.Where(id => elements[id] is T);
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

    #region Nested class DerivedElement

    [DebuggerStepThrough]
    class DerivedElement : Element
    {
      public DerivedElement(int id)
        : base(id)
      {
      }
    }

    #endregion
  }
}
