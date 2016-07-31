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
    private const string AcadRootDir = "AcadRootDir";
    private static string coreConsolePath;

    public static void Init(string coreConsolePath)
    {
      if (!File.Exists(coreConsolePath))
      {
        throw new FileNotFoundException("Path " + coreConsolePath + " not found");
      }

      TestRunner.coreConsolePath = coreConsolePath;
    }

    public static TestResult Test(string commandMethod, string acadTestAssemblyPath)
    {
      #region Parameter checks

      if (string.IsNullOrEmpty(coreConsolePath))
      {
        var assemblyPath = typeof(TestRunner).Assembly.Location;
        var configuration = ConfigurationManager.OpenExeConfiguration(assemblyPath);

        if (configuration.AppSettings.Settings.AllKeys.Any(key => key == AcadRootDir))
        {
          var fileName = Path.Combine(configuration.AppSettings.Settings[AcadRootDir].Value, "AcCoreConsole.exe");

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
          throw new FileNotFoundException("AppSetting 'AcadRootDir' not found");
        }
      }
      else if (!File.Exists(acadTestAssemblyPath))
      {
        throw new FileNotFoundException("Path " + acadTestAssemblyPath + " not found");
      }

      #endregion

      // TODO: Add test runner code here

      return TestResult.TestFailed("Not implemented");
    }
  }
}
