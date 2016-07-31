using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;

namespace Linq2Acad.Tests
{
  [TestClass]
  public class IdEnumerableTests
  {
    [TestMethod]
    public void TestConcat()
    {
      var enumerated = new List<object>();
      var result = new LazayIdEnumerable<int>(new IntegerEnumerable(0, 2), id => { enumerated.Add(id); return (int)id; })
                   .Concat(new LazayIdEnumerable<int>(new IntegerEnumerable(2, 2), id => { enumerated.Add(id); return (int)id; }))
                   .ToArray();

      Assert.AreEqual(4, enumerated.Count);
      Assert.AreEqual(0, result[0]);
      Assert.AreEqual(1, result[1]);
      Assert.AreEqual(2, result[2]);
      Assert.AreEqual(3, result[3]);
    }

    [TestMethod]
    public void TestCountAfterConcat()
    {
      var enumerated = new List<object>();
      var result = new LazayIdEnumerable<int>(new IntegerEnumerable(0, 2), id => { enumerated.Add(id); return (int)id; })
                   .Concat(new LazayIdEnumerable<int>(new IntegerEnumerable(2, 2), id => { enumerated.Add(id); return (int)id; }))
                   .Count();

      Assert.AreEqual(0, enumerated.Count);
      Assert.AreEqual(4, result);
    }

    [TestMethod]
    public void TestCount()
    {
      var enumerated = new List<object>();
      var enumerable = new LazayIdEnumerable<int>(new IntegerEnumerable(0, 3), id => { enumerated.Add(id); return (int)id; });
      var result = enumerable.Count();

      Assert.AreEqual(0, enumerated.Count);
      Assert.AreEqual(3, result);
    }

    [TestMethod]
    public void TestElementAt()
    {
      var enumerated = new List<object>();
      var enumerable = new LazayIdEnumerable<int>(new IntegerEnumerable(0, 3), id => { enumerated.Add(id); return (int)id; });
      var result = enumerable.ElementAt(1);

      Assert.AreEqual(1, enumerated.Count);
      Assert.AreEqual(1, result);
    }

    [TestMethod]
    public void TestElementAtOrDefault()
    {
      var enumerated = new List<object>();
      var enumerable = new LazayIdEnumerable<int>(new IntegerEnumerable(0, 3), id => { enumerated.Add(id); return (int)id; });
      var result = enumerable.ElementAtOrDefault(1);

      Assert.AreEqual(1, enumerated.Count);
      Assert.AreEqual(1, result);
    }

    [TestMethod]
    public void TestLast()
    {
      var enumerated = new List<object>();
      var enumerable = new LazayIdEnumerable<int>(new IntegerEnumerable(0, 3), id => { enumerated.Add(id); return (int)id; });
      var result = enumerable.Last();

      Assert.AreEqual(1, enumerated.Count);
      Assert.AreEqual(2, result);
    }

    [TestMethod]
    public void TestLastOrDefault()
    {
      var enumerated = new List<object>();
      var enumerable = new LazayIdEnumerable<int>(new IntegerEnumerable(0, 3), id => { enumerated.Add(id); return (int)id; });
      var result = enumerable.LastOrDefault();

      Assert.AreEqual(1, enumerated.Count);
      Assert.AreEqual(2, result);
    }

    [TestMethod]
    public void TestLongCount()
    {
      var enumerated = new List<object>();
      var enumerable = new LazayIdEnumerable<int>(new IntegerEnumerable(0, 3), id => { enumerated.Add(id); return (int)id; });
      var result = enumerable.LongCount();

      Assert.AreEqual(0, enumerated.Count);
      Assert.AreEqual(3, result);
    }

    [TestMethod]
    public void TestSkip()
    {
      var enumerated = new List<object>();
      var enumerable = new LazayIdEnumerable<int>(new IntegerEnumerable(0, 3), id => { enumerated.Add(id); return (int)id; });
      var result = enumerable.Skip(2)
                             .ToArray();

      Assert.AreEqual(1, enumerated.Count);
      Assert.AreEqual(1, result.Length);
      Assert.AreEqual(2, result[0]);
    }
  }
}
