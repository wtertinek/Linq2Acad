using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections;
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
      Require.ParameterNotNull(source, nameof(source));

      void saveData(ResultBuffer buffer)
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
      }

      ResultBuffer getResultBuffer(DxfCode code)
        => new ResultBuffer(new[] { new TypedValue((int)code, data) });

      // TODO: Add further types

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
      return GetData<T>(source, key, false);
    }

    /// <summary>
    /// Reads an object from the source object's extension dictionary or from XData.
    /// </summary>
    /// <typeparam name="T">The type of the object to read.</typeparam>
    /// <param name="source">The source object to read the object from.</param>
    /// <param name="key">If parameter <paramref name="useXData"/> is true, this string is the name of the RegApp to read the data from. If parameter <paramref name="useXData"/> is false, this string acts as the key in the extension dictionary.</param>
    /// <param name="useXData">True, if data should be read from the source object's XData. False, if data should be read from the source object's extension dictionary.</param>
    /// <exception cref="System.Exception">Thrown when an AutoCAD error occurs.</exception>
    /// <returns>The object in the extension dictionary.</returns>
    public static T GetData<T>(this DBObject source, string key, bool useXData)
    {
      Require.ParameterNotNull(source, nameof(source));
      Require.StringNotEmpty(key, nameof(key));

      if (useXData)
      {
        return GetFromXData<T>(source, key);
      }
      else
      {
        return GetFromXExtensionDictionary<T>(source, key);
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
    private static T GetFromXExtensionDictionary<T>(DBObject source, string key)
    {
      if (source.ExtensionDictionary.IsNull)
      {
        throw new KeyNotFoundException();
      }

      var items = new List<TypedValue>();

      Helpers.WrapInTransaction(source,
                                tr =>
                                {
                                  var dict = (DBDictionary)tr.GetObject(source.ExtensionDictionary, OpenMode.ForRead);

                                  Require.NameExists<T>(dict.Contains(key), key);

                                  var xRecord = (Xrecord)tr.GetObject(dict.GetAt(key), OpenMode.ForRead);
                                  items = xRecord.Data
                                                 .Cast<TypedValue>()
                                                 .ToList();
                                });

      if (items.Count == 1 &&
          (items[0].TypeCode != (int)DxfCode.BinaryChunk ||
           typeof(T).Equals(typeof(byte[]))))
      {
        return (T)items[0].Value;
      }
      else
      {
        return Helpers.Deserialize<T>(items.SelectMany(i => (byte[])i.Value)
                                           .ToArray());
      }
    }

    /// <summary>
    /// Reads an object from the source object's XData.
    /// </summary>
    /// <typeparam name="T">The type of the object to read.</typeparam>
    /// <param name="source">The source object to read the object from.</param>
    /// <param name="regAppName">The name of the RegApp to read the data from.</param>
    /// <exception cref="System.Exception">Thrown when an AutoCAD error occurs.</exception>
    /// <returns>The object in the extension dictionary.</returns>
    private static T GetFromXData<T>(DBObject source, string regAppName)
    {
      var resultBuffer = source.GetXDataForApplication(regAppName);

      Require.ObjectNotNull(resultBuffer, $"RegApp '{regAppName}' not found");

      using (resultBuffer)
      {
        var xData = resultBuffer.Cast<TypedValue>()
                                .Skip(1)
                                .ToArray();

        var targetType = typeof(T);
        var enumerable = typeof(T).GetInterface(typeof(IEnumerable<>).Name);

        if (typeof(T).Equals(typeof(string)))
        {
          enumerable = null;
        }
        else if (enumerable != null)
        {
          targetType = enumerable.GenericTypeArguments
                                 .FirstOrDefault();
        }

        var tmpValues = new List<object>();
        CollectValues(xData, tmpValues, targetType);
        var values = tmpValues.ToArray();

        if (values.Length == 1 && values[0] is object[])
        {
          values = values[0] as object[];
        }

        if (enumerable != null)
        {
          if (targetType == typeof(object))
          {
            return (T)(object)values;
          }
          else
          {
            var newValues = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(targetType));

            foreach (var value in values)
            {
              if (typeof(T).Equals(typeof(Vector3d)))
              {
                var tmp = (Point3d)value;
                newValues.Add(new Vector3d(tmp.X, tmp.Y, tmp.Z));
              }
              else
              {
                newValues.Add(value);
              }
            }

            return (T)newValues;
          }
        }
        else if (values.Length == 1)
        {
          return (T)values[0];
        }
        else
        {
          if (typeof(T).Equals(typeof(object)))
          {
            return (T)(object)values;
          }
          else
          {
            throw new Exception("XData contains multiple: " + values.GetType().Name + " cannot be converted to " + typeof(T).Name);
          }
        }
      }
    }

    /// <summary>
    /// Converts a collection if XData values into a list of objects.
    /// </summary>
    /// <param name="input">The XData values to convert.</param>
    /// <param name="output">The result of the conversion.</param>
    /// <param name="targetType">The underlying data type of the resulting list.</param>
    /// <returns>The number of values that have been converted.</returns>
    private static int CollectValues(IReadOnlyList<TypedValue> input, IList output, Type targetType)
    {
      for (int i = 0; i < input.Count; i++)
      {
        switch (input[i].TypeCode)
        {
          case (int)DxfCode.ExtendedDataRegAppName:
            throw new Exception("Error in XData: There can't be another RegApp inside a RegApp");
          case (int)DxfCode.ExtendedDataAsciiString:
          case (int)DxfCode.ExtendedDataLayerName:
          case (int)DxfCode.ExtendedDataHandle:
          case (int)DxfCode.ExtendedDataReal:
          case (int)DxfCode.ExtendedDataDist:
          case (int)DxfCode.ExtendedDataScale:
          case (int)DxfCode.ExtendedDataInteger32:
          case (int)DxfCode.ExtendedDataBinaryChunk:
            try
            {
              output.Add(input[i].Value);
            }
            catch (InvalidCastException)
            {
              throw new Exception($"DxfCode.{(DxfCode)input[i].TypeCode} cannot be converted to type {targetType.Name}");
            }
            break;
          case (int)DxfCode.ExtendedDataXCoordinate:
          case (int)DxfCode.ExtendedDataWorldXCoordinate:
          case (int)DxfCode.ExtendedDataWorldXDisp:
          case (int)DxfCode.ExtendedDataWorldXDir:
            try
            {
              if (targetType.Equals(typeof(Vector3d)))
              {
                var tmp = (Point3d)input[i].Value;
                output.Add(new Vector3d(tmp.X, tmp.Y, tmp.Z));
              }
              else
              {
                output.Add(input[i].Value);
              }
            }
            catch (InvalidCastException)
            {
              throw new Exception($"DxfCode.{(DxfCode)input[i].TypeCode} cannot be converted to type {targetType.Name}");
            }
            break;
          case (int)DxfCode.ExtendedDataControlString:
            if ((string)input[i].Value == "{")
            {
              var subItems = new List<object>();
              var subItemsConsumed = CollectValues(input.Skip(i + 1).ToArray(), subItems, targetType);
              i += subItemsConsumed;
              output.Add(subItems.ToArray());
            }
            else if ((string)input[i].Value == "}")
            {
              return i + 1;
            }
            break;
          default:
            throw new Exception("DxfCode." + ((DxfCode)input[i].TypeCode) + " is not a valid XData DxfCode");
        }
      }

      return input.Count;
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
      return HasData(source, key, false);
    }

    /// <summary>
    /// Returns true, if the source object has an entry with the given key in the extension dictionary.
    /// </summary>
    /// <param name="source">The source object to check.</param>
    /// <param name="key">If parameter <paramref name="useXData"/> is true, this string is the name of the RegApp to read the data from. If parameter <paramref name="useXData"/> is false, this string acts as the key in the extension dictionary.</param>
    /// <param name="useXData">True, if data should be read from the source object's XData. False, if data should be read from the source object's extension dictionary.</param>
    /// <exception cref="System.Exception">Thrown when an AutoCAD error occurs.</exception>
    /// <returns>True, if the extension dictionary contains an entry with the given key.</returns>
    public static bool HasData(this DBObject source, string key, bool useXData)
    {
      Require.ParameterNotNull(source, nameof(source));
      Require.StringNotEmpty(key, nameof(key));

      if (useXData)
      {
        var resultBuffer = source.GetXDataForApplication(key);

        if (resultBuffer != null)
        {
          resultBuffer.Dispose();
          return true;
        }
        else
        {
          return false;
        }
      }
      else
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
    }

    /// <summary>
    /// Removes the entry with the given key from the extension dictionary.
    /// </summary>
    /// <param name="source">The source object to check.</param>
    /// <param name="key">A string that acts as the key in the extension dictionary.</param>
    /// <exception cref="System.Exception">Thrown when an AutoCAD error occurs.</exception>
    /// <exception cref="System.Collections.Generic.KeyNotFoundException">Thrown when the given key is not found.</exception>
    public static void RemoveData(this DBObject source, string key)
    {
      Require.ParameterNotNull(source, nameof(source));
      Require.StringNotEmpty(key, nameof(key));

      Require.IsValid(source.ExtensionDictionary, nameof(source.ExtensionDictionary));

      Helpers.WrapInTransaction(source,
                                tr =>
                                {
                                  var dict = (DBDictionary)tr.GetObject(source.ExtensionDictionary, OpenMode.ForWrite);

                                  Require.NameExists<DBObject>(dict.Contains(key), key);

                                  dict.Remove(key);
                                });
    }
  }
}
