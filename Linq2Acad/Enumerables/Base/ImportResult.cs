using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2Acad
{
  public class ImportResult
  {
    public ImportResult(BlockTableRecord block, IdMapping mapping)
    {
      Block = block;
      Mapping = mapping.Cast<IdPair>()
                       .ToDictionary(p => p.Key, p => p.Value);
    }

    public BlockTableRecord Block { get; private set; }

    public Dictionary<ObjectId, ObjectId> Mapping { get; private set; }
  }
}
