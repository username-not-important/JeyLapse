using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace JeyLapse
{
    public partial class HelpPage : PhoneApplicationPage
    {
        public HelpPage()
        {
            InitializeComponent();

            _Radio0.IsChecked = true;
        }

        private void _Radio1_Checked(object sender, RoutedEventArgs e)
        {
            _0.Visibility = Visibility.Collapsed;
            _1.Visibility = Visibility.Visible;
            _2.Visibility = Visibility.Collapsed;
			_3.Visibility = Visibility.Collapsed;
        }

        private void _Radio0_Checked(object sender, RoutedEventArgs e)
        {
            _0.Visibility = Visibility.Visible;
            _1.Visibility = Visibility.Collapsed;
            _2.Visibility = Visibility.Collapsed;
			_3.Visibility = Visibility.Collapsed;
        }

        private void _Radio2_Checked(object sender, RoutedEventArgs e)
        {
            _0.Visibility = Visibility.Collapsed;
            _1.Visibility = Visibility.Collapsed;
            _2.Visibility = Visibility.Visible;
			_3.Visibility = Visibility.Collapsed;
        }

        private void _Radio3_Checked(object sender, RoutedEventArgs e)
        {
            _0.Visibility = Visibility.Collapsed;
            _1.Visibility = Visibility.Collapsed;
            _2.Visibility = Visibility.Collapsed;
			_3.Visibility = Visibility.Visible;
        }
    }
}