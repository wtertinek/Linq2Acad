using System;
using System.Collections.Generic;
using System.Diagnostics;
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

    public TestExecutionResult LoadAndExecuteTest(string testAssemblyPath, string testClassName, string acadTestName, string dwgFilePath)
    {
      var dwgFileProvided = !string.IsNullOrEmpty(dwgFilePath);

      using (var fileManager = new FileManager(dwgFilePath))
      {
        var builder = new ScriptBuilder()
                      .NetLoad(addinPath)
                      .Command("LoadAndExecuteTest", testAssemblyPath, testClassName, acadTestName)
                      .Quit();

        if (!dwgFileProvided)
        {
          builder.QSave(fileManager.TmpDwgFilePath);
        }

        fileManager.SaveScript(builder.ToString());

        try
        {
          var param = new StringBuilder();
          param.AppendFormat(" /s \"{0}\"", fileManager.ScriptFilePath);

          if (dwgFileProvided)
          {
            param.AppendFormat(" /i \"{0}\"", dwgFilePath);
          }

          var process = new Process();
          var output = new List<string>();
          var exitcode = process.StartAndWait(coreConsolePath, param.ToString(), o => output.Add(o.Replace("\0", "")));
          return new TestExecutionResult(exitcode, output);
        }
        catch
        {
          return new TestExecutionResult(-1, new [] { "TestRunner: error starting process" });
        }
      }
    }
  }
}
