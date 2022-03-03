using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.Colors;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Runtime;

namespace Linq2Acad
{
  public partial class SampleCode
  {
    /// <summary>
    /// This sample removes all entities from the model space
    /// </summary>
    [CommandMethod("Linq2AcadExample1")]
    public void RemovingAllEntitiesFromTheModelSpace()
    {
      WriteMessage("This sample removes all entities from the model space");

      using (var db = AcadDatabase.Active())
      {
        db.ModelSpace
          .Clear();
      }

      WriteMessage("Model space cleared");
    }

    /// <summary>
    /// This sample removes all BlockReferences from the model space
    /// </summary>
    [CommandMethod("Linq2AcadExample2")]
    public void ErasingAllBlockReferencesFromTheModelSpace()
    {
      WriteMessage("This sample removes all BlockReferences from the model space");

      using (var db = AcadDatabase.Active())
      {
        foreach (var br in db.ModelSpace
                             .OfType<BlockReference>()
                             .UpgradeOpen())
        {
          br.Erase();
        }
      }

      WriteMessage("All block references removed from model space");
    }

    /// <summary>
    /// This sample adds a line to the model space
    /// </summary>
    [CommandMethod("Linq2AcadExample3")]
    public void AddingALineToTheModelSpace()
    {
      WriteMessage("This sample adds a line to the model space");

      using (var db = AcadDatabase.Active())
      {
        db.ModelSpace
          .Add(new Line(Point3d.Origin,
                        new Point3d(100, 100, 0)));
      }

      WriteMessage("Line added to model space");
    }

    /// <summary>
    /// This sample creates a new layer
    /// </summary>
    [CommandMethod("Linq2AcadExample4")]
    public void CreatingANewLayer()
    {
      WriteMessage("This sample creates a new layer");

      var layerName = GetString("Enter layer name");
      var colorName = GetString("Enter color name");

      if (layerName != null &&
          colorName != null)
      {
        using (var db = AcadDatabase.Active())
        {
          var layer = db.Layers.Create(layerName);
          layer.Color = Color.FromColor(System.Drawing.Color.FromName(colorName));
        }

        WriteMessage($"Layer {layerName} created");
      }
    }

    /// <summary>
    /// This sample prints all layer names
    /// </summary>
    [CommandMethod("Linq2AcadExample5")]
    public void PrintingAllLayerNames()
    {
      WriteMessage("This sample prints all layer names");

      using (var db = AcadDatabase.Active())
      {
        foreach (var layer in db.Layers)
        {
          WriteMessage(layer.Name);
        }
      }
    }

    /// <summary>
    /// This sample turns off all layers, except the one the user enters
    /// </summary>
    [CommandMethod("Linq2AcadExample6")]
    public void TurningOffAllLayersExceptTheOneTheUserEnters()
    {
      WriteMessage("This sample turns off all layers, except the one the user enters");

      var layerName = GetString("Enter layer name");

      if (layerName != null)
      {
        using (var db = AcadDatabase.Active())
        {
          var layerToIgnore = db.Layers
                                .Element(layerName);

          foreach (var layer in db.Layers
                                  .Except(layerToIgnore)
                                  .UpgradeOpen())
          {
            layer.IsOff = true;
          }
        }

        WriteMessage($"All layers turned off, except {layerName}");
      }
    }

    /// <summary>
    /// This sample creates a layer and adds all red lines in the model space to it
    /// </summary>
    [CommandMethod("Linq2AcadExample7")]
    public void CreatingALayerAndAddingAllRedLinesInTheModelSpaceToIt()
    {
      WriteMessage("This sample creates a layer and This sample adds all red lines in the model space to it");

      var layerName = GetString("Enter layer name");

      if (layerName != null)
      {
        using (var db = AcadDatabase.Active())
        {
          var lines = db.ModelSpace
                        .OfType<Line>()
                        .Where(l => l.Color.ColorValue.Name == "ffff0000");
          db.Layers
            .Create(layerName, lines);
        }

        WriteMessage($"All red lines moved to new layer {layerName}");
      }
    }

    /// <summary>
    /// This sample moves entities from one layer to another
    /// </summary>
    [CommandMethod("Linq2AcadExample8")]
    public void MovingEntitiesFromOneLayerToAnother()
    {
      WriteMessage("This sample moves entities from one layer to another");

      var sourceLayerName = GetString("Enter source layer name");
      var targetLayerName = GetString("Enter target layer name");

      if (sourceLayerName != null && targetLayerName != null)
      {
        using (var db = AcadDatabase.Active())
        {
          var entities = db.CurrentSpace
                           .Where(e => e.Layer == sourceLayerName);
          db.Layers
            .Element(targetLayerName)
            .AddRange(entities);
        }

        WriteMessage($"All entities on layer {sourceLayerName} moved to layer {targetLayerName}");
      }
    }

    /// <summary>
    /// This sample imports a block from a drawing file
    /// </summary>
    [CommandMethod("Linq2AcadExample9")]
    public void ImportingABlockFromADrawingFile()
    {
      WriteMessage("This sample imports a block from a drawing file");

      var filePath = GetString("Enter file path");
      var blockName = GetString("Enter block name");

      if (filePath != null && blockName != null)
      {
        using (var sourceDb = AcadDatabase.OpenReadOnly(filePath))
        {
          var block = sourceDb.Blocks
                              .Element(blockName);

          using (var activeDb = AcadDatabase.Active())
          {
            activeDb.Blocks
                    .Import(block);
          }
        }

        WriteMessage($"Block {blockName} imported");
      }
    }

    /// <summary>
    /// This sample opens a drawing from file and counts the BlockReferences in the model space
    /// </summary>
    [CommandMethod("Linq2AcadExample10")]
    public void OpeningADrawingFromFileAndCountingTheBlockReferencesInTheModelSpace()
    {
      WriteMessage("This sample opens a drawing from file and counting the BlockReferences in the model space");

      var filePath = GetString("Enter file path");

      if (filePath != null)
      {
        using (var db = AcadDatabase.OpenReadOnly(filePath))
        {
          var count = db.ModelSpace
                        .OfType<BlockReference>()
                        .Count();

          WriteMessage($"Model space block references in file {filePath}: {count}");
        }
      }
    }

    /// <summary>
    /// This sample picks an entity and saves a string on it
    /// </summary>
    [CommandMethod("Linq2AcadExample11")]
    public void PickingAnEntityAndSavingAStringOnIt()
    {
      WriteMessage("This sample picks an entity and saves a string on it");

      var entityId = GetEntity("Pick an entity");
      var key = GetString("Enter key");
      var str = GetString("Enter string to save");

      if (entityId.IsValid && key != null && str != null)
      {
        using (var db = AcadDatabase.Active())
        {
          db.CurrentSpace
            .Element(entityId)
            .SaveData(key, str);
        }

        WriteMessage($"Key-value-pair {key}:{str} saved on entity");
      }
    }

    /// <summary>
    /// This sample picks an entity and reads a string from it
    /// </summary>
    [CommandMethod("Linq2AcadExample12")]
    public void PickingAnEntityAndReadingAStringFromIt()
    {
      WriteMessage("This sample picks an entity and reads a string from it");

      var entityId = GetEntity("Pick an entity");
      var key = GetString("Enter key");

      if (entityId.IsValid && key != null)
      {
        using (var db = AcadDatabase.Active())
        {
          var str = db.CurrentSpace
                      .Element(entityId)
                      .GetData<string>(key);

          WriteMessage($"String {str} read from entity");
        }
      }
    }

    /// <summary>
    /// This sample picks an entity and reads a string from it (with XData as the data source)
    /// </summary>
    [CommandMethod("Linq2AcadExample13")]
    public void PickingAnEntityAndReadingAStringFromItWithXDataAsTheDataSource()
    {
      WriteMessage("This sample picks an entity and reads a string from it (with XData as the data source)");

      var entityId = GetEntity("Pick an entity");
      var key = GetString("Enter RegApp name");

      if (entityId.IsValid && key != null)
      {
        using (var db = AcadDatabase.Active())
        {
          var str = db.CurrentSpace
                      .Element(entityId)
                      .GetData<string>(key, true);

          WriteMessage($"String {str} read from entity's XData");
        }
      }
    }

    /// <summary>
    /// This sample counts the number of entities in all paper space layouts
    /// </summary>
    [CommandMethod("Linq2AcadExample14")]
    public void CountingTheNumberOfEntitiesInAllPaperSpaceLayouts()
    {
      WriteMessage("This sample counts the number of entities in all paper space layouts");

      using (var db = AcadDatabase.Active())
      {
        var count = db.PaperSpace
                      .SelectMany(ps => ps)
                      .Count();

        WriteMessage($"{count} entities in all paper space layouts");
      }
    }

    /// <summary>
    /// This sample changes the summary info
    /// </summary>
    [CommandMethod("Linq2AcadExample15")]
    public void ChangingTheSummaryInfo()
    {
      WriteMessage("This sample changes the summary info");

      using (var db = AcadDatabase.Active())
      {
        db.SummaryInfo.Author = "John Doe";
        db.SummaryInfo.CustomProperties["CustomData1"] = "42";
      }

      WriteMessage("Summary info updated");
    }

    /// <summary>
    /// This sample reloads all loaded XRefs
    /// </summary>
    [CommandMethod("Linq2AcadExample16")]
    public void ReloadingAllLoadedXRefs()
    {
      WriteMessage("This sample reloads all loaded XRefs");

      using (var db = AcadDatabase.Active())
      {
        foreach (var xRef in db.XRefs
                               .Where(xr => xr.IsLoaded))
        {
          xRef.Reload();
        }
      }

      WriteMessage("XRefs reloaded");
    }
  }
}
