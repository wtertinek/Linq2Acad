using Autodesk.AutoCAD.ApplicationServices.Core;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.LayerManager;
using Autodesk.AutoCAD.Runtime;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Linq2Acad.Tests
{
  public static class Tests
  {
    private static Editor Editor
    {
      get { return Application.DocumentManager.MdiActiveDocument.Editor; }
    }

    [CommandMethod("TestPrintMLeaderStyles")]
    public static void TestPrintMLeaderStyles()
    {
      using (var db = L2ADatabase.Active())
      {
        db.MLeaderStyles
          .ForEach(m => Editor.WriteLine("" + m.Name));
      }
    }

    [CommandMethod("TestPrintMaterials")]
    public static void TestPrintMaterials()
    {
      using (var db = L2ADatabase.Active())
      {
        db.Materials
          .ForEach(m => Editor.WriteLine("" + m.Name));
      }
    }

    [CommandMethod("TestPrintLayouts")]
    public static void TestPrintLayouts()
    {
      using (var db = L2ADatabase.Active())
      {
        db.Layouts
          .ForEach(l => Editor.WriteLine("" + l.LayoutName));
      }
    }

    [CommandMethod("TestPrintBlockNames")]
    public static void TestPrintBlockNames()
    {
      using (var db = L2ADatabase.Active())
      {
        db.Blocks
          .ForEach(b => Editor.WriteLine("" + b.Name));
      }
    }

    [CommandMethod("TestPrintCurrentViewport")]
    public static void TestPrintCurrentViewport()
    {
      using (var db = L2ADatabase.Active())
      {
        Editor.WriteLine("" + db.Viewports.Current.Name);
      }
    }

    [CommandMethod("TestDeleteBlockReferences")]
    public static void TestDeleteBlockReferences()
    {
      using (var db = L2ADatabase.Active())
      {
        db.ModelSpace
          .OfType<BlockReference>()
          .ForEach(br => br.Erase());
      }
    }

    [CommandMethod("TestCurrentVsModelSpace")]
    public static void TestCurrentVsModelSpace()
    {
      using (var db = L2ADatabase.Active())
      {
        var names1 = db.ModelSpace
                       .OfType<BlockReference>()
                       .Select(br => br.Name)
                       .ToArray();

        var names2 = db.CurrentSpace
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

    [CommandMethod("TestPrintLayers")]
    public static void TestPrintLayers()
    {
      using (var db = L2ADatabase.Active())
      {
        db.Layers
          .ForEach(l => Editor.WriteLine("" + l.Name));
      }
    }

    [CommandMethod("TestCreateNewLayer")]
    public static void TestCreateNewLayer()
    {
      using (var db = L2ADatabase.Active())
      {
        db.Layers
          .Create("TestLayer");
      }
    }

    [CommandMethod("TestGetLayer0")]
    public static void TestGetLayer0()
    {
      using (var db = L2ADatabase.Active())
      {
        if (db.Layers.Contains("0"))
        {
          var name = db.Layers["0"].Name;
          Debug.Assert(name == "0");
        }
      }
    }

    [CommandMethod("TestAddLine")]
    public static void TestAddLine()
    {
      using (var db = L2ADatabase.Active())
      {
        db.ModelSpace
          .Add(new Line(new Point3d(5, 5, 0),
                        new Point3d(12, 3, 0)));

        db.ModelSpace
          .Add(new [] { new Line(new Point3d(5, 5, 0),
                                 new Point3d(12, 3, 0)),
                        new Line(new Point3d(500, 500, 0),
                                 new Point3d(1200, 300, 0)) });
      }
    }

    [CommandMethod("TestCreateGroup")]
    public static void TestCreateGroup()
    {
      using (var db = L2ADatabase.Active())
      {
        if (db.Groups.Contains("LineGroup"))
        {
          Editor.WriteLine("LineGroup already exists");
        }
        else
        {
          var lines = db.ModelSpace
                        .OfType<Line>();
          db.Groups
            .Create("LineGroup", lines);
        }
      }
    }

    [CommandMethod("TestTurnOffLayers")]
    public static void TestTurnOffLayers()
    {
      using (var db = L2ADatabase.Active())
      {
        var result = Editor.GetEntity("Pick an entity");

        if (result.Status == PromptStatus.OK)
        {
          var layerID = db.Item<Entity>(result.ObjectId)
                          .LayerId;
          db.Layers
            .Where(l => l.Id != layerID)
            .ForEach(l => l.IsOff = true);
        }
      }
    }

    [CommandMethod("TestMoveEntitiesToDifferentLayer")]
    public static void TestMoveEntitiesToDifferentLayer()
    {
      using (var db = L2ADatabase.Active())
      {
        var result = Editor.GetString("Enter source layer name:",
                                      s => db.Layers.Contains(s));

        if (result.Status == PromptStatus.OK)
        {
          var sourceLayerID = db.Layers[result.StringResult]
                                .ObjectId;

          result = Editor.GetString("Enter target layer name:",
                                    s => db.Layers.Contains(s));

          if (result.Status == PromptStatus.OK)
          {
            var targetLayerID = db.Layers[result.StringResult]
                                  .ObjectId;

            db.ModelSpace
              .Where(l => l.LayerId == targetLayerID)
              .ForEach(l => l.LayerId = sourceLayerID);
          }
        }
      }
    }

    [CommandMethod("TestDeleteLines")]
    public static void TestDeleteLines()
    {
      using (var db = L2ADatabase.Active())
      {
        db.ModelSpace
          .OfType<Line>()
          .ForEach(br => br.Erase());
      }
    }

    [CommandMethod("TestCountBlockReferences")]
    public static void TestCountBlockReferences()
    {
      using (var db = L2ADatabase.Active())
      {
        int count = db.ModelSpace
                      .OfType<BlockReference>()
                      .Count();

        Editor.WriteLine("" + count + " BlockReferences");
      }
    }

    [CommandMethod("TestReadFromFile")]
    public static void TestReadFromFile()
    {
      var result = Editor.GetString("Enter file path:",
                                    s => File.Exists(s));

      if (result.Status == PromptStatus.OK)
      {
        using (var db = L2ADatabase.Open(result.StringResult))
        {
          var count = db.ModelSpace
                        .OfType<BlockReference>()
                        .Count();

          Editor.WriteLine("Model space BlockReferences in file " + result.StringResult + ": " + count);
        }
      }
    }

    [CommandMethod("TestImportBlock")]
    public static void TestImportBlock()
    {
      var result = Editor.GetString("Enter file path:",
                                    s => File.Exists(s));
      
      if (result.Status == PromptStatus.OK)
      {
        using (var sourceDB = L2ADatabase.Open(result.StringResult))
        {
          result = Editor.GetString("Enter block name:",
                                    s => sourceDB.Blocks.Contains(s));

          if (result.Status == PromptStatus.OK)
          {
            var block = sourceDB.Blocks[result.StringResult];

            using (var activeDB = L2ADatabase.Active())
            {
              activeDB.Blocks
                      .Import(block, true);
            }

            Editor.WriteLine("Block " + result.StringResult + " successfully imported");
          }
        }
      }
    }

    [CommandMethod("TestSaveData")]
    public static void TestSaveData()
    {
      using (var db = L2ADatabase.Active())
      {
        var result = Editor.GetEntity("Pick an entity:");

        if (result.Status == PromptStatus.OK)
        {
          var result2 = Editor.GetString("Enter key:");

          if (result2.Status == PromptStatus.OK)
          {
              db.ModelSpace
                .Item(result.ObjectId)
                .SaveData(result2.StringResult, new [] { 1, 2 });
          }
        }
      }
    }

    [CommandMethod("TestGetData")]
    public static void TestGetData()
    {
      using (var db = L2ADatabase.Active())
      {
        var result = Editor.GetEntity("Pick an entity:");

        if (result.Status == PromptStatus.OK)
        {
          var result2 = Editor.GetString("Enter key:");

          if (result2.Status == PromptStatus.OK)
          {
            var value = db.ModelSpace
                          .Item(result.ObjectId)
                          .GetData<int[]>(result2.StringResult);

            Editor.WriteLine(result2.StringResult + ": " + value[0], ", " + value[1]);
          }
        }
      }
    }
  }
}
