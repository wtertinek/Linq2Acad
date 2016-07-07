using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Runtime;

namespace Linq2Acad
{
  public partial class Examples
  {
    /// <summary>
    /// Removing all entities from the model space
    /// </summary>
    [CommandMethod("Linq2AcadExample1")]
    public void RemovingAllEntitiesFromTheModelSpace()
    {
      using (var db = AcadDatabase.Active())
      {
        db.ModelSpace
          .Clear();
      }

      WriteMessage("Model space cleared");
    }

    /// <summary>
    /// Erasing all BlockReferences from the model space
    /// </summary>
    [CommandMethod("Linq2AcadExample2")]
    public void ErasingAllBlockReferencesFromTheModelSpace()
    {
      using (var db = AcadDatabase.Active())
      {
        db.ModelSpace
          .OfType<BlockReference>()
          .ForEach(br => br.Erase());
      }

      WriteMessage("All block references removed from model space");
    }

    /// <summary>
    /// Adding a line to the model space
    /// </summary>
    [CommandMethod("Linq2AcadExample3")]
    public void AddingALineToTheModelSpace()
    {
      using (var db = AcadDatabase.Active())
      {
        db.ModelSpace
          .Add(new Line(new Point3d(0, 0, 0),
                        new Point3d(100, 100, 0)));
      }

      WriteMessage("Line added to model space");
    }

    /// <summary>
    /// Printing all layer names
    /// </summary>
    [CommandMethod("Linq2AcadExample4")]
    public void PrintingAllLayerNames()
    {
      using (var db = AcadDatabase.Active())
      {
        db.Layers
          .ForEach(l => WriteMessage(l.Name));
      }
    }

    /// <summary>
    /// Turning off all layers, except the one the user enters
    /// </summary>
    [CommandMethod("Linq2AcadExample5")]
    public void TurningOffAllLayersExceptTheOneTheUserEnters()
    {
      var layerName = GetString("Enter layer name");

      using (var db = AcadDatabase.Active())
      {
        var layer = db.Layers
                      .Element(layerName);

        db.Layers
          .Except(new[] { layer })
          .ForEach(l => l.IsOff = true);
      }

      WriteMessage("All layers (except " + layerName + ") turned off");
    }

    /// <summary>
    /// Moving entities from one layer to another
    /// </summary>
    [CommandMethod("Linq2AcadExample6")]
    public void MovingEntitiesFromOneLayerToAnother()
    {
      var sourceLayerName = GetString("Enter source layer name");
      var targetLayerName = GetString("Enter target layer name");

      using (var db = AcadDatabase.Active())
      {
        var entities = db.CurrentSpace
                         .Where(e => e.Layer == sourceLayerName);
        db.Layers
          .Element(targetLayerName)
          .AddRange(entities);
      }

      WriteMessage("All entities on layer " + sourceLayerName + " moved to layer " + targetLayerName);
    }

    /// <summary>
    /// Importing a block from a drawing file
    /// </summary>
    [CommandMethod("Linq2AcadExample7")]
    public void ImportingABlockFromADrawingFile()
    {
      var filePath = GetString("Enter file path");
      var blockName = GetString("Enter block name");

      using (var sourceDb = AcadDatabase.Open(filePath))
      {
        var block = sourceDb.Blocks
                            .Element(blockName);

        using (var activeDb = AcadDatabase.Active())
        {
          activeDb.Blocks
                  .Import(block);
        }
      }

      WriteMessage("Block " + blockName + " imported");
    }

    /// <summary>
    /// Opening a drawing from file and counting the BlockReferences in the model space
    /// </summary>
    [CommandMethod("Linq2AcadExample8")]
    public void OpeningADrawingFromFileAndCountingTheBlockReferencesInTheModelSpace()
    {
      var filePath = GetString("Enter file path");

      using (var db = AcadDatabase.Open(filePath))
      {
        var count = db.ModelSpace
                      .OfType<BlockReference>()
                      .Count();

        WriteMessage("Model space block references in file " + filePath + ": " + count);
      }
    }

    /// <summary>
    /// Picking an entity and saving a string on it
    /// </summary>
    [CommandMethod("Linq2AcadExample9")]
    public void PickingAnEntityAndSavingAStringOnIt()
    {
      var entityId = GetEntity("Pick an entity");
      var key = GetString("Enter key");
      var str = GetString("Enter string to save");

      using (var db = AcadDatabase.Active())
      {
        db.CurrentSpace
          .Element(entityId)
          .SaveData(key, str);
      }

      WriteMessage("Key-value-pair " + key + ":" + str + " saved on entity");
    }

    /// <summary>
    /// Picking an entity and reading a string from it
    /// </summary>
    [CommandMethod("Linq2AcadExample10")]
    public void PickingAnEntityAndReadingAStringFromIt()
    {
      var entityId = GetEntity("Pick an entity");
      var key = GetString("Enter key");

      using (var db = AcadDatabase.Active())
      {
        var str = db.CurrentSpace
                       .Element(entityId)
                       .GetData<string>(key);

        WriteMessage("String " + str + " read from entity");
      }
    }

    /// <summary>
    /// Creating a group and adding all lines in the model space to it
    /// </summary>
    [CommandMethod("Linq2AcadExample11")]
    public void CreatingAGroupAndAddingAllLinesInTheModelSpaceToIt()
    {
      var groupName = GetString("Enter group name");

      using (var db = AcadDatabase.Active())
      {
        var lines = db.ModelSpace
                      .OfType<Line>()
                      .ToArray();
        db.Groups
          .Create(groupName, lines);

        WriteMessage("Group " + groupName + " created and " + lines.Length + " entities added");
      }
    }
  }
}
