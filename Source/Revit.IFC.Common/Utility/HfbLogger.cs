using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using Autodesk.Revit.DB;
using System;

namespace Revit.IFC.Common.Utility
{

   public class HFBExportLogger
   {
      private FileStream fs;
      private StreamWriter writer;
      private Stopwatch sw;
      private Document doc;

      public HFBExportLogger(Document document, string filepath)
      {
         fs = new FileStream(filepath, FileMode.Append);
         doc = document;
      }

      public HFBExportLogger(Document document, string folder, string filename)
      {
         fs = new FileStream(Path.Combine(folder, filename), FileMode.Append);
         doc = document;
      }

      public void Initialize()
      {
         writer = new StreamWriter(fs);
         sw = new Stopwatch();
         sw.Start();

         // Print the header
         writer.WriteLine("Element ID, duration (ms), Element name, Category, Group ID, Family, IFC Entity Type");
      }

      public void Restart()
      {
         sw.Restart();
      }

      private string GetFamily(Element element)
      {
         string famstr;
         try
         {
            FamilyInstance faminst = element as FamilyInstance;
            Family fam = faminst.Symbol.Family;
            famstr = fam.Name;
         }
         catch (Exception ex)
         {
            famstr = "";
         }
         return famstr;
      }

      private string GetIFCEntityType(Element element)
      {
         string ifctype = "";
         Element typeEle = doc.GetElement(element.GetTypeId());

         if (typeEle != null)
         {
            List<Parameter> paramlist = typeEle.GetParameters("IfcExportAs") as List<Parameter>;
            if (paramlist.Count > 0)
            {
               ifctype = paramlist[0].AsString();
            }
         }
         return ifctype;
      }

      public void Log(Element element)
      {
         int duration = (int)sw.ElapsedMilliseconds;
         string famstr = GetFamily(element);
         string ifctype = GetIFCEntityType(element);

         string logstr = element.Id.ToString() + "," + duration.ToString() + "," + element.Name + "," +
                         element.Category.Name + "," + element.GroupId.ToString() + "," + famstr + "," + ifctype;

         writer.WriteLine(logstr);
      }

      public void Update(Element element)
      {
         Log(element);
         sw.Restart();
      }

      public void Close()
      {
         writer.Close();
         fs.Dispose();
      }

   }
}