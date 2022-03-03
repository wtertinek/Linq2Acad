using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Linq2Acad.Tests
{
  public static class DwgExtractor
  {
    public static ExtractedFileInfo ExtractDwgFile(string resourceFileName)
    {
      var assembly = Assembly.GetExecutingAssembly();
      var resourceName = "Linq2Acad.Tests.Resources." + resourceFileName;
      var dwgFileName = Path.GetTempFileName()
                            .Replace(".tmp", ".dwg");
      using (var sourceStream = assembly.GetManifestResourceStream(resourceName))
      using (var sourceReader = new BinaryReader(sourceStream))
      using (var targetStream = new FileStream(dwgFileName, FileMode.Create))
      using (var targetWriter = new BinaryWriter(targetStream))
      {
        byte[] rawData = new byte[sourceStream.Length];
        sourceStream.Read(rawData, 0, rawData.Length);
        targetWriter.Write(rawData);
      }

      return new ExtractedFileInfo(dwgFileName);
    }

    public class ExtractedFileInfo : IDisposable
    {
      public ExtractedFileInfo(string fileName)
      {
        FileName = fileName;
      }

      public string FileName { get; private set; }

      public void Dispose()
      {
        File.Delete(FileName);
      }
    }
  }
}
