using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace JLEditorLib.Effects.Size
{
    public class WideScreenFX : FX
    {
        public WideScreenFX()
        {
            
        }

        public override WriteableBitmap Apply(WriteableBitmap input)
        {
            double width = input.PixelWidth;
            double height = input.PixelHeight;

            double aspect = width/height;
            double target = 1.77777777/(aspect);

            var result = input.Crop(0, (int) ((height - (height/target))/2.0), (int) width, (int)(height/target));
            
            return result;
        }
    }
}
