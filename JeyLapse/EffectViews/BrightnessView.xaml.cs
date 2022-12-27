using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using JeyLapse.EffectViews;
using JLEditorLib.Effects;
using JLEditorLib.Effects.General;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace JeyLapse
{
    public partial class BrightnessView : UserControl, IDoEffect
    {
        public BrightnessView()
        {
            InitializeComponent();
        }

        public FX GetEffect()
        {
            if (!checkbox.IsChecked.Value)
                return null;

            return new BrightnessFX() {Value = (int)slider.Value};
        }

        public void SetEffect(FX value)
        {
            FXCollection col = value as FXCollection;

            for (int i = 0; i < col.Count; i++)
            {
                var fx = col[i] as BrightnessFX;
                if (fx != null)
                {
                    checkbox.IsChecked = true;
                    slider.Value = fx.Value;

                    return;
                }
            }
        }
    }
}
