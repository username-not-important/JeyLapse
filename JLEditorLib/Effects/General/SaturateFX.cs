using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace JLEditorLib.Effects.General
{
    public class SaturateFX : FX
    {
        /// <summary>
        /// The Amount of the effect.
        /// Value between -50 and 50
        /// </summary>
        public double Value { get; set; }

        public SaturateFX()
        {

        }

        public override Color Apply(Color input)
        {
            var average = input.Average();

            double r = Value / 100.0;


            int _r = input.R - average;
            int _g = input.G - average;
            int _b = input.B - average;

            int O_r = input.R + (int)(_r * r * 2);
            int O_g = input.G + (int)(_g * r * 2);
            int O_b = input.B + (int)(_b * r * 2);

            input.R = Clamp(O_r);
            input.G = Clamp(O_g);
            input.B = Clamp(O_b);

            return input;
        }
    }
}
