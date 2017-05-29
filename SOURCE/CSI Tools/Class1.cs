using System;
using System.Collections.Generic;
using KSP;

namespace CSITools
{
    public class Class1 : PartModule
    {
        [KSPField(isPersistant = true)]
        public bool transferEnabled = false;

        [KSPEvent(guiName = "Disable Weight Transfer", active = false, guiActive = true, guiActiveEditor = true)]
        public void DisableTransfer()
        {
            ToggleTransfer(false);
            Tools.LogFormatted("Toggle Transfer Disabled");
        }

        [KSPEvent(guiName = "Enable Weight Transfer", active = true, guiActive = true, guiActiveEditor = true)]
        public void EnableTransfer()
        {
            ToggleTransfer(true);
            Tools.LogFormatted("Toggle Transfer Enabled");
        }

        private void ToggleTransfer(bool state)
        {
            transferEnabled = state;
            Events["DisableTransfer"].active = state;
            Events["EnableTransfer"].active = !state;
            WeightTransfer();
            Tools.LogFormatted("Toggle Transfer ran weight transfer");
            MonoUtilities.RefreshContextWindows(part);
        }

        private List<Part> _cargoParts;
        private int _childCount;

        public override void OnStart(StartState state)
        {
            _cargoParts = GetCargoParts();
            _childCount = part.children.Count;
            ToggleTransfer(transferEnabled);
            Tools.LogFormatted("class1 started");
            part.physicsMass = 0;
        }

        public void Update()
        {
            var c = part.children.Count;
            if (c == _childCount)
                return;

            _childCount = c;
            _cargoParts = GetCargoParts();
            WeightTransfer();
            Tools.LogFormatted("onupdate rand weight transfer");
        }

        private List<Part> GetCargoParts()
        {
            var c = part.children.Count;
            var parts = new List<Part>();
            for (int i = 0; i < c; ++i)
            {
                var p = part.children[i];
                if (p.children.Count == 0
                    && p.FindModuleImplementing<Class2>() != null)
                    parts.Add(p);
            }
            Tools.LogFormatted("GotCargoParts");
            return parts;
        }

        private void WeightTransfer()
        {
            var c = _cargoParts.Count;
            Tools.LogFormatted("weight transfer ran");
            for (int i = 0; i < c; ++i)
            {
                var disablePhysics = transferEnabled;
                var p = _cargoParts[i];
                Tools.LogFormatted("weight transfer ran");
                if (p.children.Count > 0)
                    disablePhysics = false;
                if (!p.Modules.Contains("Class2"))
                    disablePhysics = false;

                if (disablePhysics)
                {
                    p.mass = 0;
                    part.mass = 0;
                    Tools.LogFormatted("part mass set 0");
                }
                if (!disablePhysics)
                {
                    p.mass = 10;
                    Tools.LogFormatted("part mass set 10");
                }
            }
        }
    }
}
