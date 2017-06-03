//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace CSITools
//{
//    class GUIButtonTemplate : PartModule
//    {
//        [KSPField(isPersistant = true)]
//        public bool transferEnabled = false;
//        private float emptymass = 0f;
//        private float basemass = 0f;

//        [KSPEvent(guiName = "Disable Weight Transfer", active = false, guiActive = true, guiActiveEditor = true)]
//        public void DisableTransfer()
//        {
//            ToggleTransfer(false);
//            Tools.LogFormatted("Toggle Transfer Disabled");
//        }

//        [KSPEvent(guiName = "Enable Weight Transfer", active = true, guiActive = true, guiActiveEditor = true)]
//        public void EnableTransfer()
//        {
//            ToggleTransfer(true);
//            Tools.LogFormatted("Toggle Transfer Enabled");
//        }

//        private void ToggleTransfer(bool state)
//        {
//            transferEnabled = state;
//            Events["DisableTransfer"].active = state;
//            Events["EnableTransfer"].active = !state;
//            WeightTransfer();
//            Tools.LogFormatted("Toggle Transfer ran weight transfer");
//            MonoUtilities.RefreshContextWindows(part);
//        }



//        public override void OnStart(StartState state)
//        {
//            Tools.LogFormatted("class1 started");
//            basemass = part.mass;
//            Tools.LogFormatted("base mass = " + basemass);
//        }

//        public void Update()
//        {

//        }

//        private void _comtransfer()
//        {

//        }


//        private void WeightTransfer()
//        {
//            if (transferEnabled)
//            {
//                part.mass = emptymass;
//                Tools.LogFormatted("part.mass set emptymass");
//                Tools.LogFormatted("part.mass = " + part.mass);
//                Tools.LogFormatted("emptymass = " + emptymass);
//                return;
//            }
//            else
//                part.mass = basemass;
//                GetModuleMass
//                Tools.LogFormatted("part.mass set basemass");
//                Tools.LogFormatted("part.mass = " + part.mass);
//                Tools.LogFormatted("base mass = " + basemass);
//        }
//    }
//}
