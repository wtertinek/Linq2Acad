using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;

namespace Linq2Acad
{
  public partial class Examples : IExtensionApplication
  {
    public void Initialize()
    {
    }

    public void Terminate()
    {
    }

    private string GetString(string message)
    {
      var editor = Application.DocumentManager.MdiActiveDocument.Editor;
      
      var result = editor.GetString(message + ":");

      if (result.Status == PromptStatus.OK)
      {
        return result.StringResult;
      }
      else
      {
        return null;
      }
    }

    private ObjectId GetEntity(string message)
    {
      var editor = Application.DocumentManager.MdiActiveDocument.Editor;
      var result = editor.GetEntity(message + ":");

      if (result.Status == PromptStatus.OK)
      {
        return result.ObjectId;
      }
      else
      {
        return ObjectId.Null;
      }
    }

    private void WriteMessage(string message)
    {
      var editor = Application.DocumentManager.MdiActiveDocument.Editor;
      editor.WriteLine(message);
    }
  }
}
