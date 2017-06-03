using System;

using KSP;

namespace CSITools
{
    public class Class1 : PartModule
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
        [KSPField(isPersistant = true)]
        public bool armed = false;
        [KSPEvent(guiName = "Disarm", active = false, guiActive = true, guiActiveEditor = true)]
        public void Disarm()
        {
            ToggleArm(false);
        }
        [KSPEvent(guiName = "Arm", active = true, guiActive = true, guiActiveEditor = true)]
        public void Arm()
        {
            ToggleArm(true);
        }

        private ModuleAnimateGeneric module;
        private ModuleAnimateGeneric armmodule;
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
                ToggleArm(armed);
            }
            //if (!armAnimationController)
            //{
            //    Events["Disarm"].active = false;
            //    Events["Arm"].active = true;
            //}
            //module.Toggle();
            ////armmodule.Toggle();
            //if (armAnimationController) ToggleArm(false);


        }

        private void onUpdate()
        {
        }

        private void ToggleArm(bool state)
        {
            armed = state;
            Events["Disarm"].active = state;
            Events["Arm"].active = !state;
            if (!state)
            {
                var animationstate1 = armmodule.GetState();
                if (animationstate1.normalizedTime == 1f) armmodule.Toggle();
                dockingmodule.isEnabled = false;
            }
            if (state)
            {
                var animationstate1 = armmodule.GetState();
                if (animationstate1.normalizedTime == 0f) armmodule.Toggle();
                dockingmodule.isEnabled = true;
            }
            MonoUtilities.RefreshContextWindows(part);
        }

        private void onPartCouple(GameEvents.FromToAction<Part, Part> action)
        {
            Events["Disarm"].active = false;
            module.Toggle();
        }

        private void onPartUndock(Part part)
        {
            Events["Disarm"].active = true;
            module.Toggle();
        }

        private void OnDestroy()
        {
            GameEvents.onPartCouple.Remove(onPartCouple);
            GameEvents.onPartUndock.Remove(onPartUndock);
        }
    }
}

