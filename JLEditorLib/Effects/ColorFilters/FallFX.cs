using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace JLEditorLib.Effects.ColorFilters
{
    public class FallFX : FX
    {
        /// <summary>
        /// The Opacity of the effect.
        /// Value between 0.0 and 1.0
        /// </summary>
        public double Fade { get; set; }

        public FallFX()
        {
            Fade = 1;
        }

        public override Color Apply(Color input)
        {
            var average = input.Average();

            int r = (int)((Fade - 0.5) * average / 4);

            input.R = Clamp(input.R + r * 3);
            input.G = Clamp(input.G + r * 2);
            input.B = Clamp(input.B + r / 3);

            return input;
        }
    }
}
