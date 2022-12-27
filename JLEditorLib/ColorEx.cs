using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace JLEditorLib
{
    public static class ColorEx
    {
        public static byte Average(this Color C)
        {
            return (byte)((C.R + C.G + C.B) / 3);
        }
    }
}
