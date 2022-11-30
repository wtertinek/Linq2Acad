using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Linq2Acad
{
  /// <summary>
  /// Container class for static helper methods.
  /// </summary>
  internal static class Helpers
  {
    /// <summary>
    /// Uses the top transaction in the transaction manager and performs the given action using the transaction.
    /// If now transaction is avialable, a new transaction is started.
    /// </summary>
    /// <param name="source">The source object that acts as the transcation manager provider.</param>
    /// <param name="action">The action to execute.</param>
    public static void WrapInTransaction(DBObject source, Action<Transaction> action)
    {
      var tr = source.Database.TransactionManager.TopTransaction;
      var newTransaction = false;

      if (tr == null)
      {
        tr = source.Database.TransactionManager.StartTransaction();
        newTransaction = true;
      }

      try
      {
        action(tr);
      }
      catch
      {
        throw;
      }
      finally
      {
        if (newTransaction)
        {
          tr.Commit();
          tr.Dispose();
        }
      }
    }

    /// <summary>
    /// Performs a write operation on a given DBObject. If the object is not write enabled, UpgradeOpen is
    /// called on the object and the action performed.
    /// </summary>
    /// <typeparam name="T">The actual type of the item.</typeparam>
    /// <param name="element">The element to perform the action on.</param>
    /// <param name="action">The action to perfrom.</param>
    /// <param name="keepUpgraded">True, if the item should stay write enabled after the action has been performed.</param>
    public static void WriteWrap<T>(T element, Action action, bool keepUpgraded = false) where T : DBObject
      => WriteWrap<T, object>(element, () => { action(); return null; }, keepUpgraded);

    /// <summary>
    /// Performs a write operation on a given DBObject. If the object is not write enabled, UpgradeOpen is
    /// called on the object and the action performed.
    /// </summary>
    /// <typeparam name="T">The actual type of the item.</typeparam>
    /// <typeparam name="TResult">The result type of the write operation.</typeparam>
    /// <param name="element">The element to perform the action on.</param>
    /// <param name="function">The function to call on the object.</param>
    /// <param name="keepUpgraded">True, if the item should stay write enabled after the action has been performed.</param>
    /// <returns>Returns the result of the write operation.</returns>
    public static TResult WriteWrap<T, TResult>(T element, Func<TResult> function, bool keepUpgraded = false) where T : DBObject
    {
      bool changed = false;

      if (!element.IsWriteEnabled)
      {
        changed = true;
        element.UpgradeOpen();
      }

      TResult result = function();

      if (!keepUpgraded && changed)
      {
        element.DowngradeOpen();
      }

      return result;
    }

    /// <summary>
    /// If an object is serialized into binaray, his the size for each binary chunk.
    /// </summary>
    private const int ChunkSize = 127;

    /// <summary>
    /// Serializes an object into a into a byte array and cuts the byte array into chunks of a 127 byte.
    /// </summary>
    /// <param name="data">The object to serialize.</param>
    /// <returns>The serialized object in chunks of 127 bytes each.</returns>
    public static IEnumerable<byte[]> Serialize(object data)
    {
      var formatter = new BinaryFormatter();

      using (var memoryStream = new MemoryStream())
      {
        formatter.Serialize(memoryStream, data);

        memoryStream.Position = 0;
        var fullChunks = (int)memoryStream.Length / ChunkSize;

        byte[] read(int size)
        {
          var chunk = new byte[size];

          if (memoryStream.Read(chunk, 0, size) != size)
          {
            throw new Exception("Error reading from MemoryStream");
          }

          return chunk;
        }

        for (int i = 0; i < fullChunks; i++)
        {
          yield return read(ChunkSize);
        }

        var remainder = (int)memoryStream.Length % ChunkSize;

        if (remainder != 0)
        {
          yield return read(remainder);
        }
      }
    }

    /// <summary>
    /// Deserializes an object from a byte array.
    /// </summary>
    /// <typeparam name="T">The type of the serialized object.</typeparam>
    /// <param name="data">The serialized representation of the object.</param>
    /// <returns>The deserialize object.</returns>
    public static T Deserialize<T>(byte[] data)
    {
      using (var memoryStream = new MemoryStream(data))
      {
        memoryStream.Position = 0;
        var formatter = new BinaryFormatter();
        return (T)formatter.Deserialize(memoryStream);
      }
    }
  }
}
