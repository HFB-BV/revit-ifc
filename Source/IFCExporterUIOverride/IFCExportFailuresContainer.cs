using System.Collections.Generic;
using Autodesk.Revit.DB;

namespace BIM.IFC.Export.UI
{
   public class IFCExportFailuresContainer
   {
      // Reasons we can't export:
      // 1. The path for the linked instance doesn't exist.
      // 2. Couldn't create a temporary document for exporting the linked instance.
      // 3. The document for the linked instance can't be found.
      // 4. The linked instance is mirrored, non-conformal, or scaled.

      public IList<string> pathDoesntExist = new List<string>();
      public IList<string> noTempDoc = new List<string>();
      public IList<ElementId> cantFindDoc = new List<ElementId>();
      public IList<ElementId> nonConformalInst = new List<ElementId>();
      public IList<ElementId> scaledInst = new List<ElementId>();
      public IList<ElementId> instHasReflection = new List<ElementId>();

      public IFCExportFailuresContainer() { }

      public int GetNumBadInstances()
      {
         return pathDoesntExist.Count + noTempDoc.Count + cantFindDoc.Count + nonConformalInst.Count
       + scaledInst.Count + instHasReflection.Count;
      }

   }
}
