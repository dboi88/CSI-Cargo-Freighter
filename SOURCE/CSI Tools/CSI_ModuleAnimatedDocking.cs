using System;

using KSP;

namespace CSITools
{
    public class CSI_ModuleAnimatedDocking : PartModule
    {
        [KSPField]
        public string animationName = "";

        private ModuleAnimateGeneric module;

        public override void OnStart(StartState state)
        {
            base.OnStart(state);
            foreach (ModuleAnimateGeneric m in base.part.FindModulesImplementing<ModuleAnimateGeneric>())
            {
                if (m.animationName == this.animationName)
                {
                    this.module = m;
                    break;
                }
            }
            if (this.module == null) return;
            GameEvents.onPartCouple.Add(this.onPartCouple);
            GameEvents.onPartUndock.Add(this.onPartUndock);
        }
        private void onPartCouple(GameEvents.FromToAction<Part, Part> action)
        {
            var animationstate = module.GetState();
            if (animationstate.normalizedTime == 1f)
            {
                return;
            }
            if (action.to == base.part || action.from == part)
            {
                this.module.Toggle();
            }
        }

        /// AnimationState ModuleAnimateGeneric.GetState	(		)	
        private void onPartUndock(Part part)
        {
            var animationstate = module.GetState();
            if (animationstate.normalizedTime == 0f)
            {
                return;
            }
            if (part == base.part)
            {
                this.module.Toggle();
            }

        }
        private void OnDestroy()
        {
            GameEvents.onPartCouple.Remove(this.onPartCouple);
            GameEvents.onPartUndock.Remove(this.onPartUndock);
        }
    }
}
