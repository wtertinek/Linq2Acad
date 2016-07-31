using System;
using System.Linq;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using AcadTestRunner;

namespace Linq2Acad.Tests
{
  public class BlockContainerTests
  {
    [CommandMethod("TestCreateBlock")]
    public void TestCreateBlock()
    {
      var notifier = new Notification("TestCreateBlock");

      try
      {
        using (var db = AcadDatabase.Active())
        {
          var newBlock = db.Blocks.Create("NewBlock");
          
          var ok = Check.Table(db.Database, table => table.Has("NewBlock"));
          if (!ok) { notifier.TestFailed("BlockTable does not contain an element with name 'NewBlock'"); return; }

          ok = Check.DictionaryIDs(db.Database, ids => ids.Any(id => id == newBlock.ObjectId));
          if (!ok) { notifier.TestFailed("BlockTable does not contain the newly created element"); return; }
        }
      }
      catch (System.Exception e)
      {
        notifier.TestFailed(e);
      }

      notifier.TestPassed();
    }

    [CommandMethod("TestAddBlock")]
    public void TestAddBlock()
    {
      var notifier = new Notification("TestAddBlock");

      try
      {
        using (var db = AcadDatabase.Active())
        {
          var newElement = new BlockTableRecord() { Name = "NewBlock" };
          db.Blocks.Add(newElement);
          
          var ok = Check.Table(db.Database, table => table.Has("NewBlock"));
          if (!ok) { notifier.TestFailed("BlockTable does not contain an element with name 'NewBlock'"); return; }
        }
      }
      catch (System.Exception e)
      {
        notifier.TestFailed(e);
      }
    
      notifier.TestPassed();
    }
  }
}
