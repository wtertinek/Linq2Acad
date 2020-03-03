using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;

namespace Linq2Acad
{
  [DebuggerStepThrough]
  internal static class Require
  {
    public static void NewlyCreated<T>(T item, string parameterName) where T : DBObject
    {
      if (!item.ObjectId.IsNull)
      {
        throw new Exception($"{typeof(T).Name} {parameterName} must be newly created");
      }
    }

    public static void DifferentOrigin(Database currentDatabase, IEnumerable<DBObject> dBObjects, string name)
    {
      if (dBObjects.Any(o => o.Database == currentDatabase))
      {
        throw new Exception($"{name} must be from a different database");
      }
    }

    public static void NameDoesNotExists<T>(bool contains, string name)
    {
      if (contains)
      {
        throw new Exception($"{typeof(T).Name} with name '{name}' already exists");
      }
    }

    public static void NameExists<T>(bool contains, string name)
    {
      if (!contains)
      {
        throw new Exception($"{typeof(T).Name} with name '{name}' does not exist exists");
      }
    }

    public static void IsValidSymbolName(string name, string parameterName)
    {
      StringNotEmpty(name, parameterName);

      try
      {
        SymbolUtilityServices.ValidateSymbolName(name, false);
      }
      catch
      {
        throw new ArgumentException($"'{name}' is not a valid name", parameterName);
      }
    }

    public static void ElementsNotNull<T>(IEnumerable<T> enumerable, string parameterName) where T : class
    {
      ParameterNotNull(enumerable, parameterName);

      if (enumerable.Any(e => e == null))
      {
        throw new Exception($"{parameterName} must not contain null values");
      }
    }

    public static void FileExists(string fileName, string parameterName)
    {
      ParameterNotNull(fileName, parameterName);

      if (!File.Exists(fileName))
      {
        throw new FileNotFoundException($"File {fileName} does not exist", fileName);
      }
    }

    public static void ValidArrayIndex(int index, int arrayLength, string parameterName)
    {
      if (!(index >= 0 && index < arrayLength))
      {
        throw new IndexOutOfRangeException($"Parameter {parameterName} is out of range");
      }
    }

    public static void ObjectNotNull<T>(T obj, string message) where T : class
    {
      if (obj == null)
      {
        throw new Exception(message);
      }
    }

    public static void ParameterNotNull<T>(T obj, string parameterName) where T : class 
    {
      if (obj == null)
      {
        throw new ArgumentNullException(parameterName);
      }
    }

    public static void StringNotEmpty(string str, string parameterName)
    {
      if (string.IsNullOrEmpty(str))
      {
        throw new ArgumentException($"Parameter {parameterName} must not be empty", parameterName);
      }
    }

    public static void ElementsValid(IEnumerable<ObjectId> objectIds, string parameterName)
    {
      ParameterNotNull(objectIds, parameterName);

      if (objectIds.Any(id => !id.IsValid))
      {
        throw new Exception($"All {parameterName} must be valid");
      }
    }

    public static void IsValid(ObjectId objectId, string parameterName)
    {
      if (!objectId.IsValid)
      {
        throw new ArgumentException($"ObjectId {parameterName} must be valid", parameterName);
      }
    }

    public static void NotDisposed(bool disposed, string className, string objectName = null)
    {
      if (disposed)
      {
        if (objectName != null)
        {
          throw new ObjectDisposedException(objectName, $"Object {objectName} of type {className} is disposed");
        }
        else
        {
          throw new ObjectDisposedException(className, $"Instance of type {className} is disposed");
        }
      }
    }

    public static void IsTrue(bool condition, string message)
    {
      if (!condition)
      {
        throw new Exception(message);
      }
    }
  }
}
