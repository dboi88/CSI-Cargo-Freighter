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

        public override void OnStart(StartState state)
        {
            part.physicalSignificance = Part.PhysicalSignificance.FULL;
        }

        public void Update()
        {
            var c = part.children.Count;
            if (c == _childcount)
                return;

            _childcount = c;
            _trussattached = IsTrussAttached();
            WeightTransfer();
        }

        private bool IsTrussAttached()
        {
            return false;
        }


        private void WeightTransfer()
        {
            if (_trussattached)
                part.physicalSignificance = Part.PhysicalSignificance.NONE;
            else
                part.physicalSignificance = Part.PhysicalSignificance.FULL;
        }
    }
}
