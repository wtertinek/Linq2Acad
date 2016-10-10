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
    public StringBuilder builder;

    public ScriptBuilder()
    {
      builder = new StringBuilder();
    }

    public ScriptBuilder QSave(string dwgFilePath)
    {
      builder.Insert(0, GetFilePath(dwgFilePath) + Environment.NewLine);
      builder.Insert(0, "_qsave" + Environment.NewLine);
      return this;
    }

    public ScriptBuilder NetLoad(string path)
    {
      builder.AppendLine("_netload");
      builder.AppendLine(GetFilePath(path));
      return this;
    }

    public ScriptBuilder Command(string command, params string[] argumnets)
    {
      builder.AppendLine(command);
      argumnets.ToList()
               .ForEach(a => builder.AppendLine(a));
      return this;
    }

    public ScriptBuilder Quit()
    {
      builder.AppendLine("_quit" + Environment.NewLine);
      return this;
    }

    public override string ToString()
    {
      return builder.ToString();
    }

    private static string GetFilePath(string path)
    {
      return "\"" + path.Replace("\\", "/") + "\"";
    }
  }
}
