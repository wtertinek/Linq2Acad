using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;

namespace Linq2Acad
{
  class DbDictionaryEnumerable<T> : EnumerableBase<T> where T : DBObject
  {
    public DbDictionaryEnumerable(Lazy<Transaction> transaction, ObjectId containerID)
      : base(transaction, containerID)
    {
    }

    public override int Count
    {
      get { return ((DBDictionary)transaction.Value.GetObject(ContainerID, OpenMode.ForRead)).Count; }
    }

    public override IEnumerator<T> GetEnumerator()
    {
      var dict = (DBDictionary)transaction.Value.GetObject(ContainerID, OpenMode.ForRead);

      foreach (var entry in dict)
      {
        yield return (T)transaction.Value.GetObject((ObjectId)entry.Value, OpenMode.ForRead);
      }
    }
  }
}
