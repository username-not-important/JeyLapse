using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace JLEditorLib.Effects.ColorFilters
{
    public class LightLeak : FX
    {
        private static WriteableBitmap _leakmask;
        public static WriteableBitmap LeakMask
        {
            get
            {
                return _leakmask ?? (_leakmask = new WriteableBitmap(1, 1).FromResource("/Effects/Assets/LeakOverlay.png"));
            }
        }

        public static void FreeMemory()
        {
            _leakmask = null;
            GC.Collect();
        }

        /// <summary>
        /// The Opacity of the effect.
        /// Value between 0.0 and 1.0
        /// </summary>
        public double Fade { get; set; }

        public LightLeak()
        {
            Fade = 1;
        }

        public override WriteableBitmap Apply(WriteableBitmap input)
        {
            byte val = (byte)(int)(Fade * 150);

            input.Blit(new Point(), input, new Rect(0, 0, input.PixelWidth, input.PixelHeight), Color.FromArgb(255, val,val,val), WriteableBitmapExtensions.BlendMode.Additive);
            input.Blit(new Rect(0, 0, input.PixelWidth, input.PixelHeight), LeakMask, new Rect(0, 0, LeakMask.PixelWidth, LeakMask.PixelHeight), WriteableBitmapExtensions.BlendMode.Additive);
            
            return input;
        }
    }
}
