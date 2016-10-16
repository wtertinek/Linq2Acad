using System;
using System.Linq;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using AcadTestRunner;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linq2Acad.Tests
{
  public class BlockContainerTests
  {
    [AcadTest]
    public void TestCreateBlock()
    {
      var newId = ObjectId.Null;

      using (var db = AcadDatabase.Active())
      {
        var newBlock = db.Blocks.Create("NewBlock");
        newId = newBlock.ObjectId;
      }

      AcadAssert.That.BlockTable.Contains("NewBlock");
      AcadAssert.That.BlockTable.Contains(newId);
    }

    [AcadTest]
    public void TestAddBlock()
    {
      var newId = ObjectId.Null;

      using (var db = AcadDatabase.Active())
      {
        var newBlock = new BlockTableRecord() { Name = "NewBlock" };
        db.Blocks.Add(newBlock);
        newId = newBlock.ObjectId;
      }

      AcadAssert.That.BlockTable.Contains(newId);
    }
  }
}
