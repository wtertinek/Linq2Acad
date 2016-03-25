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
  static class Helpers
  {
    public static void WriteWrap<T>(T item, Action action, bool keepUpgraded = false) where T : DBObject
    {
      WriteWrap<T, object>(item, () => { action(); return null; }, keepUpgraded);
    }

    public static TResult WriteWrap<T, TResult>(T item, Func<TResult> function, bool keepUpgraded = false) where T : DBObject
    {
      bool changed = false;

      if (!item.IsWriteEnabled)
      {
        changed = true;
        item.UpgradeOpen();
      }

      TResult result = function();

      if (!keepUpgraded && changed)
      {
        item.DowngradeOpen();
      }

      return result;
    }

    private const int ChunkSize = 127;

    public static IEnumerable<byte[]> Serialize(object data)
    {
      var formatter = new BinaryFormatter();

      using (var memoryStream = new MemoryStream())
      {
        formatter.Serialize(memoryStream, data);

        memoryStream.Position = 0;
        var fullChunks = (int)memoryStream.Length / ChunkSize;

        Func<int, byte[]> read = size =>
                                 {
                                   var chunk = new byte[size];

                                   if (memoryStream.Read(chunk, (int)memoryStream.Position, size) != size)
                                   {
                                     throw new IOException("Error reading from MemoryStream");
                                   }

                                   return chunk;
                                 };
        
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
