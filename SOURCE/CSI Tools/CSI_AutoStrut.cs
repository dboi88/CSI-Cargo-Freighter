using System;
using KSP;

/// <summary>
/// test
/// </summary>
namespace CSITools
{
    public class CSI_AutoStrut : PartModule
    {
        [KSPField]
        public string type = "disabled";

        public override void OnStart(StartState state)
        {
            if (type == "ForceGrandparent")
            {
                part.autoStrutMode = Part.AutoStrutMode.ForceGrandparent;
                part.UpdateAutoStrut();
                return;
            }
            if (type == "ForceHeaviest")
            {
                part.autoStrutMode = Part.AutoStrutMode.ForceHeaviest;
                part.UpdateAutoStrut();
                return;
            }
            if (type == "Grandparent")
            {
                part.autoStrutMode = Part.AutoStrutMode.Grandparent;
                part.UpdateAutoStrut();
                return;
            }
            if (type == "Heaviest")
            {
                part.autoStrutMode = Part.AutoStrutMode.Heaviest;
                part.UpdateAutoStrut();
                return;
            }
            if (type == "Off")
            {
                part.autoStrutMode = Part.AutoStrutMode.Off;
                part.UpdateAutoStrut();
                return;
            }
            if (type == "Root")
            {
                part.autoStrutMode = Part.AutoStrutMode.Root;
                part.UpdateAutoStrut();
                return;
            }
            if (type == "ForceRoot")
            {
                part.autoStrutMode = Part.AutoStrutMode.ForceRoot;
                part.UpdateAutoStrut();
                return;
            }
            else
                Tools.LogFormatted("Input String Error, Check CFG settings");
        }
    }
}
