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
        public bool armAnimationController = false;
        [KSPField]
        public string armAnimation = "";
        [KSPEvent(guiName = "Disarm", active = false, guiActive = true, guiActiveEditor = true)]
        public void Disarm()
        {
            ToggleArm(false);
        }
        [KSPEvent(guiName = "Arm", active = false, guiActive = true, guiActiveEditor = true)]
        public void Arm()
        {
            ToggleArm(true);
        }

        private ModuleAnimateGeneric module;
        private ModuleAnimateGeneric armmodule;
        private PartModule dockingmodule;

        public override void OnStart(StartState state)
        {
            base.OnStart(state);
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
            foreach (ModuleAnimateGeneric n in part.FindModulesImplementing<ModuleAnimateGeneric>())
            {
                if (n.animationName == armAnimation)
                {
                    armmodule = n;
                }
            }
            if (module != null)
            {
                GameEvents.onPartCouple.Add(onPartCouple);
                GameEvents.onPartUndock.Add(onPartUndock);
            }
            if (!armAnimationController)
            {
                Events["Disarm"].active = false;
                Events["Arm"].active = false;
            }
            if (armAnimationController) ToggleArm(false);


        }

        private void ToggleArm(bool state)
        {
            Events["Disarm"].active = state;
            Events["Arm"].active = !state;
            if (!state)
            {
                var animationstate = armmodule.GetState();
                var animationstate1 = module.GetState();
                if (animationstate.normalizedTime == 0f) armmodule.Toggle();
                if (animationstate1.normalizedTime == 0f) module.Toggle();
                dockingmodule.isEnabled = false;
            }
            if (state)
            {
                var animationstate = armmodule.GetState();
                var animationstate1 = module.GetState();
                if (animationstate.normalizedTime == 1f) armmodule.Toggle();
                if (animationstate1.normalizedTime == 1f) module.Toggle();
                dockingmodule.isEnabled = true;
            }
            MonoUtilities.RefreshContextWindows(part);
        }

        private void onPartCouple(GameEvents.FromToAction<Part, Part> action)
        {
            Events["Disarm"].active = false;
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
            Events["Arm"].active = true;
            var animationstate = module.GetState();
            if (animationstate.normalizedTime == 0f)
            {
                return;
            }
            if (part == base.part)
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
