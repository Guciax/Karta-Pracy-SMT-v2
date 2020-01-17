using RawInput_dll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karta_Pracy_SMT_v2
{
    public class KeyboardDeviceListener
    {
        public KeyboardDeviceListener()
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

        public static string ReverseHex(string inputHex)
        {
            var splitted = inputHex.SplitInParts(2);
            return string.Join("", splitted.Reverse());
        }

        
    }

    static class StringExtensions
    {
        public static IEnumerable<String> SplitInParts(this String s, Int32 partLength)
        {
            if (s == null)
                throw new ArgumentNullException("s");
            if (partLength <= 0)
                throw new ArgumentException("Part length has to be positive.", "partLength");

            for (var i = 0; i < s.Length; i += partLength)
                yield return s.Substring(i, Math.Min(partLength, s.Length - i));
        }
    }
}
