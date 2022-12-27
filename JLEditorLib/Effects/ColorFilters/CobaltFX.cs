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
    public class CobaltFX : FX
    {
        private static WriteableBitmap _cobaltmask;
        public static WriteableBitmap CobaltMask
        {
            get
            {
                return _cobaltmask ?? (_cobaltmask = new WriteableBitmap(1, 1).FromResource("/Effects/Assets/CobaltMultiply.jpg"));
            }
        }

        private static WriteableBitmap _cobaltmask2;
        public static WriteableBitmap CobaltMask2
        {
            get
            {
                return _cobaltmask2 ?? (_cobaltmask2 = new WriteableBitmap(1, 1).FromResource("/Effects/Assets/CobaltAdd.jpg"));
            }
        }

        public static void FreeMemory()
        {
            _cobaltmask = null;
            _cobaltmask2 = null;

            GC.Collect();
        }
        /// <summary>
        /// The Opacity of the effect.
        /// Value between 0.0 and 1.0
        /// </summary>
        public double Fade { get; set; }

        public CobaltFX()
        {
            Fade = 1;
        }

        public override WriteableBitmap Apply(WriteableBitmap input)
        {
            input.Blit(new Rect(0, 0, input.PixelWidth, input.PixelHeight), CobaltMask, new Rect(0, 0, CobaltMask.PixelWidth, CobaltMask.PixelHeight), WriteableBitmapExtensions.BlendMode.Multiply);
            input.Blit(new Rect(0, 0, input.PixelWidth, input.PixelHeight), CobaltMask2, new Rect(0, 0, CobaltMask.PixelWidth, CobaltMask.PixelHeight), WriteableBitmapExtensions.BlendMode.Additive);

            byte val = (byte) (int) (Fade*150);
            input.Blit(new Point(), input, new Rect(0,0,input.PixelWidth,input.PixelHeight), Color.FromArgb(255,val,val,val), WriteableBitmapExtensions.BlendMode.Additive);

            return input;
        }
    }
}
