using System;
using Linq2Acad;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;

namespace Linq2Acad.Tests
{
  ///////////////////////////////////////////
  // This code is automatically generated ///
  ///////////////////////////////////////////
  public class GroupTests
  {
    [CommandMethod("TestCreateGroup")]
    public void TestCreateGroup()
    {
      using (var db = AcadDatabase.Active())
      {
        var newGroup = db.Groups.Create("NewGroup");
        var ok = Assert.Dictionary(db.Database, dict => dict.Contains("NewGroup"));
        
        Console.WriteLine("Test result TestCreateGroup: " + (ok ? "PASSED" : "FAILED"));
      }
    }
  }
}
