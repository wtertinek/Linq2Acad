using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Runtime;

namespace Linq2Acad
{
  public class Examples
  {
    /// <summary>
    /// Adding a line to the model space
    /// </summary>
    [CommandMethod("AddingALineToTheModelSpace")]
    public void AddingALineToTheModelSpace()
    {
      using (var db = AcadDatabase.Active())
      {
        db.ModelSpace
          .Add(new Line(new Point3d(5, 5, 0),
                        new Point3d(12, 3, 0)));
      }
    }

    /// <summary>
    /// Erasing all BlockReferences from the model space
    /// </summary>
    [CommandMethod("ErasingAllBlockReferencesFromTheModelSpace")]
    public void ErasingAllBlockReferencesFromTheModelSpace()
    {
      using (var db = AcadDatabase.Active())
      {
        db.ModelSpace
          .OfType<BlockReference>()
          .ForEach(br => br.Erase());
      }
    }

    /// <summary>
    /// Printing all layer names
    /// </summary>
    [CommandMethod("PrintingAllLayerNames")]
    public void PrintingAllLayerNames()
    {
      var editor = Application.DocumentManager.MdiActiveDocument.Editor;

      using (var db = AcadDatabase.Active())
      {
        db.Layers
          .ForEach(l => editor.WriteLine(l.Name));
      }
    }

    /// <summary>
    /// Turning off all layers, except the one the user enters
    /// </summary>
    [CommandMethod("TurningOffAllLayersExceptTheOneTheUserEnters")]
    public void TurningOffAllLayersExceptTheOneTheUserEnters()
    {
      var editor = Application.DocumentManager.MdiActiveDocument.Editor;

      using (var db = AcadDatabase.Active())
      {
        var result = editor.GetString("Enter layer name",
                                      s => db.Layers.Contains(s));

        if (result.Status == PromptStatus.OK)
        {
          var layer = db.Layers.Element(result.StringResult);

          db.Layers
            .Except(new[] { layer })
            .ForEach(l => l.IsOff = true);
        }
      }
    }

    /// <summary>
    /// Moving entities from one layer to another
    /// </summary>
    [CommandMethod("MovingEntitiesFromOneLayerToAnother")]
    public void MovingEntitiesFromOneLayerToAnother()
    {
      var editor = Application.DocumentManager.MdiActiveDocument.Editor;

      using (var db = AcadDatabase.Active())
      {
        var result = editor.GetString("Enter source layer name:",
                                      s => db.Layers.Contains(s));

        if (result.Status == PromptStatus.OK)
        {
          var sourceLayer = db.Layers
                              .Element(result.StringResult);

          result = editor.GetString("Enter target layer name:",
                                    s => db.Layers.Contains(s));

          if (result.Status == PromptStatus.OK)
          {
            var entities = db.ModelSpace
                              .Where(e => e.Layer == result.StringResult);
            db.Layers
              .Element(result.StringResult)
              .AddRange(entities);
          }
        }
      }
    }

    /// <summary>
    /// Importing a block from a drawing file
    /// </summary>
    [CommandMethod("ImportingABlockFromADrawingFile")]
    public void ImportingABlockFromADrawingFile()
    {
      var editor = Application.DocumentManager.MdiActiveDocument.Editor;
      var result = editor.GetString("Enter file path:", s => File.Exists(s));

      if (result.Status == PromptStatus.OK)
      {
        using (var sourceDB = AcadDatabase.Open(result.StringResult))
        {
          result = editor.GetString("Enter block name:",
                                    s => sourceDB.Blocks.Contains(s));

          if (result.Status == PromptStatus.OK)
          {
            var block = sourceDB.Blocks
                                .Element(result.StringResult);

            using (var activeDB = AcadDatabase.Active())
            {
              activeDB.Blocks
                      .Import(block, true);
            }

            editor.WriteLine("Block " + result.StringResult + " successfully imported");
          }
        }
      }
    }

    /// <summary>
    /// Opening a drawing from file and counting the BlockReferences in the model space
    /// </summary>
    [CommandMethod("OpeningADrawingFromFileAndCountingTheBlockReferencesInTheModelSpace")]
    public void OpeningADrawingFromFileAndCountingTheBlockReferencesInTheModelSpace()
    {
      var editor = Application.DocumentManager.MdiActiveDocument.Editor;
      var result = editor.GetString("Enter file path:", s => File.Exists(s));

      if (result.Status == PromptStatus.OK)
      {
        using (var db = AcadDatabase.Open(result.StringResult))
        {
          var count = db.ModelSpace
                        .OfType<BlockReference>()
                        .Count();

          editor.WriteLine("Model space BlockReferences in file " + result.StringResult + ": " + count);
        }
      }
    }

    /// <summary>
    /// Picking an entity and saving a string on it
    /// </summary>
    [CommandMethod("PickingAnEntityAndSavingAStringOnIt")]
    public void PickingAnEntityAndSavingAStringOnIt()
    {
      var editor = Application.DocumentManager.MdiActiveDocument.Editor;

      var result1 = editor.GetEntity("Pick an entity:");

      if (result1.Status == PromptStatus.OK)
      {
        var result2 = editor.GetString("Enter key:");

        if (result2.Status == PromptStatus.OK)
        {
          var result3 = editor.GetString("Enter text to save:");

          if (result3.Status == PromptStatus.OK)
          {
            var entityID = result1.ObjectId;
            var key = result2.StringResult;
            var value = result3.StringResult;

            using (var db = AcadDatabase.Active())
            {
              db.ModelSpace
                .Element(entityID)
                .SaveData(key, value);
            }
          }
        }
      }
    }

    /// <summary>
    /// Picking an entity and reading a string from it
    /// </summary>
    [CommandMethod("PickingAnEntityAndReadingAStringFromIt")]
    public void PickingAnEntityAndReadingAStringFromIt()
    {
      var editor = Application.DocumentManager.MdiActiveDocument.Editor;

      var result1 = editor.GetEntity("Pick an entity:");

      if (result1.Status == PromptStatus.OK)
      {
        var result2 = editor.GetString("Enter key:");

        if (result2.Status == PromptStatus.OK)
        {
          var entityID = result1.ObjectId;
          var key = result2.StringResult;

          using (var db = AcadDatabase.Active())
          {
            var value = db.ModelSpace
                          .Element(entityID)
                          .GetData<string>(key);

            editor.WriteLine("Value: " + value);
          }
        }
      }
    }

    /// <summary>
    /// Creating a group and adding all lines in the model space to it
    /// </summary>
    [CommandMethod("CreatingAGroupAndAddingAllLinesInTheModelSpaceToIt")]
    public void CreatingAGroupAndAddingAllLinesInTheModelSpaceToIt()
    {
      var editor = Application.DocumentManager.MdiActiveDocument.Editor;

      using (var db = AcadDatabase.Active())
      {
        if (db.Groups.Contains("LineGroup"))
        {
          editor.WriteLine("LineGroup already exists");
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
  }
}
