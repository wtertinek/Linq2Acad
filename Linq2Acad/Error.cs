using System;
using System.Collections.Generic;
using System.IO;

namespace Linq2Acad
{
  static class Error
  {
    public static Exception Generic(string message)
    {
      return new Exception(message);
    }

    public static Exception ArgumentNull(string paramName)
    {
      return new ArgumentNullException(paramName);
    }

    public static Exception InvalidName(string name)
    {
      return new Exception(name + " is not a valid name");
    }

    public static Exception FileNotFound(string fileName)
    {
      return new FileNotFoundException("File " + fileName + " not found", fileName);
    }

    public static Exception KeyNotFound(string key)
    {
      return new KeyNotFoundException("No element with key " + key + " found");
    }

    public static Exception IO(string message)
    {
      return new IOException(message);
    }
  }
}
