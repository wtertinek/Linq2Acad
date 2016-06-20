using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2Acad
{
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
}
