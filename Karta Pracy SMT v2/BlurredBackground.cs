
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karta_Pracy_SMT_v2
{
    public class BlurredBackground
    {
        public static Bitmap currentScreenShot;
        public static Bitmap ssGrayColor
        {
            get
            {
                return ApplyBlur(currentScreenShot);
            }
        }
        public static System.Drawing.Bitmap ApplyBlur(System.Drawing.Bitmap img)
        {
            //Image<Gray, float> inputImage = new Image<Gray, float>(img);
           // Image<Gray, float> smoothedImage = inputImage.SmoothBlur(10, 10);

            //var result = inputImage.ToBitmap();

            return img;
        }


    }
}
