using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2Acad
{
  public class ImportResult<T> where T : DBObject
  {
    public ImportResult(T item, IdMapping mapping)
    {
      Item = item;
      Mapping = mapping.Cast<IdPair>()
                       .ToDictionary(p => p.Key, p => p.Value);
    }

    public T Item { get; private set; }

    public Dictionary<ObjectId, ObjectId> Mapping { get; private set; }
  }
}
