using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using JeyLapse.EffectViews;
using JLEditorLib.Effects;
using JLEditorLib.Effects.ColorFilters;
using JLEditorLib.Effects.General;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace JeyLapse
{
    public partial class CobaltView : UserControl, IDoEffect
    {
        public CobaltView()
        {
            InitializeComponent();
        }


        public FX GetEffect()
        {
            if (!checkbox.IsChecked.Value)
            {
                CobaltFX.FreeMemory();
                return null;
            }

            return new CobaltFX() { Fade = slider.Value };
        }

        public void SetEffect(FX value)
        {
            FXCollection col = value as FXCollection;

            for (int i = 0; i < col.Count; i++)
            {
                var fx = col[i] as CobaltFX;
                if (fx != null)
                {
                    checkbox.IsChecked = true;
                    slider.Value = fx.Fade;

                    return;
                }
            }
        }
    }
}
