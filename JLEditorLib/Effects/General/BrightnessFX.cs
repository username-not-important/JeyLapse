using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JLEditorLib.Effects.General
{
    public class BrightnessFX : FX
    {
        /// <summary>
        /// The Amount of effect.
        /// Value between -40,40
        /// </summary>
        public int Value { get; set; }

        public override byte Apply(byte input)
        {
            int V = input + Value;

            if (V > 255)
                return 255;
            else if (V < 0)
                return 0;
            else
                return (byte)V;
        }

    }
}
