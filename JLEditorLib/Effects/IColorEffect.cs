using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace JLEditorLib.Effects
{
    public interface IColorEffect
    {
        Color Apply(Color input);
    }

}
