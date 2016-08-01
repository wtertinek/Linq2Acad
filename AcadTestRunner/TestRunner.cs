using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcadTestRunner
{
  public static class TestRunner
  {
    private const string AppSetting = "AcadRootDir";
    private static string coreConsolePath;

    public static void Init(string coreConsolePath)
    {
      if (!File.Exists(coreConsolePath))
      {
        throw new FileNotFoundException("Path " + coreConsolePath + " not found");
      }

      TestRunner.coreConsolePath = coreConsolePath;
    }

    public static TestResult Test(string acadTestAssembly, string acadTestClass, string acadTestMethod)
    {
      #region Parameter checks

      if (string.IsNullOrEmpty(coreConsolePath))
      {
        var assemblyPath = typeof(TestRunner).Assembly.Location;
        var configuration = ConfigurationManager.OpenExeConfiguration(assemblyPath);

        if (configuration.AppSettings.Settings.AllKeys.Any(key => key == AppSetting))
        {
          var fileName = Path.Combine(configuration.AppSettings.Settings[AppSetting].Value, "AcCoreConsole.exe");

          if (File.Exists(fileName))
          {
            coreConsolePath = fileName;
          }
          else
          {
            throw new FileNotFoundException("Path " + coreConsolePath + " not found");
          }
        }
        else
        {
          throw new FileNotFoundException("AppSetting '" + AppSetting + "' not found");
        }
      }
      else if (!File.Exists(acadTestAssembly))
      {
        throw new FileNotFoundException("Path " + acadTestAssembly + " not found");
      }

      #endregion

      // TODO: Add test runner code here

      return TestResult.TestFailed("Not implemented", "");
    }
  }
}
