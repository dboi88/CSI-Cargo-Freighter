using System;

using KSP;

namespace CSITools
{
    public class CSI_ModuleAnimatedDocking : PartModule
    {
        [KSPField]
        public string animationName = "";
        [KSPField]
        public bool dockAnimationController = false;
        [KSPField]
        public string referenceAttachNode = "";
        [KSPField]
        public string armAnimation = "";
 
        

        private ModuleAnimateGeneric module;
        private PartModule dockingmodule;

        public override void OnStart(StartState state)
        {
            Tools.LogFormatted("OnStart");
            foreach (ModuleDockingNode d in part.FindModulesImplementing<ModuleDockingNode>())
            {
                if (d.referenceAttachNode == referenceAttachNode)
                {
                    dockingmodule = d;
                    break;
                }
            }
            foreach (ModuleAnimateGeneric m in part.FindModulesImplementing<ModuleAnimateGeneric>())
            {
                if (m.animationName == animationName)
                {
                    module = m;
                    break;
                }
            }
            if (module != null)
            {
                GameEvents.onPartCouple.Add(onPartCouple);
                GameEvents.onPartUndock.Add(onPartUndock);
            }
        }

        private void onUpdate()
        {
        }

        

        private void onPartCouple(GameEvents.FromToAction<Part, Part> action)
        {
            var animationstate = module.GetState();
            if (animationstate.normalizedTime == 1f)
            {
                return;
            }
            if (action.to == part || action.from == part)
            {
                module.Toggle();
            }
        }

        private void onPartUndock(Part part)
        {
            var animationstate = module.GetState();
            if (animationstate.normalizedTime == 0f)
            {
                return;
            }
            if (this.part == base.part)
            {
                module.Toggle();
            }

        }

        private void OnDestroy()
        {
            GameEvents.onPartCouple.Remove(onPartCouple);
            GameEvents.onPartUndock.Remove(onPartUndock);
        }
    }
}
