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
            if (action.to == base.part)
            {
                module.Toggle();
            }
        }
        private void onPartUndock(Part part)
        {
            if (part = base.part)
            {
                this.module.Toggle();
            }
        }
        private void OnDestroy()
        {
            GameEvents.onPartCouple.Add(this.onPartCouple);
            GameEvents.onPartUndock.Remove(this.onPartUndock);
        }
    }
}
