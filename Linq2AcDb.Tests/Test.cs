using Autodesk.AutoCAD.ApplicationServices.Core;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Linq2AcDb.AcadTest
{
  public static class Test
  {
    private static Database Database
    {
      get { return Application.DocumentManager.MdiActiveDocument.Database; }
    }

    private static Editor Editor
    {
      get { return Application.DocumentManager.MdiActiveDocument.Editor; }
    }

    [CommandMethod("TestMLeaderStyles")]
    public static void TestMLeaderStyles()
    {
      using (var db = new ActiveDatabase(Database))
      {
        var names = db.MLeaderStyles
                      .Select(m => m.Name)
                      .ToArray();
      }
    }

    [CommandMethod("TestMaterials")]
    public static void TestMaterials()
    {
      using (var db = new ActiveDatabase(Database))
      {
        var names = db.Materials
                      .Select(m => m.Name)
                      .ToArray();
      }
    }

    [CommandMethod("TestLayouts")]
    public static void TestLayouts()
    {
      using (var db = new ActiveDatabase(Database))
      {
        var names = db.Layouts
                      .Select(l => l.LayoutName)
                      .ToArray();
      }
    }

    [CommandMethod("TestCurrentViewport")]
    public static void TestCurrentViewport()
    {
      using (var db = new ActiveDatabase(Database))
      {
        var name = db.CurrentViewport
                     .Name;
      }
    }

    [CommandMethod("TestDeleteBlockReferences")]
    public static void TestDeleteBlockReferences()
    {
      var start = DateTime.Now;

      using (var db = new ActiveDatabase(Database))
      {
        db.ModelSpace
          .Items()
          .OfType<BlockReference>()
          .UpgradeOpen()
          .ForEach(br => br.Erase());
      }

      Editor.WriteMessage("Done in " + (DateTime.Now - start).TotalMilliseconds + "ms");
    }

    [CommandMethod("TestCurrentVsModelSpace")]
    public static void CurrentVsModelSpace()
    {
      using (var db = new ActiveDatabase(Database))
      {
        var names1 = db.ModelSpace
                       .Items()
                       .OfType<BlockReference>()
                       .Select(br => br.Name)
                       .ToArray();

        var names2 = db.CurrentSpace
                       .Items()
                       .OfType<BlockReference>()
                       .Select(br => br.Name)
                       .ToArray();

        Debug.Assert(names1.Length == names2.Length);

        for (int i = 0; i < names1.Length; i++)
        {
          Debug.Assert(names1[i] == names2[i]);
        }
      }
    }

    [CommandMethod("TestLayers")]
    public static void TestLayers()
    {
      using (var db = new ActiveDatabase(Database))
      {
        var names = db.Layers
                      .Select(l => l.Name)
                      .ToArray();
      }
    }
  }
}
