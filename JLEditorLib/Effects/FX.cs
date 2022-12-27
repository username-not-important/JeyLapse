using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace JLEditorLib.Effects
{
    public abstract class FX : IByteEffect, IColorEffect
    {
        public bool Preview { get; set; }

        public virtual byte Apply(byte input)
        {
            throw new NotImplementedException();
        }

        public virtual Color Apply(Color input)
        {
            if (input.R == input.G && input.G == input.B)
            {
                byte v = Apply(input.R);
                return Color.FromArgb(255, v, v, v);
            }

            return Color.FromArgb(255, Apply(input.R), Apply(input.G), Apply(input.B));
        }

        public virtual WriteableBitmap Apply(WriteableBitmap input)
        {
            input.ForEach((x, y, C) =>
            {
                if ((Preview && x <= input.PixelWidth/2.0)) return C;
                else return Apply(C);
            });

            return input;
        }

        protected byte Clamp(int i)
        {
            if (i > 255)
                return 255;
            else if (i < 0)
                return 0;
            else
                return (byte)i;
        }
    }
}
