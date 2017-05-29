using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSITools
{
    class Class2 : PartModule
    {
        public void OnStart()
        {
            Tools.LogFormatted("Class2 Started");
        }

        public void setmass(int mass)
        {
            part.mass = mass;
        }
    }
}
