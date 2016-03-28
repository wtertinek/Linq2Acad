using System;
using System.IO;

namespace Linq2Acad
{
  static class Error
  {
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
  }
}
