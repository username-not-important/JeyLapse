using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JLEditorLib.Effects
{
    public interface IByteEffect
    {
        byte Apply(byte input);
    }
}
