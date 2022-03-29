using System;
using System.IO;

namespace Revit.IFC.Common.Utility
{
   public class HfbDebugLogger
   {
      private FileStream fs;
      private StreamWriter writer;

      public HfbDebugLogger(string folder, string filename)
      {
         fs = new FileStream(Path.Combine(folder, filename), FileMode.Append);
         writer = new StreamWriter(fs);
      }

      public void Log(string msg)
      {
         writer.WriteLine(DateTime.Now.ToString("yyyy-MM-dd_HHmmss") + ": " + msg);
      }

      public void Close()
      {
         writer.Close();
         fs.Dispose();
      }
   }
}
