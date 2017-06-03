using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSITools
{
    public class Tools : MonoUtilities
    {
        public static bool _debug = false;

        /// <summary>
        /// Name of the Assembly that is running this 
        /// </summary>
        internal static String _AssemblyName
        { get { return System.Reflection.Assembly.GetExecutingAssembly().GetName().Name; } }

        /// <summary>
        /// Some Structured logging to the debug file
        /// </summary>
        /// <param name="Message">Text to be printed - can be formatted as per String.format</param>
        /// <param name="strParams">Objects to feed into a String.format</param>
        internal static void LogFormatted(String Message, params object[] strParams)
        {
            Message = String.Format(Message, strParams);                  // This fills the params into the message
            String strMessageLine = String.Format("{0},{2},{1}",
                DateTime.Now, Message,
                _AssemblyName);                                           // This adds our standardised wrapper to each line
            UnityEngine.Debug.Log(strMessageLine);                        // And this puts it in the log
        }
    }
}
