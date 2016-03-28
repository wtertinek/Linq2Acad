using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2Acad
{
  public static class DbObjectsExtensions
  {
    private static void WrapInTransaction(DBObject source, Action<Transaction> action)
    {
      var tr = source.Database.TransactionManager.TopTransaction;
      var newTransaction = false;

      if (tr == null)
      {
        tr = source.Database.TransactionManager.StartOpenCloseTransaction();
        newTransaction = true;
      }

      action(tr);

      if (newTransaction)
      {
        tr.Commit();
        tr.Dispose();
      }
    }

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

                                        WrapInTransaction(source, tr =>
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

      if (typeof(T) == typeof(bool))
      {
        saveData(getResultBuffer(DxfCode.Bool));
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

    public static T GetData<T>(this DBObject source, string key)
    {
      if (source.ExtensionDictionary.IsNull)
      {
        throw new KeyNotFoundException();
      }

      var items = new TypedValue[0];

      WrapInTransaction(source, tr =>
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
                                    throw new KeyNotFoundException();
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

    public static void ForEach<T>(this IEnumerable<T> items, Action<T> action) where T : DBObject
    {
      foreach (var item in items)
      {
        Helpers.WriteWrap(item, () => action(item));
      }
    }

    public static IEnumerable<T> UpgradeOpen<T>(this IEnumerable<T> source) where T : DBObject
    {
      foreach (var item in source)
      {
        if (!item.IsWriteEnabled)
        {
          item.UpgradeOpen();
        }

        yield return item;
      }
    }

    public static IEnumerable<T> DowngradeOpen<T>(this IEnumerable<T> source) where T : DBObject
    {
      foreach (var item in source)
      {
        if (!item.IsReadEnabled)
        {
          item.DowngradeOpen();
        }

        yield return item;
      }
    }
  }
}
