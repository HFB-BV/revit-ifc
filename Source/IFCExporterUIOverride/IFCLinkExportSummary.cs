using System.Collections.Generic;
using Autodesk.Revit.DB;

namespace BIM.IFC.Export.UI
{
   /// <summary>
   /// Represents the data related to the IFC export process for linked instances related to a link in Revit.
   /// This class serves as a container for successful export information.
   /// </summary>
   public class IFCLinkExportSummary
   {
      public string LinkFilePath { get; }
      public int NumExportedLinkInstances = 0;
      public IList<string> linkFileNames = new List<string>();
      public IList<ElementId> exportedInstanceIds = new List<ElementId>();

      public IFCLinkExportSummary(string linkFilePath)
      {
         LinkFilePath = linkFilePath;
      }
   }
}