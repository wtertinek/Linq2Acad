using System;
using System.Linq;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  public partial class BlockContainerTests
  {
    [AcadTest("CreateBlock")]
    public void CreateBlock()
    {
      using (var db = AcadDatabase.Active())
      {
        var newBlock = db.Blocks.Create("NewBlock");

        Assert.Table(db.Database, db.Database.BlockTableId, table => table.Has("NewBlock"),
                     "BlockTable does not contain an element with name 'NewBlock'");
        Assert.TableIDs(db.Database, db.Database.BlockTableId, ids => ids.Any(id => id == newBlock.ObjectId),
                        "BlockTable does not contain the newly created element");
      }
    }

    [AcadTest("AddBlock")]
    public void AddBlock()
    {
      using (var db = AcadDatabase.Active())
      {
        var newElement = new BlockTableRecord() { Name = "NewBlock" };
        db.Blocks.Add(newElement);
          
        Assert.Table(db.Database, db.Database.BlockTableId, table => table.Has("NewBlock"),
                     "BlockTable does not contain an element with name 'NewBlock'");
      }
    }
  }
}
