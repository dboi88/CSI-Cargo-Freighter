using System;
using System.Collections.Generic;
using KSP;
using UnityEngine;

namespace CSITools
{
    public class CSI_WeightTransfer : PartModule 
    {
        [KSPField]
        public bool autostrut = false;

        private bool _trussattached = false;
        public override void OnStart(StartState state)
        {
            GameEvents.onPartAttach.Add(onPartAttach);
            GameEvents.onPartCouple.Add(onPartCouple);
            GameEvents.onPartUndock.Add(onPartUndock);
            GameEvents.onPartUnpack.Add(onPartUnpack);
            GameEvents.onVesselWasModified.Add(onVesselWasModified);
            if (Tools._debug == true)
            {
                Tools.LogFormatted("CSI_WeightTransfer Started");
            }
            trusschecker();
        }

        private int attachcounter = 0;
        public void Update()
        {
            if (attachcounter == 0)
            {
                return;
            }
            if (attachcounter < 3)
            {
                attachcounter++;
                return;
            }
            else
            {
                if (Tools._debug == true)
                {
                    Tools.LogFormatted("Run Attach Counter");
                }
                trusschecker();
                attachcounter = 0;
            }
        }

        /// Checks to see if the truss is attached if Parent != Null, Amend _trussattached then runs weight transfer
        private void trusschecker()
        {
            if (part.parent != null)
            {
                if (Tools._debug == true)
                {
                    Tools.LogFormatted("Parent != null, parent = " + part.parent);
                }
                if (part.parent.FindModuleImplementing<CSI_Truss>() == true)
                {
                    if (Tools._debug == true)
                    {
                        Tools.LogFormatted("Parent contains CSI_Truss, parent = " + part.parent);
                    }
                    _trussattached = true;
                    WeightTransfer();
                }
                else
                {
                    if (Tools._debug == true)
                    {
                        Tools.LogFormatted("Parent does not contain CSI_Truss");
                    }
                    _trussattached = false;
                    WeightTransfer();
                }
            }
            else
            {
                if (Tools._debug == true)
                {
                    Tools.LogFormatted("Parent = null");
                }
                _trussattached = false;
                WeightTransfer();
            }
        }

        ///Set physics significance depending on presence of rear truss module (CSI_Truss)
        public void WeightTransfer()
        {
            if (_trussattached == true)
            {
                getparentworldcom();
                part.CoMOffset = kontainerlocal;
                if (autostrut)
                {
                    part.autoStrutMode = Part.AutoStrutMode.Grandparent;
                    part.UpdateAutoStrut();
                }
                if (Tools._debug == true)
                {
                    Tools.LogFormatted("COM Set Parent");
                }
            }
            if (_trussattached == false)
            {
                if (autostrut)
                {
                    part.autoStrutMode = Part.AutoStrutMode.Off;
                    part.UpdateAutoStrut();
                }
                part.CoMOffset = Vector3d.zero;
                if (Tools._debug == true)
                {
                    Tools.LogFormatted("COM Set Start");
                }
            }
        }

        private Vector3d worldParentPosition;
        private Vector3d localParentPosition;
        private Vector3d kontainerlocal;
        private Transform kontainertransform;
        private Transform parenttransform;

        private void getparentworldcom()
        {
            localParentPosition = part.parent.CoMOffset;
            parenttransform = part.parent.partTransform;

            worldParentPosition = parenttransform.TransformPoint(localParentPosition);

            kontainertransform = part.partTransform;

            kontainerlocal = kontainertransform.InverseTransformPoint(worldParentPosition);
        }

        private void onVesselStandardModification(Vessel vessel)
        {
            if (Tools._debug == true)
            {
                Tools.LogFormatted("onVesselStandardModification");
            }
            attachcounter = 1;
        }

        private void onPartAttach(GameEvents.HostTargetAction<Part, Part> action)
        {
            if (Tools._debug == true)
            {
                Tools.LogFormatted("onPartAttach");
            }
            attachcounter = 1;
        }

        private void onPartCouple(GameEvents.FromToAction<Part, Part> action)
        {
            if (Tools._debug == true)
            {
                Tools.LogFormatted("onPartCouple");
            }
            attachcounter = 1;
        }

        private void onPartUndock(Part part)
        {
            if (Tools._debug == true)
            {
                Tools.LogFormatted("onPartUndock");
            }
            attachcounter = 1;
        }
        private void onVesselWasModified(Vessel vessel)
        {
            if (Tools._debug == true)
            {
                Tools.LogFormatted("onVesselWasModified");
            }
            attachcounter = 1;
        }
        private void onPartUnpack(Part part)
        {
            if (Tools._debug == true)
            {
                Tools.LogFormatted("onPartUnpack");
            }
            attachcounter = 1;
        }

        public void OnDestroy()
        {
            GameEvents.onPartAttach.Remove(onPartAttach);
            GameEvents.onPartCouple.Remove(onPartCouple);
            GameEvents.onPartUndock.Remove(onPartUndock);
            GameEvents.onPartUnpack.Remove(onPartUnpack);
            GameEvents.onVesselWasModified.Remove(onVesselWasModified);
            if (Tools._debug == true)
            {
                Tools.LogFormatted("part destroyed");
            }
        }
    }
}

