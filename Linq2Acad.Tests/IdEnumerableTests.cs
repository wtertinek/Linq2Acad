using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linq2Acad
{
  [TestClass]
  public class IdEnumerableTests
  {
    [TestMethod]
    public void TestConcatWithArrayNotMaterialized()
    {
      var accessed = new List<int>();
      var enumerable = new IntegerEnumerable(0, 4, id => accessed.Add(id));
      enumerable.Concat(new [] { 0, 1 });

      Assert.AreEqual(0, accessed.Count);
    }

    [TestMethod]
    public void TestConcatWithIdEnumerableNotMaterialized()
    {
      var accessed = new List<int>();
      var enumerable = new IntegerEnumerable(0, 2, id => accessed.Add(id));
      enumerable.Concat(new IntegerEnumerable(0, 2, id => accessed.Add(id)));

      Assert.AreEqual(0, accessed.Count);
    }

    [TestMethod]
    public void TestConcatWithArrayMaterialized()
    {
      var accessed = new List<int>();
      var enumerable = new IntegerEnumerable(0, 2, id => accessed.Add(id));
      var result = enumerable.Concat(new[] { 2, 3 })
                             .ToArray();

      Assert.AreEqual(2, accessed.Count);
    }

    [TestMethod]
    public void TestConcatWithIdEnumerableMaterialized()
    {
      var accessed = new List<int>();
      var enumerable = new IntegerEnumerable(0, 2, id => accessed.Add(id));
      var result = enumerable.Concat(new IntegerEnumerable(2, 2, id => accessed.Add(id)))
                             .ToArray();

      Assert.AreEqual(4, accessed.Count);
      Assert.AreEqual(0, accessed[0]);
      Assert.AreEqual(1, accessed[1]);
      Assert.AreEqual(2, accessed[2]);
      Assert.AreEqual(3, accessed[3]);

      Assert.AreEqual(4, result.Length);
      Assert.AreEqual(0, result[0]);
      Assert.AreEqual(1, result[1]);
      Assert.AreEqual(2, result[2]);
      Assert.AreEqual(3, result[3]);
    }

    [TestMethod]
    public void TestConcatWithIdEnumerableFollowedBySkipNotMaterialized()
    {
      var accessed = new List<int>();
      var enumerable = new IntegerEnumerable(0, 2, id => accessed.Add(id));
      var result = enumerable.Concat(new IntegerEnumerable(2, 2, id => accessed.Add(id)))
                             .Skip(2);

      Assert.AreEqual(0, accessed.Count);
    }

    [TestMethod]
    public void TestConcatWithIdEnumerableFollowedBySkipMaterialized()
    {
      var accessed = new List<int>();
      var enumerable = new IntegerEnumerable(0, 2, id => accessed.Add(id));
      var result = enumerable.Concat(new IntegerEnumerable(2, 2, id => accessed.Add(id)))
                             .Skip(2)
                             .ToArray();

      Assert.AreEqual(2, accessed.Count);
      Assert.AreEqual(2, accessed[0]);
      Assert.AreEqual(3, accessed[1]);

      Assert.AreEqual(2, result.Length);
      Assert.AreEqual(2, result[0]);
      Assert.AreEqual(3, result[1]);
    }

    [TestMethod]
    public void TestSkipFollowedByTake()
    {
      var accessed = new List<int>();
      var enumerable = new IntegerEnumerable(0, 6, id => accessed.Add(id));
      var result = enumerable.Skip(2)
                             .Take(2)
                             .ToArray();

      Assert.AreEqual(2, accessed.Count);
      Assert.AreEqual(2, accessed[0]);
      Assert.AreEqual(3, accessed[1]);

      Assert.AreEqual(2, result.Length);
      Assert.AreEqual(2, result[0]);
      Assert.AreEqual(3, result[1]);
    }

    [TestMethod]
    public void TestLast()
    {
      var accessed = new List<int>();
      var enumerable = new IntegerEnumerable(0, 3, id => accessed.Add(id));
      var result = enumerable.Last();

      Assert.AreEqual(1, accessed.Count);
      Assert.AreEqual(2, accessed[0]);

      Assert.AreEqual(2, result);
    }

    [TestMethod]
    public void TestConcatFollowedByLast()
    {
      var accessed = new List<int>();
      var result = new IntegerEnumerable(0, 2, id => accessed.Add(id))
                   .Concat(new IntegerEnumerable(2, 2, id => accessed.Add(id)))
                   .Last();

      Assert.AreEqual(1, accessed.Count);
      Assert.AreEqual(3, accessed[0]);

      Assert.AreEqual(3, result);
    }

    [TestMethod]
    public void TestConcatAndCount()
    {
      var accessed = new List<int>();
      var result = new IntegerEnumerable(0, 2, id => accessed.Add(id))
                   .Concat(new IntegerEnumerable(2, 2, id => accessed.Add(id)))
                   .Count();

      Assert.AreEqual(0, accessed.Count);
      Assert.AreEqual(4, result);
    }

    [TestMethod]
    public void TestConcatWithArrayAndCount()
    {
      var accessed = new List<int>();
      var result = new IntegerEnumerable(0, 2, id => accessed.Add(id))
                   .Concat(new[] { 2, 3 })
                   .Concat(new IntegerEnumerable(4, 2, id => accessed.Add(id)))
                   .Count();

      Assert.AreEqual(4, accessed.Count);
      Assert.AreEqual(6, result);
    }

    [TestMethod]
    public void TestSkipNotMaterialized()
    {
      var accessed = new List<int>();
      var enumerable = new IntegerEnumerable(0, 4, id => accessed.Add(id));
      enumerable.Skip(2);

      Assert.AreEqual(0, accessed.Count);
    }

    [TestMethod]
    public void TestSkipMaterialized()
    {
      var accessed = new List<int>();
      var enumerable = new IntegerEnumerable(0, 4, id => accessed.Add(id));
      var result = enumerable.Skip(2)
                             .ToArray();

      Assert.AreEqual(2, accessed.Count);
      Assert.AreEqual(2, accessed[0]);
      Assert.AreEqual(3, accessed[1]);

      Assert.AreEqual(2, result.Length);
      Assert.AreEqual(2, result[0]);
      Assert.AreEqual(3, result[1]);
    }

    [DebuggerStepThrough]
    private class IntegerEnumerable : LazyIdEnumerable<int>
    {
      public IntegerEnumerable(int startIndex, int count, Action<int> accessed)
        : base(Enumerable.Range(startIndex, count).Select(id => (object)id),
               obj => { var id = (int)obj; accessed(id); return id; })
      {
      }
    }
  }
}
