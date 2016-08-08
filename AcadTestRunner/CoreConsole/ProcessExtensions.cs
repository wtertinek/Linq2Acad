using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Diagnostics
{
  internal static class ProcessExtensions
  {
    public static int StartAndWait(this Process process, string fileName, Action<string> writeOutput)
    {
      return StartAndWait(process, fileName, null, null, writeOutput);
    }

    public static int StartAndWait(this Process process, string fileName, string arguments, Action<string> writeOutput)
    {
      return StartAndWait(process, fileName, arguments, null, writeOutput);
    }

    public static int StartAndWait(this Process process, string fileName, string arguments, string workingDirectory, Action<string> writeOutput)
    {
      process.StartInfo = new ProcessStartInfo()
                          {
                            FileName = fileName,
                            UseShellExecute = false,
                            RedirectStandardOutput = true,
                            RedirectStandardError = true
                          };

      if (arguments != null)
      {
        process.StartInfo.Arguments = arguments;
      }

      if (workingDirectory != null)
      {
        process.StartInfo.WorkingDirectory = workingDirectory;
      }

      process.OutputDataReceived += (sender, e) => { if (e.Data != null) { writeOutput(e.Data); } };
      process.ErrorDataReceived += (sender, e) => { if (e.Data != null) { writeOutput(e.Data); } };

      process.Start();
      process.BeginOutputReadLine();
      process.BeginErrorReadLine();
      process.WaitForExit();

      return process.ExitCode;
    }
  }
}
