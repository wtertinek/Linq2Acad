using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linq2Acad
{
  [TestClass]
  public class ElementEnumerableTests
  {
    [TestMethod]
    public void TestSkip()
    {
      var elements = new List<Element>();
      var ids = new List<int>();
      var result = new DummyEnumerable(0, 4, e => elements.Add(e), id => ids.Add(id))
                   .Skip(2)
                   .ToArray();

      Assert.AreEqual(2, elements.Count);
      Assert.AreEqual(2, ids.Count);

      Assert.AreEqual(2, result.Length);
      Assert.AreEqual(2, result[0].ID);
      Assert.AreEqual(3, result[1].ID);
    }

    [TestMethod]
    public void TestSequenceEqual()
    {
      var elements = new List<Element>();
      var ids = new List<int>();
      var result = new DummyEnumerable(0, 2, e => elements.Add(e), id => ids.Add(id))
                   .SequenceEqual(new DummyEnumerable(0, 2, e => elements.Add(e), id => ids.Add(id)));

      Assert.AreEqual(0, elements.Count);
      Assert.AreEqual(4, ids.Count);

      Assert.IsTrue(result);
    }

    [TestMethod]
    public void TestConcatFollowedByLast()
    {
      var elements = new List<Element>();
      var ids = new List<int>();
      var result = new DummyEnumerable(0, 2, e => elements.Add(e), id => ids.Add(id))
                   .Concat(new DummyEnumerable(2, 2, e => elements.Add(e), id => ids.Add(id)))
                   .Last();

      Assert.AreEqual(1, elements.Count);
      Assert.AreEqual(1, ids.Count);

      Assert.AreEqual(3, result.ID);
    }

    private class DummyEnumerable : LazyElementEnumerable<Element, int, Element>
    {
      public DummyEnumerable(int startIndex, int count, Action<Element> elementAccessed, Action<int> idAccessed)
        : base(new LazyIdEnumerable<int>(Enumerable.Range(startIndex, count).Select(i => (object)i), i => { idAccessed((int)i); return (int)i; }),
               new DataProvider(elementAccessed, idAccessed))
      {
      }
    }

    #region Nested class DataProvider

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

      public IdEnumerable<int> Filter<T>(IdEnumerable<int> ids) where T : Element
      {
        return ids;
      }
    }

    #endregion

    #region Nested class Element

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
