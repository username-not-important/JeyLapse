using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JLEditorLib.Effects.General
{
    public class ContrastFX : FX 
    {
        /// <summary>
        /// Amount of contrast to apply on Image.
        /// Between -50,50
        /// </summary>
        public int Value { get; set; }

        public override byte Apply(byte input)
        {
            if (Value == 0)
                return input;

            int d = input - 128;
            double r = Value / 100.0;

            int O = input + (int)(d * r * r);

            if (O > 255)
                return 255;
            else if (O < 0)
                return 0;
            else
                return (byte)O;
        }

    }
}
