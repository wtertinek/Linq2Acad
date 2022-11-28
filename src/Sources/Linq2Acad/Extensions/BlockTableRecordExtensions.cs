using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2Acad
{
  /// <summary>
  /// Extension methods for instances of LayerTableRecord.
  /// </summary>
  public static class BlockTableRecordExtensions
  {
    
    public static BlockReference NewReference(this BlockTableRecord blockTableRecord, Point3d insertionPoint)
    {
            BlockReference blockReference = new BlockReference(insertionPoint, blockTableRecord.Id);
            return blockReference;
    }
  }
}
