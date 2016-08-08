using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcadTestRunner
{
  internal class ScriptBuilder
  {
    public StringBuilder script;
    private string tmpFileName;

    public ScriptBuilder(string dwgFilePath)
    {
      script = new StringBuilder();
      tmpFileName = Path.GetTempFileName()
                        .Replace(".tmp", "");

      if (string.IsNullOrEmpty(dwgFilePath))
      {
        script.AppendLine("_qsave");
        script.AppendLine(GetFilePath(tmpFileName + ".dwg"));
      }
    }

    public ScriptBuilder NetLoad(string path)
    {
      script.AppendLine("_netload");
      script.AppendLine(GetFilePath(path));
      return this;
    }

    public ScriptBuilder Command(string command, params string[] argumnets)
    {
      script.AppendLine(command);
      argumnets.ToList()
               .ForEach(a => script.AppendLine(a));
      return this;
    }

    public ScriptBuilder Quit()
    {
      script.AppendLine("_quit" + Environment.NewLine);
      return this;
    }

    public string SaveScript()
    {
      SaveScript(tmpFileName + ".scr");
      return tmpFileName + ".scr";
    }

    public void SaveScript(string filePath)
    {
      File.WriteAllText(filePath, script.ToString(), Encoding.Default);
    }

    public string GetTmpDwgFileName()
    {
      return tmpFileName + ".dwg";
    }

    private static string GetFilePath(string path)
    {
      return "\"" + path.Replace("\\", "/") + "\"";
    }
  }
}
