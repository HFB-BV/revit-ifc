using System.Collections.Generic;
using Autodesk.Revit.DB;

namespace BIM.IFC.Export.UI
{
   /// <summary>
   /// Represents the data related to the IFC export process for linked instances related to a link in Revit.
   /// This class serves as a container for both successful export information and potential export issues.
   /// </summary>
   public class IFCLinkExportSummary
   {
      // Reasons we can't export:
      // 1. The path for the linked instance doesn't exist.
      // 2. Couldn't create a temporary document for exporting the linked instance.
      // 3. The document for the linked instance can't be found.
      // 4. The linked instance is mirrored, non-conformal, or scaled.

      public string LinkFilePath { get; }
      public int NumExportedLinkInstances = 0;
      public IList<string> linkFileNames = new List<string>();
      public IList<ElementId> exportedInstanceIds = new List<ElementId>();

      // Fields required for the exporter - do not touch
      public IList<string> pathDoesntExist = new List<string>();
      public IList<string> noTempDoc = new List<string>();
      public IList<ElementId> cantFindDoc = new List<ElementId>();
      public IList<ElementId> nonConformalInst = new List<ElementId>();
      public IList<ElementId> scaledInst = new List<ElementId>();
      public IList<ElementId> instHasReflection = new List<ElementId>();

      public IFCLinkExportSummary(string linkFilePath)
      {
         LinkFilePath = linkFilePath;
      }

      public int GetNumBadInstances()
      {
         return pathDoesntExist.Count + noTempDoc.Count + cantFindDoc.Count + nonConformalInst.Count
       + scaledInst.Count + instHasReflection.Count;
      }

   }
}