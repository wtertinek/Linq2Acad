using System;
using System.Collections.Generic;
using System.IO;

namespace Linq2Acad
{
  /// <summary>
  /// Helper class for throwing exceptions.
  /// </summary>
  internal static class Error
  {
    /// <summary>
    /// Creates a generic exception of type System.Exception.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <returns>A new instance of System.Exception.</returns>
    public static Exception Generic(string message)
    {
      return new Exception(message);
    }

    /// <summary>
    /// Creates a generic exception of type System.Exception.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="innerException">The that is the cause of the current exception.</param>
    /// <returns>A new instance of System.Exception.</returns>
    public static Exception Generic(string message, Exception innerException)
    {
      return new Exception(message, innerException);
    }

    /// <summary>
    /// Creates an exception of type System.ArgumentNullException.
    /// </summary>
    /// <param name="paramName">The name of the parameter that caused the exception.</param>
    /// <returns>A new instance of System.ArgumentNullException.</returns>
    public static Exception ArgumentNull(string paramName)
    {
      return new ArgumentNullException(paramName);
    }

    /// <summary>
    /// Creates an exception of type System.Exception that indicates that the given name is valid.
    /// </summary>
    /// <param name="name">The name that is invalid.</param>
    /// <returns>A new instance of System.Exception.</returns>
    public static Exception InvalidName(string name)
    {
      return new Exception(name + " is not a valid name");
    }

    /// <summary>
    /// Creates an excpetion of type System.FileNotFoundException.
    /// </summary>
    /// <param name="fileName">The full file name of the file that was not found.</param>
    /// <returns>A new instance of System.FileNotFoundException.</returns>
    public static Exception FileNotFound(string fileName)
    {
      return new FileNotFoundException("File " + fileName + " not found", fileName);
    }

    /// <summary>
    /// Creates an exception of type System.KeyNotFoundException.
    /// </summary>
    /// <param name="key">The key that was not found.</param>
    /// <returns>A new instance of System.KeyNotFoundException.</returns>
    public static Exception KeyNotFound(string key)
    {
      return new KeyNotFoundException("No element with key " + key + " found");
    }

    /// <summary>
    /// Creates an exception of type System.IOException.
    /// </summary>
    /// <param name="message">A message that describes the error.</param>
    /// <returns>A new instance of System.IOException.</returns>
    public static Exception IO(string message)
    {
      return new IOException(message);
    }

    /// <summary>
    /// Creates an exception of type System.Exception that indicates that no acctive document exists.
    /// </summary>
    /// <returns>A new instance of System.Exception.</returns>
    public static Exception NoActiveDocument()
    {
      return new Exception("No active document");
    }

    /// <summary>
    /// Creates an exception of type InvalidObjectException that indicates that the given object is invalid.
    /// </summary>
    /// <param name="objectTypeName">The type name of the given object.</param>
    /// <returns>A new instance of InvalidObjectException.</returns>
    public static InvalidObjectException InvalidObject(string objectTypeName)
    {
      return new InvalidObjectException(objectTypeName);
    }

    /// <summary>
    /// Creates an exception of type System.Exception that indicates that an AutoCAD exception occured.
    /// </summary>
    /// <param name="innerException">The that is the cause of the current exception.</param>
    /// <returns>A new instance of System.Exception.</returns>
    public static Exception AutoCadException(Exception innerException)
    {
      return AutoCadException(innerException, "An AutoCAD error occured");
    }

    /// <summary>
    /// Creates an exception of type System.Exception that indicates that an AutoCAD exception occured.
    /// </summary>
    /// <param name="innerException">The that is the cause of the current exception.</param>
    /// <param name="message">The message that describes the error.</param>
    /// <returns>A new instance of System.Exception.</returns>
    public static Exception AutoCadException(Exception innerException, string message)
    {
      // TODO: We can add some code here to make sense of AutoCAD exception messages like eWasOpenForWrite

      if (innerException.Message == "eWasOpenForWrite")
      {
        // TODO: Add further context information
        return new Exception(message, innerException);
      }
      else
      {
        return new Exception(message, innerException);
      }
    }

    /// <summary>
    /// Creates an exception of type System.IndexOutOfRangeException that indicates that an index was out of bounds.
    /// </summary>
    /// <param name="paramName">The name of the parameter that is out of bounds.</param>
    /// <param name="upperBound">The upper bound that has been exceeded.</param>
    /// <returns>A new instance of System.IndexOutOfRangeException.</returns>
    public static IndexOutOfRangeException IndexOutOfRange(string paramName, int upperBound)
    {
      return new IndexOutOfRangeException(paramName + " has to be >= 0 and <= " + upperBound);
    }
  }

  /// <summary>
  /// An exception that indicates that an object is invalid.
  /// </summary>
  internal class InvalidObjectException : Exception
  {
    public InvalidObjectException(string objectTypeName)
      : base("The given " + objectTypeName + " is invalid")
    {
    }
  }
}
