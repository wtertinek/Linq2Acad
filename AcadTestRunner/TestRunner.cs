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
    private const string AppSettingAcadRootDir = "AcadRootDir";
    private const string AppSettingAddinRootDir = "AddinRootDir";
    private static string coreConsolePath;
    private static string addinPath;

    public static void Init(string acadRootDir, string addinRootDir)
    {
      if (!Directory.Exists(acadRootDir))
      {
        throw new FileNotFoundException("Directory " + acadRootDir + " not found");
      }
      else if (!File.Exists(Path.Combine(acadRootDir, "AcCoreConsole.exe")))
      {
        throw new FileNotFoundException("File " + Path.Combine(acadRootDir, "AcCoreConsole.exe") + " not found");
      }
      else if (!Directory.Exists(addinRootDir))
      {
        throw new FileNotFoundException("Directory " + addinRootDir + " not found");
      }
      else if (!File.Exists(Path.Combine(addinRootDir, Path.GetFileName(typeof(TestRunner).Assembly.Location))))
      {
        throw new FileNotFoundException("File " + Path.Combine(addinRootDir, Path.GetFileName(typeof(TestRunner).Assembly.Location)) + " not found");
      }

      TestRunner.coreConsolePath = Path.Combine(acadRootDir, "AcCoreConsole.exe");
      TestRunner.addinPath = Path.Combine(addinRootDir, Path.GetFileName(typeof(TestRunner).Assembly.Location));
    }

    public static TestResult RunTest<T>(string acadTestName) where T : class
    {
      return RunTest(typeof(T).Assembly.Location, typeof(T).Name, acadTestName);
    }

    public static TestResult RunTest(Type testClassType, string acadTestName)
    {
      return RunTest(testClassType.Assembly.Location, testClassType.Name, acadTestName);
    }

    public static TestResult RunTest(string testAssemblyPath, string testClassName, string acadTestName)
    {
      #region Parameter checks

      if (string.IsNullOrEmpty(coreConsolePath) ||
          string.IsNullOrEmpty(addinPath))
      {
        var assemblyPath = typeof(TestRunner).Assembly.Location;
        var configuration = ConfigurationManager.OpenExeConfiguration(assemblyPath);

        if (configuration.AppSettings.Settings.AllKeys.Any(key => key == AppSettingAcadRootDir))
        {
          Init(configuration.AppSettings.Settings[AppSettingAcadRootDir].Value,
               configuration.AppSettings.Settings[AppSettingAddinRootDir].Value);
        }
        else
        {
          throw new FileNotFoundException("AppSetting '" + AppSettingAcadRootDir + "' not found");
        }
      }
      
      if (!File.Exists(testAssemblyPath))
      {
        throw new FileNotFoundException("Path " + testAssemblyPath + " not found");
      }

      #endregion

      var coreConsole = new CoreConsole(coreConsolePath, addinPath);
      var metadata = new TestMetadata(testAssemblyPath, testClassName, acadTestName);
      var dwgFilePath = metadata.AcadTestAttribute != null ? metadata.AcadTestAttribute.DwgFilePath : null;
      var result = coreConsole.LoadAndExecuteTest(testAssemblyPath, testClassName, acadTestName, dwgFilePath);

      if (result.ExitCode == 0)
      {
        var idx = FindIndex(result.Output, Notification.GetPassedMessage(acadTestName));

        if (idx >= 0)
        {
          return TestResult.TestPassed(result.Output);
        }
        else
        {
          var failedMessage = Notification.GetFailedMessage(acadTestName);
          idx = FindIndex(result.Output, failedMessage);

          if (idx >= 0)
          {
            var msg = result.Output
                            .ElementAt(idx)
                            .Trim()
                            .Replace(failedMessage + " - ", "");

            return TestResult.TestFailed(msg, result.Output);
          }
          else
          {
            return TestResult.TestFailed("A general error occured", result.Output);
          }
        }
      }
      else
      {
        return TestResult.TestFailed("A general error occured", result.Output);
      }
    }

    private static int FindIndex(IReadOnlyCollection<string> output, string searchString)
    {
      return output.ToList()
                   .FindIndex(l => l.TrimStart()
                                    .StartsWith(searchString));
    }
  }
}
