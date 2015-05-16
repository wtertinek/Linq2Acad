using Autodesk.AutoCAD.ApplicationServices.Core;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
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

    [CommandMethod("TestAddLayer")]
    public static void TestAddLayer()
    {
      using (var db = new ActiveDatabase(Database))
      {
        db.Layers
          .Add(new LayerTableRecord() { Name = "TestLayer" });
      }
    }

    [CommandMethod("TestGetLayer0")]
    public static void TestGetLayer0()
    {
      using (var db = new ActiveDatabase(Database))
      {
        if (db.Layers.Contains("0"))
        {
          var name = db.Layers
                       .GetItem("0").Name;
          Debug.Assert(name == "0");
        }
      }
    }

    [CommandMethod("TestAddLine")]
    public static void TestAddLine()
    {
      using (var db = new ActiveDatabase(Database))
      {
        db.ModelSpace
          .Add(new Line(new Point3d(5, 5, 0),
                        new Point3d(12, 3, 0)));

        db.ModelSpace
          .AddRange(new [] { new Line(new Point3d(5, 5, 0),
                                      new Point3d(12, 3, 0)),
                             new Line(new Point3d(500, 500, 0),
                                      new Point3d(1200, 300, 0)) });
      }
    }

    [CommandMethod("TestAddGroup")]
    public static void TestAddGroup()
    {
      using (var db = new ActiveDatabase(Database))
      {
        if (db.Groups.Contains("Group1"))
        {
          Editor.WriteMessage("Group1 already exists");
        }
        else
        {
          db.Groups.Add("Group1", new Group("This is Group 1", true));
        }
      }
    }

    [CommandMethod("TestTurnOffLayers")]
    public static void TestTurnOffLayers()
    {
      using (var db = new ActiveDatabase(Database))
      {
        var result = Editor.GetEntity("Select an entity");

        if (result.Status == PromptStatus.OK)
        {
          var layerID = db.Database
                          .GetObject<Entity>(result.ObjectId)
                          .LayerId;
          db.Layers
            .Where(l => l.Id != layerID)
            .ForEach(l => l.IsOff = true);
        }
      }
    }
  }
}
