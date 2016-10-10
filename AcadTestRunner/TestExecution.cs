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
      Type expectedException = null;

      try
      {
        var editor = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
        var assemblyPath = editor.GetString(Notification.GetMessage("TestLoader", "Assembly path")).StringResult;
        var testClassName = editor.GetString(Notification.GetMessage("TestLoader", "Class name")).StringResult;
        var testName = editor.GetString(Notification.GetMessage("TestLoader", "AcadTest name")).StringResult;
        testNotifier = new Notification(testName);

        var type = Assembly.LoadFrom(assemblyPath)
                           .GetTypes()
                           .FirstOrDefault(t => t.Name == testClassName);

        if (type == null)
        {
          testNotifier.TestFailed("Class " + testClassName + " not found");
          loaderNotifier.WriteMessage("Test execution finished with errors");
        }
        else
        {
          loaderNotifier.WriteMessage("Class " + type.Name + " loaded");

          var method = type.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                           .Select(m => new { Method = m, AcadTestAttribute = m.GetCustomAttributes(typeof(AcadTestAttribute), false).FirstOrDefault() })
                           .FirstOrDefault(m => m.AcadTestAttribute != null &&
                                                ((AcadTestAttribute)m.AcadTestAttribute).TestMethodName == testName);

          if (method == null)
          {
            testNotifier.TestFailed("No AcadTest \"" + testName + "\" found");
            loaderNotifier.WriteMessage("Test execution finished with errors");
          }
          else
          {
            loaderNotifier.WriteMessage("AcadTest " + testName + " found");

            var hasDefaultConstructor = type.GetConstructors()
                                            .Any(c => c.IsPublic &&
                                                       c.GetParameters().Count() == 0);

            if (!hasDefaultConstructor)
            {
              testNotifier.TestFailed("No public default constructor found in class " + testClassName);
              loaderNotifier.WriteMessage("Test execution finished with errors");
            }
            else
            {
              var expectedExceptionAttribute = method.Method
                                                     .GetCustomAttribute(typeof(ExpectedExceptionAttribute), false);

              if (expectedExceptionAttribute != null)
              {
                expectedException = (expectedExceptionAttribute as ExpectedExceptionAttribute).ExpectedException as Type;
                loaderNotifier.WriteMessage("ExcpectedException " + expectedException.Name + " found");
              }

              var instance = Activator.CreateInstance(type);
              loaderNotifier.WriteMessage("Instance of " + type.Name + " created");

              loaderNotifier.WriteMessage("Executing AcadTest " + testName);

              var delay = (method.AcadTestAttribute as AcadTestAttribute).InvocationDelay;

              if (delay > 0)
              {
                loaderNotifier.WriteMessage("Waiting " + delay + " seconds");
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(delay));
              }

              type.InvokeMember(method.Method.Name, BindingFlags.InvokeMethod, null, instance, new object[0]);

              if (expectedException != null)
              {
                testNotifier.TestFailed("Expected exception of type " + expectedException.FullName + " not thrown");
                loaderNotifier.WriteMessage("Test execution finished with errors");
              }
              else
              {
                testNotifier.TestPassed();
                loaderNotifier.WriteMessage("Test execution finished");
              }
            }
          }
        }
      }
      catch (AcadAssertFailedException e)
      {
        testNotifier.TestFailed(e.Message, e.StackTrace);
        loaderNotifier.WriteMessage("Test execution finished with errors");
      }
      catch (TargetInvocationException tie)
      {
        var e = tie.InnerException;

        if (expectedException != null &&
            e.GetType().Equals(expectedException))
        {
          testNotifier.TestPassed();
        }
        else
        {
          testNotifier.TestFailed(e);
          loaderNotifier.WriteMessage("Test execution finished with errors");
        }
      }
      catch (System.Exception e)
      {
        loaderNotifier.WriteMessage(e.Message);
        loaderNotifier.WriteMessage("Test execution finished with errors");
      }
    }
  }
}
