using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcadTestRunner
{
  internal class CoreConsole
  {
    private string coreConsolePath;
    private string addinPath;

    public CoreConsole(string coreConsolePath, string addinPath)
    {
      this.coreConsolePath = coreConsolePath;
      this.addinPath = addinPath;
    }

    public TestExecutionResult LoadAndExecuteTest(string acadTestAssemblyPath, string acadTestClassName, string acadTestMethodName, string dwgFilePath)
    {
      var builder = new ScriptBuilder(dwgFilePath)
                    .NetLoad(addinPath)
                    .Command("LoadAndExecuteTest", acadTestAssemblyPath, acadTestClassName, acadTestMethodName)
                    .Quit();
      var scriptFilePath = builder.SaveScript();

      try
      {
        var param = new StringBuilder();
        param.AppendFormat(" /s \"{0}\"", scriptFilePath);

        if (!string.IsNullOrEmpty(dwgFilePath))
        {
          param.AppendFormat(" /i \"{0}\"", dwgFilePath);
        }

        var process = new Process();
        var output = new List<string>();
        var exitcode = process.StartAndWait(coreConsolePath, param.ToString(), o => output.Add(o.Replace("\0", "")));
        return new TestExecutionResult(exitcode, output);
      }
      finally
      {
        if (File.Exists(scriptFilePath))
        {
          File.Delete(scriptFilePath);
        }

        var tmpDwgFileName = builder.GetTmpDwgFileName();

        if (File.Exists(tmpDwgFileName))
        {
          File.Delete(tmpDwgFileName);
        }
      }
    }
  }
}
