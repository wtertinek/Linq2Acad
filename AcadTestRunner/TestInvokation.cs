using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.Runtime;

namespace AcadTestRunner
{
  public class TestInvokation
  {
    [CommandMethod("LoadAndExecuteTest")]
    public static void LoadAndExecuteTest()
    {
      string assemblyPath = "";
      string testClassName = "";
      string testMethodName = "";

      var notifier = new Notification("TestLoader");

      try
      {
        var type = Assembly.LoadFrom(assemblyPath)
                           .GetTypes()
                           .Select(t => new { Type = t, Attributes = t.GetCustomAttributes(typeof(AcadTestClassAttribute)).ToArray() })
                           .FirstOrDefault(t => t.Attributes.Any(a => ((AcadTestClassAttribute)a).TestClassName == testClassName));

        if (type == null)
        {
          notifier.TestFailed("Test class " + testClassName + " not found");
        }
        else
        {
          var method = type.Type.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                                .Select(m => new { Method = m, Attributes = m.GetCustomAttributes(typeof(AcadTestMethodAttribute)).ToArray() })
                                .FirstOrDefault(m => m.Attributes.Any(a => ((AcadTestMethodAttribute)a).TestMethodName == testMethodName));

          if (method == null)
          {
            notifier.TestFailed("Test method " + testMethodName + " not found");
          }
          else
          {
            var instance = Activator.CreateInstance(type.Type);
            notifier = new Notification(testMethodName);
            type.Type.InvokeMember(method.Method.Name, BindingFlags.InvokeMethod, null, instance, new object[0]);
            notifier.TestPassed();
          }
        }
      }
      catch (System.Exception e)
      {
        notifier.TestFailed(e);
      }
    }
  }
}
