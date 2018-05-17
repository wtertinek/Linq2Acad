
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports Autodesk.AutoCAD.ApplicationServices
Imports Autodesk.AutoCAD.DatabaseServices
Imports Autodesk.AutoCAD.EditorInput
Imports Autodesk.AutoCAD.Runtime

Partial Public Class SampleCode
  Implements IExtensionApplication
  Public Sub Initialize() Implements IExtensionApplication.Initialize
  End Sub

  Public Sub Terminate() Implements IExtensionApplication.Terminate
  End Sub

  Private Function GetString(message As String) As String
    If Application.DocumentManager.MdiActiveDocument IsNot Nothing Then
      Dim editor = Application.DocumentManager.MdiActiveDocument.Editor

      Dim result = editor.GetString(If(message.EndsWith(":"), message, (message & Convert.ToString(":"))))

      If result.Status = PromptStatus.OK Then
        Return result.StringResult
      Else
        Return Nothing
      End If
    Else
      Return Nothing
    End If
  End Function

  Private Function GetEntity(message As String) As ObjectId
    If Application.DocumentManager.MdiActiveDocument IsNot Nothing Then
      Dim editor = Application.DocumentManager.MdiActiveDocument.Editor
      Dim result = editor.GetEntity(message & Convert.ToString(":"))

      If result.Status = PromptStatus.OK Then
        Return result.ObjectId
      Else
        Return ObjectId.Null
      End If
    Else
      Return ObjectId.Null
    End If
  End Function

  Private Sub WriteMessage(message As String)
    If Application.DocumentManager.MdiActiveDocument IsNot Nothing Then
      Dim editor = Application.DocumentManager.MdiActiveDocument.Editor
      editor.WriteLine(message)
    End If
  End Sub
End Class
