using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karta_Pracy_SMT_v2
{
    public class BlurredBackground
    {
        private static System.Drawing.Bitmap ApplyBlur(System.Drawing.Bitmap img)
        {
            Image<Gray, float> inputImage;
            Image<Gray, float> smoothedImage = new Image<Gray, float>(img.Width, img.Height);
            int apertureWidth = 10;
            int apertureHeight = 10;

            //cvSmooth(inputImage.Ptr, smoothedImage.Ptr, SMOOTH_TYPE.CV_GAUSSIAN, apertureWidth, apertureHeight, 0, 0);

            return new System.Drawing.Bitmap(1, 1);
        }
    }
}
