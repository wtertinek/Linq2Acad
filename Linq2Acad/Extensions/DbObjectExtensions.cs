using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2Acad
{
  /// <summary>
  /// Extension methods for instances of DbObject.
  /// </summary>
  public static class DbObjectExtensions
  {
    /// <summary>
    /// Save an object in the source object's extension dictionary.
    /// </summary>
    /// <typeparam name="T">The type of the object to store.</typeparam>
    /// <param name="source">The source object to write the object to.</param>
    /// <param name="key">A string that acts as the key in the extension dictionary.</param>
    /// <param name="data">The object to store.</param>
    /// <exception cref="System.Exception">Thrown when an AutoCAD error occurs.</exception>
    public static void SaveData<T>(this DBObject source, string key, T data)
    {
      Action<ResultBuffer> saveData = buffer =>
                                      {
                                        if (!source.IsWriteEnabled)
                                        {
                                          source.UpgradeOpen();
                                        }

                                        if (source.ExtensionDictionary.IsNull)
                                        {
                                          source.CreateExtensionDictionary();
                                        }

                                        Helpers.WrapInTransaction(source, tr =>
                                                                          {
                                                                            var dict = (DBDictionary)tr.GetObject(source.ExtensionDictionary, OpenMode.ForWrite);

                                                                            if (dict.Contains(key))
                                                                            {
                                                                              var xRecord = (Xrecord)tr.GetObject(dict.GetAt(key), OpenMode.ForWrite);
                                                                              xRecord.Data = buffer;
                                                                            }
                                                                            else
                                                                            {
                                                                              var xRecord = new Xrecord();
                                                                              xRecord.Data = buffer;
                                                                              dict.SetAt(key, xRecord);
                                                                              tr.AddNewlyCreatedDBObject(xRecord, true);
                                                                            }
                                                                          });
                                      };

      Func<DxfCode, ResultBuffer> getResultBuffer = code => new ResultBuffer(new[] { new TypedValue((int)code, data) });

      try
      {
        // TODO: Add further types for types

        if (typeof(T) == typeof(bool))
        {
          saveData(getResultBuffer(DxfCode.Bool));
        }
        else if (typeof(T) == typeof(double))
        {
          saveData(getResultBuffer(DxfCode.Real));
        }
        else if (typeof(T) == typeof(byte))
        {
          saveData(getResultBuffer(DxfCode.Int8));
        }
        else if (typeof(T) == typeof(char) ||
                 typeof(T) == typeof(short))
        {
          saveData(getResultBuffer(DxfCode.Int16));
        }
        else if (typeof(T) == typeof(int))
        {
          saveData(getResultBuffer(DxfCode.Int32));
        }
        else if (typeof(T) == typeof(long))
        {
          saveData(getResultBuffer(DxfCode.Int64));
        }
        else if (typeof(T) == typeof(string))
        {
          saveData(getResultBuffer(DxfCode.Text));
        }
        else
        {
          saveData(new ResultBuffer(Helpers.Serialize(data)
                                           .Select(a => new TypedValue((int)DxfCode.BinaryChunk, a))
                                           .ToArray()));
        }
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e);
      }
    }

    /// <summary>
    /// Reads an object from the source object's extension dictionary.
    /// </summary>
    /// <typeparam name="T">The type of the object to read.</typeparam>
    /// <param name="source">The source object to read the object from.</param>
    /// <param name="key">A string that acts as the key in the extension dictionary.</param>
    /// <exception cref="System.Exception">Thrown when an AutoCAD error occurs.</exception>
    /// <returns>The object in the extension dictionary.</returns>
    public static T GetData<T>(this DBObject source, string key)
    {
      if (source.ExtensionDictionary.IsNull)
      {
        throw new KeyNotFoundException();
      }

      try
      {
        var items = new TypedValue[0];

        Helpers.WrapInTransaction(source, tr =>
                                          {
                                            var dict = (DBDictionary)tr.GetObject(source.ExtensionDictionary, OpenMode.ForRead);

                                            if (dict.Contains(key))
                                            {
                                              var xRecord = (Xrecord)tr.GetObject(dict.GetAt(key), OpenMode.ForRead);
                                              items = xRecord.Data
                                                             .Cast<TypedValue>()
                                                             .ToArray();
                                            }
                                            else
                                            {
                                              throw Error.KeyNotFound(key);
                                            }
                                          });

        if (items.Length == 1 &&
            items[0].TypeCode != (int)DxfCode.BinaryChunk)
        {
          return (T)items[0].Value;
        }
        else
        {
          return Helpers.Deserialize<T>(items.SelectMany(i => (byte[])i.Value)
                                              .ToArray());
        }
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e);
      }
    }

    /// <summary>
    /// Returns true, if the source object has an entry with the given key in the extension dictionary.
    /// </summary>
    /// <param name="source">The source object to check.</param>
    /// <param name="key">A string that acts as the key in the extension dictionary.</param>
    /// <exception cref="System.Exception">Thrown when an AutoCAD error occurs.</exception>
    /// <returns>True, if the extension dictionary contains an entry with the given key.</returns>
    public static bool HasData(this DBObject source, string key)
    {
      try
      {
        if (source.ExtensionDictionary.IsNull)
        {
          return false;
        }
        else
        {
          var hasData = false;
          Helpers.WrapInTransaction(source, tr =>
                                            {
                                              var dict = (DBDictionary)tr.GetObject(source.ExtensionDictionary, OpenMode.ForRead);
                                              hasData = dict.Contains(key);
                                            });

          return hasData;
        }
      }
      catch (Exception e)
      {
        throw Error.AutoCadException(e);
      }
    }
  }
}
