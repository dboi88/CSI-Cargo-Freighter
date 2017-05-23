using System;
using System.Collections.Generic;
using KSP;

namespace CSITools
{
    public class Class1 : PartModule
    {
        ///Set part physics significane Full
        public override void OnStart(StartState state)
        {
            /// Set part significance to Full
            part.physicalSignificance = Part.PhysicalSignificance.FULL;
        }

    }
}
