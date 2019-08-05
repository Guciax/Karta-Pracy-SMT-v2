using RawInput_dll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karta_Pracy_SMT_v2
{
    public class QrReader
    {
        public QrReader()
        {

        }
        public static RawInput rawinput;
        public static string lastInputDeviceName = "";

        // Otherwise default behavior is to capture always

        //_rawinput = new RawInput(Handle);
        //_rawinput.KeyPressed += _rawinput_KeyPressed;
        //_rawinput.AddMessageFilter();                   // Adding a message filter will cause keypresses to be handled
        //_rawinput.CaptureOnlyIfTopMostWindow = true;


        public static void rawinput_KeyPressed(object sender, InputEventArg e)
        {
            lastInputDeviceName = e.KeyPressEvent.DeviceName;
        }

    }
}
