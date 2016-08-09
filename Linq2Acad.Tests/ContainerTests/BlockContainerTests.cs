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
      var newId = ObjectId.Null;

      using (var db = AcadDatabase.Active())
      {
        var newBlock = db.Blocks.Create("NewBlock");
        newId = newBlock.ObjectId;
      }

      Assert.That.BlockTable.Contains("NewBlock");
      Assert.That.BlockTable.Contains(newId);
    }

    [AcadTest("AddBlock")]
    public void AddBlock()
    {
      var newId = ObjectId.Null;

      using (var db = AcadDatabase.Active())
      {
        var newBlock = new BlockTableRecord() { Name = "NewBlock" };
        db.Blocks.Add(newBlock);
        newId = newBlock.ObjectId;
      }

      Assert.That.BlockTable.Contains(newId);
    }
  }
}
