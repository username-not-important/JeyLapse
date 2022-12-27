using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace JLEditorLib.Effects.ColorFilters
{
    public class SunriseFX : FX
    {
        /// <summary>
        /// The Opacity of the effect.
        /// Value between 0.0 and 1.0
        /// </summary>
        public double Fade { get; set; }

        /// <summary>
        /// The Amount of Shadow Recovery
        /// Value Between -1.0 and 1.0
        /// </summary>
        public double ShadowAdjust { get; set; }

        public SunriseFX()
        {
            Fade = 1;
        }

        public override Color Apply(Color input)
        {
            var average = input.Average();

            double r = Fade / 4.0;

            double s = ShadowAdjust * (average / 5.0);

            int _r = input.R - average;
            int _g = input.G - average;
            int _b = input.B - average;

            int O_r = input.R + (int)(_r * r * 4 + s);
            int O_g = input.G + (int)(_g * r * 2 + s);
            int O_b = input.B + (int)(_b * r * 4 + s);

            input.R = Clamp(O_r);
            input.G = Clamp(O_g);
            input.B = Clamp(O_b);

            return input;
        }
    }
}
