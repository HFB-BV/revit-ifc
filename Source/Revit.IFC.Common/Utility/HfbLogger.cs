using System.IO;
using System.Diagnostics;
using Autodesk.Revit.DB;


namespace Revit.IFC.Common.Utility
{

   public class HFBExportLogger
   {
      private FileStream fs;
      private StreamWriter writer;
      private Stopwatch sw;
      public HFBExportLogger(string filepath)
      {
         fs = new FileStream(filepath, FileMode.Append);
      }

      public HFBExportLogger(string folder, string filename)
      {
         fs = new FileStream(Path.Combine(folder, filename), FileMode.Append);
      }

      public void Initialize()
      {
         writer = new StreamWriter(fs);
         sw = new Stopwatch();
         sw.Start();

         // Print the header
         writer.WriteLine("Element ID, duration (ms), Element name, Category, Group ID");
      }

      public void Restart()
      {
         sw.Restart();
      }

      public void Update(Element element)
      {
         // We can retrieve the elapsed time without calling Stop() first.
         int duration = (int)sw.ElapsedMilliseconds;
         writer.WriteLine(element.Id.ToString() + "," + duration.ToString() + "," + element.Name + "," + element.Category.Name + "," + element.GroupId.ToString());
         sw.Restart();
      }

      public void Close()
      {
         writer.Close();
         fs.Dispose();
      }


   }
}