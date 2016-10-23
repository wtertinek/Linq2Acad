
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports Autodesk.AutoCAD.Colors
Imports Autodesk.AutoCAD.DatabaseServices
Imports Autodesk.AutoCAD.Geometry
Imports Autodesk.AutoCAD.Runtime

Partial Public Class SampleCode
  ''' <summary>
  ''' Removing all entities from the model space
  ''' </summary>
  <CommandMethod("Linq2AcadExample1")>
  Public Sub RemovingAllEntitiesFromTheModelSpace()
    Using db = AcadDatabase.Active()
      db.ModelSpace.Clear()
    End Using

    WriteMessage("Model space cleared")
  End Sub

  ''' <summary>
  ''' Removing all BlockReferences from the model space
  ''' </summary>
  <CommandMethod("Linq2AcadExample2")>
  Public Sub ErasingAllBlockReferencesFromTheModelSpace()
    Using db = AcadDatabase.Active()
      For Each br In db.ModelSpace.OfType(Of BlockReference)().UpgradeOpen()
        br.Erase()
      Next
    End Using

    WriteMessage("All block references removed from model space")
  End Sub

  ''' <summary>
  ''' Adding a line to the model space
  ''' </summary>
  <CommandMethod("Linq2AcadExample3")>
  Public Sub AddingALineToTheModelSpace()
    Using db = AcadDatabase.Active()
      db.ModelSpace.Add(New Line(New Point3d(0, 0, 0), New Point3d(100, 100, 0)))
    End Using

    WriteMessage("Line added to model space")
  End Sub

  ''' <summary>
  ''' Creating a new layer
  ''' </summary>
  <CommandMethod("Linq2AcadExample4")>
  Public Sub CreatingANewLayer()
    Dim name = GetString("Enter layer name")
    Dim colorName = GetString("Enter color name")

    Using db = AcadDatabase.Active()
      Dim layer = db.Layers.Create(name)
      layer.Color = Color.FromColor(System.Drawing.Color.FromName(colorName))
    End Using

    WriteMessage("Layer " + name + " created")
  End Sub

  ''' <summary>
  ''' Printing all layer names
  ''' </summary>
  <CommandMethod("Linq2AcadExample5")>
  Public Sub PrintingAllLayerNames()
    Using db = AcadDatabase.Active()
      For Each layer In db.Layers
        WriteMessage(layer.Name)
      Next
    End Using
  End Sub

  ''' <summary>
  ''' Turning off all layers, except the one the user enters
  ''' </summary>
  <CommandMethod("Linq2AcadExample6")>
  Public Sub TurningOffAllLayersExceptTheOneTheUserEnters()
    Dim layerName = GetString("Enter layer name")

    Using db = AcadDatabase.Active()
      Dim layer = db.Layers.Element(layerName)

      For Each layer In db.Layers.Except({layer}).UpgradeOpen()
        layer.IsOff = True
      Next
    End Using

    WriteMessage("All layers (except " + layerName + ") turned off")
  End Sub

  ''' <summary>
  ''' Creating a layer and adding all red lines in the model space to it
  ''' </summary>
  <CommandMethod("Linq2AcadExample7")>
  Public Sub CreatingALayerAndAddingAllRedLinesInTheModelSpaceToIt()
    Dim layerName = GetString("Enter layer name")

    Using db = AcadDatabase.Active()
      Dim lines = db.ModelSpace.OfType(Of Line)().Where(Function(l) l.Color.ColorValue.Name = "ffff0000")
      db.Layers.Create(layerName, lines)

      WriteMessage("All red Lines moved to new layer " + layerName)
    End Using
  End Sub

  ''' <summary>
  ''' Moving entities from one layer to another
  ''' </summary>
  <CommandMethod("Linq2AcadExample8")>
  Public Sub MovingEntitiesFromOneLayerToAnother()
    Dim sourceLayerName = GetString("Enter source layer name")
    Dim targetLayerName = GetString("Enter target layer name")

    Using db = AcadDatabase.Active()
      Dim entities = db.CurrentSpace.Where(Function(e) e.Layer = sourceLayerName)
      db.Layers.Element(targetLayerName).AddRange(entities)
    End Using

    WriteMessage("All entities on layer " + sourceLayerName + " moved to layer " + targetLayerName)
  End Sub

  ''' <summary>
  ''' Importing a block from a drawing file
  ''' </summary>
  <CommandMethod("Linq2AcadExample9")>
  Public Sub ImportingABlockFromADrawingFile()
    Dim filePath = GetString("Enter file path")
    Dim blockName = GetString("Enter block name")

    Using sourceDb = AcadDatabase.Open(filePath, DwgOpenMode.ReadOnly)
      Dim block = sourceDb.Blocks.Element(blockName)

      Using activeDb = AcadDatabase.Active()
        activeDb.Blocks.Import(block)
      End Using
    End Using

    WriteMessage("Block " + blockName + " imported")
  End Sub

  ''' <summary>
  ''' Opening a drawing from file and counting the BlockReferences in the model space
  ''' </summary>
  <CommandMethod("Linq2AcadExample10")>
  Public Sub OpeningADrawingFromFileAndCountingTheBlockReferencesInTheModelSpace()
    Dim filePath = GetString("Enter file path")

    Using db = AcadDatabase.Open(filePath, DwgOpenMode.ReadOnly)
      Dim count = db.ModelSpace.OfType(Of BlockReference)().Count()

      WriteMessage("Model space block references in file " + filePath + ": " + count)
    End Using
  End Sub

  ''' <summary>
  ''' Picking an entity and saving a string on it
  ''' </summary>
  <CommandMethod("Linq2AcadExample11")>
  Public Sub PickingAnEntityAndSavingAStringOnIt()
    Dim entityId = GetEntity("Pick an entity")
    Dim key = GetString("Enter key")
    Dim str = GetString("Enter string to save")

    Using db = AcadDatabase.Active()
      db.CurrentSpace.Element(entityId).SaveData(key, str)
    End Using

    WriteMessage("Key-value-pair " + key + ":" + str + " saved on entity")
  End Sub

  ''' <summary>
  ''' Picking an entity and reading a string from it
  ''' </summary>
  <CommandMethod("Linq2AcadExample12")>
  Public Sub PickingAnEntityAndReadingAStringFromIt()
    Dim entityId = GetEntity("Pick an entity")
    Dim key = GetString("Enter key")

    Using db = AcadDatabase.Active()
      Dim str = db.CurrentSpace.Element(entityId).GetData(Of String)(key)

      WriteMessage("String " + str + " read from entity")
    End Using
  End Sub

  ''' <summary>
  ''' Counting the number of entities in all paper space layouts
  ''' </summary>
  <CommandMethod("Linq2AcadExample13")>
  Public Sub CountingTheNumberOfEntitiesInAllPaperSpaceLayouts()
    Using db = AcadDatabase.Active()
      Dim allEntities = db.PaperSpace().SelectMany(Function(ps) ps)

      WriteMessage(allEntities.Count() + " entities in all paper space layouts")
    End Using
  End Sub

  ''' <summary>
  ''' Changing the summary info
  ''' </summary>
  <CommandMethod("Linq2AcadExample14")>
  Public Sub ChangingTheSummaryInfo()
    Using db = AcadDatabase.Active()
      db.SummaryInfo.Author = "John Doe"
            db.SummaryInfo.CustomProperties("CustomData1") = "42"

      WriteMessage("Summary info updated")
    End Using
  End Sub
End Class
