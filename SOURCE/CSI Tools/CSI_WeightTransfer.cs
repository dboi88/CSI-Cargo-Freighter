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
            part.physicalSignificance = Part.PhysicalSignificance.FULL;
        }

        private int counter = 0;
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