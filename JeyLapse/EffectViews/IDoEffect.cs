using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JLEditorLib.Effects;

namespace JeyLapse.EffectViews
{
    interface IDoEffect
    {
        FX GetEffect();
        void SetEffect(FX value);
    }
}
