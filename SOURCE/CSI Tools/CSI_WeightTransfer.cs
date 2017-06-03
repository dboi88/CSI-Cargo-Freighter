using System;
using System.Collections.Generic;
using KSP;

namespace CSITools
{
    public class CSI_WeightTransfer : PartModule
    {
        [KSPField(isPersistant = true)]
        private bool _trussattached = false;
        
        ///Set part physics significane Full
        public override void OnStart(StartState state)
        {   
            /// Set part significance to Full
            part.physicalSignificance = Part.PhysicalSignificance.FULL;
        }
        
        /// create counter variable
        private int counter = 0;
        ///runs automtacially on every physics tick
        public void FixedUpdate()
        {

            if (counter < 100)
            {
                counter++;
                return;
            }
            else
                Tools.LogFormatted("Run trusschecker");
                trusschecker();
                counter = 0;
        }
        
        /// Checks to see if the truss is attached if Parent != Null, Amend _trussattached then runs weight transfer
        private void trusschecker()
        {
            if (part.parent != null)
            {
                if (part.parent.FindModuleImplementing<CSI_Truss>() == true)
                {
                    _trussattached = true;
                    WeightTransfer();
                }
            }
            else
            {
                _trussattached = false;
                WeightTransfer();
            }
        }

        ///Set physics significance depending on presence of rear truss module (CSI_Truss)
        private void WeightTransfer()
        {
            if (_trussattached == true)
            {
                part.physicalSignificance = Part.PhysicalSignificance.NONE;
                Tools.LogFormatted("Physics Set NONE");
            }
            if (_trussattached == false)
            {
                part.physicalSignificance = Part.PhysicalSignificance.FULL;
                Tools.LogFormatted("Physics Set FULL");
            }
        }
    }
}
//private void onVesselWasModified(Vessel v)
//{
//    if (v == vessel)
//    {
//        StartCoroutine(UpdateNetwork());
//    }
//}
