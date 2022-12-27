﻿#pragma checksum "C:\Users\MyDear\Documents\Visual Studio 2013\Projects\JeyLapse\JeyLapse\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "5E972F3E94C1D3AAAF5A5F10449FEFC9"
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
    
    
    public partial class MainPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal Microsoft.Phone.Controls.PhoneApplicationPage Page;
        
        internal System.Windows.Media.Animation.Storyboard ProcessingStoryboard;
        
        internal System.Windows.Media.Animation.Storyboard ProcessingFinishedStoryboard;
        
        internal System.Windows.Media.Animation.Storyboard EyeBlinkStoryboard;
        
        internal System.Windows.Media.Animation.Storyboard PageStartupStoryboard;
        
        internal System.Windows.Media.Animation.Storyboard SavedStoryboard;
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.VisualStateGroup CaptureStates;
        
        internal System.Windows.VisualState Capturing;
        
        internal System.Windows.VisualState Stopped;
        
        internal System.Windows.Controls.Canvas CameraCanvas;
        
        internal System.Windows.Media.VideoBrush CameraBrush;
        
        internal System.Windows.Controls.Grid _BlackGrid;
        
        internal System.Windows.Controls.Grid RightPanel;
        
        internal System.Windows.Controls.Grid TitleBar;
        
        internal System.Windows.Controls.Viewbox viewbox;
        
        internal System.Windows.Controls.Grid SettingsBar;
        
        internal JeyLapse.SettingView _SettingInterval;
        
        internal JeyLapse.SettingView _SettingFlash;
        
        internal JeyLapse.SettingView _SettingDuration;
        
        internal System.Windows.Controls.Grid ResBar;
        
        internal System.Windows.Controls.TextBlock _TextRes;
        
        internal JeyLapse.SettingView _SettingEffects;
        
        internal System.Windows.Controls.Grid TimeBar;
        
        internal System.Windows.Controls.TextBlock _TextTimer;
        
        internal System.Windows.Controls.Grid FrameBar;
        
        internal System.Windows.Controls.TextBlock _TextFrame;
        
        internal System.Windows.Controls.Grid BatteryBar;
        
        internal System.Windows.Controls.TextBlock _TextBattery;
        
        internal System.Windows.Controls.Button _ButtonStop;
        
        internal System.Windows.Controls.Button _ButtonStart;
        
        internal System.Windows.Controls.Grid _IconProcess;
        
        internal System.Windows.Controls.Grid _IconEffects;
        
        internal System.Windows.Controls.Grid _IconSaved;
        
        internal System.Windows.Controls.Grid _introGrid;
        
        internal System.Windows.Controls.TextBlock _jeyART;
        
        internal System.Windows.Controls.TextBlock _jeylapse;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/JeyLapse;component/MainPage.xaml", System.UriKind.Relative));
            this.Page = ((Microsoft.Phone.Controls.PhoneApplicationPage)(this.FindName("Page")));
            this.ProcessingStoryboard = ((System.Windows.Media.Animation.Storyboard)(this.FindName("ProcessingStoryboard")));
            this.ProcessingFinishedStoryboard = ((System.Windows.Media.Animation.Storyboard)(this.FindName("ProcessingFinishedStoryboard")));
            this.EyeBlinkStoryboard = ((System.Windows.Media.Animation.Storyboard)(this.FindName("EyeBlinkStoryboard")));
            this.PageStartupStoryboard = ((System.Windows.Media.Animation.Storyboard)(this.FindName("PageStartupStoryboard")));
            this.SavedStoryboard = ((System.Windows.Media.Animation.Storyboard)(this.FindName("SavedStoryboard")));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.CaptureStates = ((System.Windows.VisualStateGroup)(this.FindName("CaptureStates")));
            this.Capturing = ((System.Windows.VisualState)(this.FindName("Capturing")));
            this.Stopped = ((System.Windows.VisualState)(this.FindName("Stopped")));
            this.CameraCanvas = ((System.Windows.Controls.Canvas)(this.FindName("CameraCanvas")));
            this.CameraBrush = ((System.Windows.Media.VideoBrush)(this.FindName("CameraBrush")));
            this._BlackGrid = ((System.Windows.Controls.Grid)(this.FindName("_BlackGrid")));
            this.RightPanel = ((System.Windows.Controls.Grid)(this.FindName("RightPanel")));
            this.TitleBar = ((System.Windows.Controls.Grid)(this.FindName("TitleBar")));
            this.viewbox = ((System.Windows.Controls.Viewbox)(this.FindName("viewbox")));
            this.SettingsBar = ((System.Windows.Controls.Grid)(this.FindName("SettingsBar")));
            this._SettingInterval = ((JeyLapse.SettingView)(this.FindName("_SettingInterval")));
            this._SettingFlash = ((JeyLapse.SettingView)(this.FindName("_SettingFlash")));
            this._SettingDuration = ((JeyLapse.SettingView)(this.FindName("_SettingDuration")));
            this.ResBar = ((System.Windows.Controls.Grid)(this.FindName("ResBar")));
            this._TextRes = ((System.Windows.Controls.TextBlock)(this.FindName("_TextRes")));
            this._SettingEffects = ((JeyLapse.SettingView)(this.FindName("_SettingEffects")));
            this.TimeBar = ((System.Windows.Controls.Grid)(this.FindName("TimeBar")));
            this._TextTimer = ((System.Windows.Controls.TextBlock)(this.FindName("_TextTimer")));
            this.FrameBar = ((System.Windows.Controls.Grid)(this.FindName("FrameBar")));
            this._TextFrame = ((System.Windows.Controls.TextBlock)(this.FindName("_TextFrame")));
            this.BatteryBar = ((System.Windows.Controls.Grid)(this.FindName("BatteryBar")));
            this._TextBattery = ((System.Windows.Controls.TextBlock)(this.FindName("_TextBattery")));
            this._ButtonStop = ((System.Windows.Controls.Button)(this.FindName("_ButtonStop")));
            this._ButtonStart = ((System.Windows.Controls.Button)(this.FindName("_ButtonStart")));
            this._IconProcess = ((System.Windows.Controls.Grid)(this.FindName("_IconProcess")));
            this._IconEffects = ((System.Windows.Controls.Grid)(this.FindName("_IconEffects")));
            this._IconSaved = ((System.Windows.Controls.Grid)(this.FindName("_IconSaved")));
            this._introGrid = ((System.Windows.Controls.Grid)(this.FindName("_introGrid")));
            this._jeyART = ((System.Windows.Controls.TextBlock)(this.FindName("_jeyART")));
            this._jeylapse = ((System.Windows.Controls.TextBlock)(this.FindName("_jeylapse")));
        }
    }
}

