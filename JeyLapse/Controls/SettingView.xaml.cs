using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace JeyLapse
{
    public partial class SettingView : UserControl
    {
        public static readonly DependencyProperty TextProperty;
        public static readonly DependencyProperty ImageProperty;

        public string Text
        {
            get { return (string) GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        public ImageSource Image
        {
            get { return (ImageSource)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        static SettingView()
        {
            TextProperty = DependencyProperty.Register("Text", typeof (string), typeof (SettingView), new PropertyMetadata(default(string)));
            ImageProperty = DependencyProperty.Register("Image", typeof (ImageSource), typeof (SettingView), new PropertyMetadata(null));
        }

        public SettingView()
        {
            InitializeComponent();
        }
    }
}
