
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
  ''' This sample removes all entities from the model space
  ''' </summary>
  <CommandMethod("Linq2AcadExample1")>
  Public Sub RemovingAllEntitiesFromTheModelSpace()
    WriteMessage("This sample removes all entities from the model space")

    Using db = AcadDatabase.Active()
      db.ModelSpace.Clear()
    End Using

    WriteMessage("Model space cleared")
  End Sub

  ''' <summary>
  ''' This sample removes all BlockReferences from the model space
  ''' </summary>
  <CommandMethod("Linq2AcadExample2")>
  Public Sub ErasingAllBlockReferencesFromTheModelSpace()
    WriteMessage("This sample removes all BlockReferences from the model space")

    Using db = AcadDatabase.Active()
      For Each br In db.ModelSpace.OfType(Of BlockReference)().UpgradeOpen()
        br.Erase()
      Next
    End Using

    WriteMessage("All block references removed from model space")
  End Sub

  ''' <summary>
  ''' This sample adds a line to the model space
  ''' </summary>
  <CommandMethod("Linq2AcadExample3")>
  Public Sub AddingALineToTheModelSpace()
    WriteMessage("This sample adds a line to the model space")

    Using db = AcadDatabase.Active()
      db.ModelSpace.Add(New Line(New Point3d(0, 0, 0), New Point3d(100, 100, 0)))
    End Using

    WriteMessage("Line added to model space")
  End Sub

  ''' <summary>
  ''' This sample creates a new layer
  ''' </summary>
  <CommandMethod("Linq2AcadExample4")>
  Public Sub CreatingANewLayer()
    WriteMessage("This sample creates a new layer")

    Dim layerName = GetString("Enter layer name")
    Dim colorName = GetString("Enter color name")

    If layerName IsNot Nothing And colorName IsNot Nothing Then
      Using db = AcadDatabase.Active()
        Dim layer = db.Layers.Create(layerName)
        layer.Color = Color.FromColor(System.Drawing.Color.FromName(colorName))
      End Using
    End If

    WriteMessage($"Layer {layerName} created")
  End Sub

  ''' <summary>
  ''' This sample prints all layer names
  ''' </summary>
  <CommandMethod("Linq2AcadExample5")>
  Public Sub PrintingAllLayerNames()
    WriteMessage("This sample prints all layer names")

    Using db = AcadDatabase.Active()
      For Each layer In db.Layers
        WriteMessage(layer.Name)
      Next
    End Using
  End Sub

  ''' <summary>
  ''' This sample turns off all layers, except the one the user enters
  ''' </summary>
  <CommandMethod("Linq2AcadExample6")>
  Public Sub TurningOffAllLayersExceptTheOneTheUserEnters()
    WriteMessage("This sample turns off all layers, except the one the user enters")

    Dim layerName = GetString("Enter layer name")

    If layerName IsNot Nothing Then
      Using db = AcadDatabase.Active()
        Dim layer = db.Layers.Element(layerName)

        For Each layer In db.Layers.Except(layer).UpgradeOpen()
          layer.IsOff = True
        Next
      End Using

      WriteMessage($"All layers turned off, except {layerName}")
    End If

  End Sub

  ''' <summary>
  ''' This sample creates a layer and adds all red lines in the model space to it
  ''' </summary>
  <CommandMethod("Linq2AcadExample7")>
  Public Sub CreatingALayerAndAddingAllRedLinesInTheModelSpaceToIt()
    WriteMessage("This sample creates a layer and This sample adds all red lines in the model space to it")

    Dim layerName = GetString("Enter layer name")

    If layerName IsNot Nothing Then
      Using db = AcadDatabase.Active()
        Dim lines = db.ModelSpace.OfType(Of Line)().Where(Function(l) l.Color.ColorValue.Name = "ffff0000")
        db.Layers.Create(layerName, lines)

        WriteMessage($"All red Lines moved to new layer {layerName}")
      End Using
    End If
  End Sub

  ''' <summary>
  ''' This sample moves entities from one layer to another
  ''' </summary>
  <CommandMethod("Linq2AcadExample8")>
  Public Sub MovingEntitiesFromOneLayerToAnother()
    WriteMessage("This sample moves entities from one layer to another")

    Dim sourceLayerName = GetString("Enter source layer name")
    Dim targetLayerName = GetString("Enter target layer name")

    If sourceLayerName IsNot Nothing And targetLayerName IsNot Nothing Then
      Using db = AcadDatabase.Active()
        Dim entities = db.CurrentSpace.Where(Function(e) e.Layer = sourceLayerName)
        db.Layers.Element(targetLayerName).AddRange(entities)
      End Using

      WriteMessage($"All entities on layer {sourceLayerName} moved to layer {targetLayerName}")
    End If
  End Sub

  ''' <summary>
  ''' This sample imports a block from a drawing file
  ''' </summary>
  <CommandMethod("Linq2AcadExample9")>
  Public Sub ImportingABlockFromADrawingFile()
    WriteMessage("This sample imports a block from a drawing file")

    Dim filePath = GetString("Enter file path")
    Dim blockName = GetString("Enter block name")

    If filePath IsNot Nothing And blockName IsNot Nothing Then
      Using sourceDb = AcadDatabase.OpenReadOnly(filePath)
        Dim block = sourceDb.Blocks.Element(blockName)

        Using activeDb = AcadDatabase.Active()
          activeDb.Blocks.Import(block)
        End Using
      End Using

      WriteMessage($"Block {blockName} imported")
    End If
  End Sub

  ''' <summary>
  ''' This sample opens a drawing from file and counts the BlockReferences in the model space
  ''' </summary>
  <CommandMethod("Linq2AcadExample10")>
  Public Sub OpeningADrawingFromFileAndCountingTheBlockReferencesInTheModelSpace()
    WriteMessage("This sample opens a drawing from file and counting the BlockReferences in the model space")

    Dim filePath = GetString("Enter file path")

    If filePath IsNot Nothing Then
      Using db = AcadDatabase.OpenReadOnly(filePath)
        Dim count = db.ModelSpace.OfType(Of BlockReference)().Count()

        WriteMessage($"Model space block references in file {filePath}: {count}")
      End Using
    End If
  End Sub

  ''' <summary>
  ''' This sample picks an entity and saves a string on it
  ''' </summary>
  <CommandMethod("Linq2AcadExample11")>
  Public Sub PickingAnEntityAndSavingAStringOnIt()
    WriteMessage("This sample picks an entity and saves a string on it")

    Dim entityId = GetEntity("Pick an entity")
    Dim key = GetString("Enter key")
    Dim str = GetString("Enter string to save")

    If entityId.IsValid And key IsNot Nothing And str IsNot Nothing Then
      Using db = AcadDatabase.Active()
        db.CurrentSpace.Element(entityId).SaveData(key, str)
      End Using

      WriteMessage($"Key-value-pair {key}:{str} saved on entity")
    End If
  End Sub

  ''' <summary>
  ''' This sample picks an entity and reads a string from it
  ''' </summary>
  <CommandMethod("Linq2AcadExample12")>
  Public Sub PickingAnEntityAndReadingAStringFromIt()
    WriteMessage("This sample picks an entity and reads a string from it")

    Dim entityId = GetEntity("Pick an entity")
    Dim key = GetString("Enter key")

    If entityId.IsValid And key IsNot Nothing Then

      Using db = AcadDatabase.Active()
        Dim str = db.CurrentSpace.Element(entityId).GetData(Of String)(key)

        WriteMessage($"String {str} read from entity")
      End Using
    End If
  End Sub

  ''' <summary>
  ''' This sample picks an entity and reads a string from it (with XData as the data source)
  ''' </summary>
  <CommandMethod("Linq2AcadExample13")>
  Public Sub PickingAnEntityAndReadingAStringFromItWithXDataAsTheDataSource()
    WriteMessage("This sample picks an entity and reads a string from it (with XData as the data source)")

    Dim entityId = GetEntity("Pick an entity")
    Dim key = GetString("Enter key")

    If entityId.IsValid And key IsNot Nothing Then
      Using db = AcadDatabase.Active()
        Dim str = db.CurrentSpace.Element(entityId).GetData(Of String)(key, True)

        WriteMessage($"String {str} read from entity's XData")
      End Using
    End If
  End Sub

  ''' <summary>
  ''' This sample counts the number of entities in all paper space layouts
  ''' </summary>
  <CommandMethod("Linq2AcadExample14")>
  Public Sub CountingTheNumberOfEntitiesInAllPaperSpaceLayouts()
    WriteMessage("This sample counts the number of entities in all paper space layouts")

    Using db = AcadDatabase.Active()
      Dim count = db.PaperSpace().SelectMany(Function(ps) ps).Count()

      WriteMessage($"{count} entities in all paper space layouts")
    End Using
  End Sub

  ''' <summary>
  ''' This sample changes the summary info
  ''' </summary>
  <CommandMethod("Linq2AcadExample15")>
  Public Sub ChangingTheSummaryInfo()
    WriteMessage("This sample changes the summary info")

    Using db = AcadDatabase.Active()
      db.SummaryInfo.Author = "John Doe"
      db.SummaryInfo.CustomProperties("CustomData1") = "42"

      WriteMessage("Summary info updated")
    End Using
  End Sub

  ''' <summary>
  ''' This sample reloads all loaded XRefs
  ''' </summary>
  <CommandMethod("Linq2AcadExample16")>
  Public Sub ReloadingAllLoadedXRefs()
    WriteMessage("This sample reloads all loaded XRefs")

    Using db = AcadDatabase.Active()
      For Each xRef In db.XRefs.Where(Function(xr) xr.IsLoaded)
        xRef.Reload()
      Next
    End Using

    WriteMessage("XRefs reloaded")
  End Sub
End Class
