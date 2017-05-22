using System;
using System.Collections.Generic;

using KSP;

namespace CSITools
{
    public class CSI_WeightTransfer : PartModule
    {
        [KSPField(isPersistant = true)]
        public int _childcount;
        private bool _trussattached = false;
        
        ///Set part physics significane Full
        public override void OnStart(StartState state)
        {
            part.physicalSignificance = Part.PhysicalSignificance.FULL;
        }
        
        ///check if part count changed, if changed check if truss attached and run 'WeightTransfer'
        public void Update()
        {
            var c = part.children.Count;
            if (c == _childcount)
                return;

            _childcount = c;
            _trussattached = IsTrussAttached();
            WeightTransfer();
        }
        
        /// check if truss module is present on attached parts
        private bool IsTrussAttached()
        {
            return false;
        }

        ///Set physics significance depending on presence of rear truss module (CSI_Truss)
        private void WeightTransfer()
        {
            if (_trussattached)
                part.physicalSignificance = Part.PhysicalSignificance.NONE;
            else
                part.physicalSignificance = Part.PhysicalSignificance.FULL;
        }
    }
}
