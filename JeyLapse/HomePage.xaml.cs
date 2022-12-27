using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Navigation;
using Windows.ApplicationModel;
using JeyLapse.Operations;
using JeyLapse.Session;
using Microsoft.Devices;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;
using FlashMode = JeyLapse.Session.FlashMode;

namespace JeyLapse
{
    public partial class HomePage : PhoneApplicationPage
    {
        private PhotoCamera _cam;
        private int _currentResIndex;

        private SessionProfileHelper helper = new SessionProfileHelper();

        public HomePage()
        {
            InitializeComponent();

            _currentResIndex = 0;
            if (Camera.IsCameraTypeSupported(CameraType.Primary) ||
                Camera.IsCameraTypeSupported(CameraType.FrontFacing))
            {
                _cam = Camera.IsCameraTypeSupported(CameraType.Primary)
                    ? new PhotoCamera(CameraType.Primary)
                    : new PhotoCamera(CameraType.FrontFacing);
            }

            RefreshResolutions();

            Session.Session s = helper.SavedSession;

            _SliderDuration.Value = s.Duration.TotalMinutes;
            _SliderInterval.Value = s.Interval.TotalSeconds;

            switch (s.Flash)
            {
                case FlashMode.Off:
                    _RadioFlashOff.IsChecked = true;
                    break;
                case FlashMode.On:
                    _RadioFlashOn.IsChecked = true;
                    break;
                case FlashMode.Auto:
                    _RadioFlashAuto.IsChecked = true;
                    break;
            }

            _CheckWideScreen.IsChecked = s.WideScreen;
            _CheckWorkLock.IsChecked = s.UnderLock;
        }

        public PowerSaverManager PowerSaverManager
        {
            get { return Resources["PowerSaverManager"] as PowerSaverManager; }
        }

        private void RefreshResolutions()
        {
            IEnumerable<Size> resList = _cam.AvailableResolutions;
            int resCount = resList.Count();
            Size res;

            for (int i = 0; i < resCount; i++)
            {
                res = resList.ElementAt(i);
            }

            res = resList.ElementAt((_currentResIndex + 1)%resCount);

            _currentResIndex = (_currentResIndex + 1)%resCount;

            _ButtonAspect.Content = res.Width + "x" + res.Height;
        }

        private bool SaveSessionData()
        {
            int interval = (int) _SliderInterval.Value;
            int duration = (int) _SliderDuration.Value;
            FlashMode mode = FlashMode.Auto;
            bool wideScreen = _CheckWideScreen.IsChecked != null && _CheckWideScreen.IsChecked.Value;
            bool underLock = _CheckWorkLock.IsChecked != null && _CheckWorkLock.IsChecked.Value;

            string key = _BoxKey.Text.Trim();

            if (_RadioFlashOff.IsChecked.Value)
                mode = FlashMode.Off;
            else if (_RadioFlashOn.IsChecked.Value)
                mode = FlashMode.On;

            string aspect = _ButtonAspect.Content.ToString();

            Size res = new Size(Double.Parse(aspect.Substring(0, aspect.IndexOf('x'))),
                Double.Parse(aspect.Substring(aspect.IndexOf('x') + 1)));

            helper.SavedSession = new Session.Session(TimeSpan.FromSeconds(interval), TimeSpan.FromMinutes(duration),
                mode, res, PowerSaverManager.CurrentProfile.Mode, wideScreen, underLock,key);

            try
            {
                if (!string.IsNullOrEmpty(key))
                {
                    var file = new FileInfo(key);
                }
            }
            catch (NotSupportedException)
            {
                MessageBox.Show(
                    "You should enter a correct Key for your session.\nNote that it will be used in pictures file names.",
                    "Incorrect Key", MessageBoxButton.OK);

                return false;
            }

            return true;
        }

        private void _ButtonStart_Click(object sender, RoutedEventArgs e)
        {
            if (!SaveSessionData())
                return;

            if (NetworkNotifier.ManageNavigation())
                NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        private void _ButtonAspect_OnClick(object sender, RoutedEventArgs e)
        {
            RefreshResolutions();
        }

        private void _ButtonRate_OnClick(object sender, RoutedEventArgs e)
        {
            MarketplaceReviewTask marketplaceReviewTask = new MarketplaceReviewTask();
            marketplaceReviewTask.Show();

        }

        private void _ButtonContact_OnClick(object sender, RoutedEventArgs e)
        {
            EmailComposeTask emailComposeTask = new EmailComposeTask();

            emailComposeTask.Subject = @"در مورد برنامه جی لپس";
            emailComposeTask.Body = "";
            emailComposeTask.To = "ac_ali2582@yahoo.com";

            emailComposeTask.Show();
        }

        private void _ButtonJeyLapseVideo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<Package> apps =
                    Windows.Phone.Management.Deployment.InstallationManager.FindPackagesForCurrentPublisher().ToList();

                Package jeyLapseVideo = apps.First(p => p.Id.Name == "Jey Lapse Video");

                if (jeyLapseVideo == null)
                {
                    MessageBox.Show("You haven't installed 'Jey Lapse Video' Yet! Go to store and download it.");
                }
                else
                {
                    jeyLapseVideo.Launch("");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Download 'Jey Lapse Video' app from WP Store and enjoy :)");
            }
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.Back)
                SaveSessionData();

            base.OnNavigatingFrom(e);
        }

        private void _ButtonDonate_OnClick(object sender, RoutedEventArgs e)
        {
            string info = "Account Info: \nBank Melli Iran\nAlireza Joonbakhsh Najaf-Abadi\n\n6037-9914-4685-6290";

            MessageBox.Show(info, "Donation Info", MessageBoxButton.OK);
        }

        private void _HelpPersian_Click(object sender, RoutedEventArgs e)
        {
            SaveSessionData();

            NavigationService.Navigate(new Uri("/HelpPage.xaml", UriKind.Relative));
        }

        private void _ButtonSetEffects_Click(object sender, RoutedEventArgs e)
        {
            SaveSessionData();

            NavigationService.Navigate(new Uri("/Pages/EffectsPage.xaml", UriKind.Relative));
        }

        private void _ButtonPower_Click(object sender, RoutedEventArgs e)
        {
            PowerSaverManager.CycleProfile();
        }


    }
}