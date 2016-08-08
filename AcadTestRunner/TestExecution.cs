using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.Runtime;

namespace AcadTestRunner
{
  public class TestExecution
  {
    [CommandMethod("LoadAndExecuteTest")]
    public static void LoadAndExecuteTest()
    {
      var loaderNotifier = new Notification("TestLoader");
      Notification testNotifier = null;

      try
      {
        var editor = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
        var assemblyPath = editor.GetString(Notification.GetMessage("TestLoader", "Test assembly path")).StringResult;
        var testClassName = editor.GetString(Notification.GetMessage("TestLoader", "Test class name")).StringResult;
        var testMethodName = editor.GetString(Notification.GetMessage("TestLoader", "Test method name")).StringResult;
        testNotifier = new Notification(testMethodName);

        var type = Assembly.LoadFrom(assemblyPath)
                           .GetTypes()
                           .Select(t => new { Type = t, Attributes = t.GetCustomAttributes(typeof(AcadTestClassAttribute)).ToArray() })
                           .FirstOrDefault(t => t.Attributes.Any(a => ((AcadTestClassAttribute)a).TestClassName == testClassName));

        if (type == null)
        {
          testNotifier.TestFailed("Test class " + testClassName + " not found");
          loaderNotifier.WriteMessage("Test execution finished with errors");
        }
        else
        {
          loaderNotifier.WriteMessage("Type " + type.Type.Name + " loaded");

          var method = type.Type.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                                .Select(m => new { Method = m, Attributes = m.GetCustomAttributes(typeof(AcadTestMethodAttribute)).ToArray() })
                                .FirstOrDefault(m => m.Attributes.Any(a => ((AcadTestMethodAttribute)a).TestMethodName == testMethodName));

          if (method == null)
          {
            testNotifier.TestFailed("Test method " + testMethodName + " not found");
            loaderNotifier.WriteMessage("Test execution finished with errors");
          }
          else
          {
            loaderNotifier.WriteMessage("Test method " + method.Method.Name + " found");

            var instance = Activator.CreateInstance(type.Type);
            loaderNotifier.WriteMessage("Instance of " + type.Type.Name + " created");

            loaderNotifier.WriteMessage("Invoking test method " + method.Method);
            type.Type.InvokeMember(method.Method.Name, BindingFlags.InvokeMethod, null, instance, new object[0]);

            testNotifier.TestPassed();

            loaderNotifier.WriteMessage("Test execution finished");
          }
        }
      }
      catch (AssertFailedException e)
      {
        if (testNotifier != null)
        {
          testNotifier.TestFailed(e.Message);
        }

        loaderNotifier.WriteMessage("Test execution finished with errors");
      }
      catch (System.Exception e)
      {
        if (testNotifier != null)
        {
          testNotifier.TestFailed(e);
        }

        loaderNotifier.WriteMessage("Test execution finished with errors");
      }
    }
  }
}
