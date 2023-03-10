#pragma checksum "C:\Users\MyDear\Documents\Visual Studio 2013\Projects\JeyLapse\JeyLapse\Pages\EffectsPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "E99DF09A19ECB2785728502287909BE3"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using JeyLapse;
using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace JeyLapse {
    
    
    public partial class EffectsPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.VisualStateGroup PreviewStateGroup;
        
        internal System.Windows.VisualState Capturing;
        
        internal System.Windows.VisualState Previewing;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.Controls.StackPanel _IDoEffects;
        
        internal JeyLapse.BrightnessView _Brightness;
        
        internal JeyLapse.ContrastView _Contrast;
        
        internal JeyLapse.SaturateView _Saturate;
        
        internal JeyLapse.CobaltView _Cobalt;
        
        internal JeyLapse.FallView _Fall;
        
        internal JeyLapse.SunriseView _Sunrise;
        
        internal JeyLapse.LightLeakView _LightLeak;
        
        internal System.Windows.Controls.Border border;
        
        internal System.Windows.Controls.Canvas CameraCanvas;
        
        internal System.Windows.Media.VideoBrush CameraBrush;
        
        internal System.Windows.Controls.TextBlock _WaitMessage;
        
        internal System.Windows.Controls.Button _PreviewButton;
        
        internal System.Windows.Controls.Button _ApplyButton;
        
        internal System.Windows.Controls.Grid PreviewGrid;
        
        internal System.Windows.Controls.Image PreviewImage;
        
        internal System.Windows.Controls.Image _IconClose;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/JeyLapse;component/Pages/EffectsPage.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.PreviewStateGroup = ((System.Windows.VisualStateGroup)(this.FindName("PreviewStateGroup")));
            this.Capturing = ((System.Windows.VisualState)(this.FindName("Capturing")));
            this.Previewing = ((System.Windows.VisualState)(this.FindName("Previewing")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this._IDoEffects = ((System.Windows.Controls.StackPanel)(this.FindName("_IDoEffects")));
            this._Brightness = ((JeyLapse.BrightnessView)(this.FindName("_Brightness")));
            this._Contrast = ((JeyLapse.ContrastView)(this.FindName("_Contrast")));
            this._Saturate = ((JeyLapse.SaturateView)(this.FindName("_Saturate")));
            this._Cobalt = ((JeyLapse.CobaltView)(this.FindName("_Cobalt")));
            this._Fall = ((JeyLapse.FallView)(this.FindName("_Fall")));
            this._Sunrise = ((JeyLapse.SunriseView)(this.FindName("_Sunrise")));
            this._LightLeak = ((JeyLapse.LightLeakView)(this.FindName("_LightLeak")));
            this.border = ((System.Windows.Controls.Border)(this.FindName("border")));
            this.CameraCanvas = ((System.Windows.Controls.Canvas)(this.FindName("CameraCanvas")));
            this.CameraBrush = ((System.Windows.Media.VideoBrush)(this.FindName("CameraBrush")));
            this._WaitMessage = ((System.Windows.Controls.TextBlock)(this.FindName("_WaitMessage")));
            this._PreviewButton = ((System.Windows.Controls.Button)(this.FindName("_PreviewButton")));
            this._ApplyButton = ((System.Windows.Controls.Button)(this.FindName("_ApplyButton")));
            this.PreviewGrid = ((System.Windows.Controls.Grid)(this.FindName("PreviewGrid")));
            this.PreviewImage = ((System.Windows.Controls.Image)(this.FindName("PreviewImage")));
            this._IconClose = ((System.Windows.Controls.Image)(this.FindName("_IconClose")));
        }
    }
}

