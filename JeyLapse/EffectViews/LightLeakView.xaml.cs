using System.Windows.Controls;
using JeyLapse.EffectViews;
using JLEditorLib.Effects;
using JLEditorLib.Effects.ColorFilters;

namespace JeyLapse
{
    public partial class LightLeakView : UserControl, IDoEffect
    {
        public LightLeakView()
        {
            InitializeComponent();
        }

        public FX GetEffect()
        {
            if (!checkbox.IsChecked.Value)
            {
                LightLeak.FreeMemory();
                return null;
            }

            return new LightLeak {Fade = slider.Value};
        }

        public void SetEffect(FX value)
        {
            FXCollection col = value as FXCollection;

            for (int i = 0; i < col.Count; i++)
            {
                var fx = col[i] as LightLeak;
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