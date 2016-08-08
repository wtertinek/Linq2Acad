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

        Assert.That().BlockTable().Contains("NewBlock");
        //Assert.That().BlockTable().Contains(newBlock.ObjectId);
      }
    }

    [AcadTest("AddBlock")]
    public void AddBlock()
    {
      using (var db = AcadDatabase.Active())
      {
        var newElement = new BlockTableRecord() { Name = "NewBlock" };
        db.Blocks.Add(newElement);

        Assert.That().BlockTable().Contains("NewBlock");
      }
    }
  }
}
