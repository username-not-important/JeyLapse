﻿#pragma checksum "C:\Users\MyDear\Documents\Visual Studio 2013\Projects\JeyLapse\JeyLapse\EffectViews\SaturateView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "99DBF8A7D25EFFECE23FA6CE9F49E104"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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
    
    
    public partial class SaturateView : System.Windows.Controls.UserControl {
        
        internal System.Windows.Controls.StackPanel LayoutRoot;
        
        internal System.Windows.Controls.CheckBox checkbox;
        
        internal System.Windows.Controls.Slider slider;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/JeyLapse;component/EffectViews/SaturateView.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.StackPanel)(this.FindName("LayoutRoot")));
            this.checkbox = ((System.Windows.Controls.CheckBox)(this.FindName("checkbox")));
            this.slider = ((System.Windows.Controls.Slider)(this.FindName("slider")));
        }
    }
}
