using System;
using System.Collections.Generic;
using KSP;

namespace CSITools
{
    public class CSI_WeightTransfer : PartModule
    {
        [KSPField(isPersistant = true)]
        private bool _trussattached = false;
        private bool _physicalSignificance = true;

        ///Set part physics significane Full
        public override void OnStart(StartState state)
        {   
            /// Set part significance to Full
            part.physicalSignificance = Part.PhysicalSignificance.FULL;
            GameEvents.onPartPack.Add(onPartPack);
            GameEvents.onPartUnpack.Add(onPartUnpack);
        }
        
        /// create counter variable
        private int counter = 900;
        ///runs automtacially on every physics tick
        public void FixedUpdate()
        {

            if (counter < 1000)
            {
                counter++;
                return;
            }
            else
            { 
                Tools.LogFormatted("Run trusschecker");
                trusschecker();
                counter = 900;
            }
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
                else
                {
                    _trussattached = false;
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
            if (_trussattached == true && _physicalSignificance == true)
            {
                part.physicalSignificance = Part.PhysicalSignificance.NONE;
                _physicalSignificance = false;
                Tools.LogFormatted("Physics Set NONE");
            }
            if (_trussattached == false && _physicalSignificance == false)
            {
                part.physicalSignificance = Part.PhysicalSignificance.FULL;
                _physicalSignificance = true;
                Tools.LogFormatted("Physics Set FULL");
            }
        }

        private void onPartPack(Part thePart)
        {
            part.physicalSignificance = Part.PhysicalSignificance.FULL;
            _physicalSignificance = true;
            Tools.LogFormatted("onPartPack Physics Set FULL");
            counter = 0;
        }

        private void onPartUnpack(Part thePart)
        {
            Tools.LogFormatted("Run trusschecker OnPartUnpack");
            part.physicalSignificance = Part.PhysicalSignificance.FULL;
            _physicalSignificance = true;
            counter = 0;
        }

        private void onDestroy()
        {
            GameEvents.onPartPack.Remove(onPartPack);
            GameEvents.onPartUnpack.Remove(onPartUnpack);
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
