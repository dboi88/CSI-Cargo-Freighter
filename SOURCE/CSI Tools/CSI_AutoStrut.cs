using System;
using KSP;


namespace CSITools
{
    public class CSI_AutoStrut : PartModule
    {
        public override void OnStart(StartState state)
        {
            part.autoStrutMode = Part.AutoStrutMode.ForceRoot;
            part.UpdateAutoStrut();
        }
    }
}
