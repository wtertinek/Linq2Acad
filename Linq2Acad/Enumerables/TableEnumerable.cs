using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;

namespace Linq2Acad
{
  class TableEnumerable<T> : IEnumerable<T>, IAcadEnumerable where T : DBObject
  {
    private string acDbType;

    public TableEnumerable(Lazy<Transaction> transaction, ObjectId containerID, bool filter = false)
    {
      Transaction = transaction;
      ContainerID = containerID;

      if (filter)
      {
        acDbType = "AcDb" + typeof(T).Name;
      }
    }

    public Lazy<Transaction> Transaction { get; private set; }

    public ObjectId ContainerID { get; private set; }

    public int Count
    {
      get
      {
        var count = 0;
        var iterator = Iterate(id => null);

        while (iterator.MoveNext())
        {
          count++;
        }

        return count;
      }
    }

    public IEnumerator<T> GetEnumerator()
    {
      return Iterate(id => (T)Transaction.Value.GetObject(id, OpenMode.ForRead));
    }

    private IEnumerator<T> Iterate(Func<ObjectId, T> getItem)
    {
      var container = (IEnumerable)Transaction.Value.GetObject(ContainerID, OpenMode.ForRead);
      var idEnumerator = container.GetEnumerator();

      if (acDbType != null)
      {
        while (idEnumerator.MoveNext())
        {
          var id = (ObjectId)idEnumerator.Current;

          if (id.ObjectClass.Name != acDbType)
          {
            continue;
          }

          yield return getItem(id);
        }
      }
      else
      {
        while (idEnumerator.MoveNext())
        {
          yield return getItem((ObjectId)idEnumerator.Current);
        }
      }
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }
  }
}
