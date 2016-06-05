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
      var elements = new List<string>();
      var ids = new List<int>();
      var result = new StringEnumerable(0, 4, e => elements.Add(e), id => ids.Add(id))
                   .Skip(2)
                   .ToArray();

      Assert.AreEqual(2, elements.Count);
      Assert.AreEqual(2, ids.Count);

      Assert.AreEqual(2, result.Length);
      Assert.AreEqual("2", result[0]);
      Assert.AreEqual("3", result[1]);
    }

    [TestMethod]
    public void TestSequenceEqual()
    {
      var elements = new List<string>();
      var ids = new List<int>();
      var result = new StringEnumerable(0, 2, e => elements.Add(e), id => ids.Add(id))
                   .SequenceEqual(new StringEnumerable(0, 2, e => elements.Add(e), id => ids.Add(id)));

      Assert.AreEqual(0, elements.Count);
      Assert.AreEqual(4, ids.Count);

      Assert.IsTrue(result);
    }

    [TestMethod]
    public void TestConcatFollowedByLast()
    {
      var elements = new List<string>();
      var ids = new List<int>();
      var result = new StringEnumerable(0, 2, e => elements.Add(e), id => ids.Add(id))
                   .Concat(new StringEnumerable(2, 2, e => elements.Add(e), id => ids.Add(id)))
                   .Last();

      Assert.AreEqual(1, elements.Count);
      Assert.AreEqual(1, ids.Count);

      Assert.AreEqual(1, result.Length);
      Assert.AreEqual("3", result);
    }

    private class StringEnumerable : LazyElementEnumerable<string, int>
    {
      public StringEnumerable(int startIndex, int count, Action<string> elementAccessed, Action<int> idAccessed)
        : base(new LazyIdEnumerable<int>(Enumerable.Range(startIndex, count).Select(i => (object)i), i => { idAccessed((int)i); return (int)i; }),
               id => { elementAccessed(id.ToString()); return id.ToString(); }, s => int.Parse(s))
      {
      }
    }
  }
}
