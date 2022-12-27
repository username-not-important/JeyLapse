using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using JeyLapse.Editing;
using JeyLapse.EffectViews;
using JeyLapse.Session;
using JLEditorLib.Effects;
using JLEditorLib.Effects.Size;
using Microsoft.Devices;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Info;
using Microsoft.Xna.Framework.Media.PhoneExtensions;
using FlashMode = JeyLapse.Session.FlashMode;

namespace JeyLapse
{
    public partial class EffectsPage : PhoneApplicationPage
    {
        private PhotoCamera cam;
        private SessionProfileHelper helper = new SessionProfileHelper();
        private Session.Session session;
        private WriteableBitmap temp = BitmapFactory.New(1, 1);
        private WriteableBitmap wbmp;

        public EffectsPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // Check to see if the camera is available on the device.
            if (Camera.IsCameraTypeSupported(CameraType.Primary) ||
                Camera.IsCameraTypeSupported(CameraType.FrontFacing))
            {
                // Initialize the camera, when available.
                if (Camera.IsCameraTypeSupported(CameraType.Primary))
                {
                    // Use front-facing camera if available.
                    cam = new PhotoCamera(CameraType.Primary);
                }
                else
                {
                    // Otherwise, use standard camera on back of device.
                    cam = new PhotoCamera(CameraType.FrontFacing);
                }


                // Event is fired when the PhotoCamera object has been initialized.
                cam.Initialized += cam_Initialized;

                cam.CaptureStarted += cam_CaptureStarted;
                cam.CaptureCompleted += cam_CaptureCompleted;

                // Event is fired when the capture sequence is complete and an image is available.
                cam.CaptureImageAvailable += cam_CaptureImageAvailable;

                //Set the VideoBrush source to the camera.
                CameraBrush.SetSource(cam);
            }
            else
            {
                // The camera is not supported on the device.
            }

            CheckMemoryUsage();
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            if (cam != null)
            {
                // Dispose camera to minimize power consumption and to expedite shutdown.
                cam.Dispose();

                // Release memory, ensure garbage collection.
                cam.Initialized -= cam_Initialized;
                cam.CaptureCompleted -= cam_CaptureCompleted;
                cam.CaptureImageAvailable -= cam_CaptureImageAvailable;
            }

            PreviewImage.Source = null;

            ImageEditor.FxCollection.Preview = false;
            ImageEditor.FxCollection.FreeMemory();
        }

        private void cam_CaptureStarted(object sender, EventArgs e)
        {
            Dispatcher.BeginInvoke(delegate
            {
                _PreviewButton.IsEnabled = false;
                _ApplyButton.IsEnabled = false;
                _WaitMessage.Visibility = Visibility.Visible;
            });
        }

        private void cam_CaptureCompleted(object sender, CameraOperationCompletedEventArgs e)
        {
            Deployment.Current.Dispatcher.BeginInvoke(delegate
            {
                VisualStateManager.GoToState(this, "Previewing", true);

                _PreviewButton.IsEnabled = true;
                _ApplyButton.IsEnabled = true;
                _WaitMessage.Visibility = Visibility.Collapsed;
            });
        }

        private void cam_Initialized(object sender, CameraOperationCompletedEventArgs e)
        {
            if (e.Succeeded)
            {
                Dispatcher.BeginInvoke(delegate
                {
                    IEnumerable<Size> resList = cam.AvailableResolutions;
                    int resCount = resList.Count();
                    Size res;

                    for (int i = 0; i < resCount; i++)
                    {
                        res = resList.ElementAt(i);
                        if (res.Width == session.Resolution.Width && res.Height == session.Resolution.Height)
                        {
                            cam.Resolution = res;

                            double ratio = CameraCanvas.ActualHeight/res.Height;
                            CameraCanvas.Width = ratio*res.Width;

                            break;
                        }
                    }


                    switch (session.Flash)
                    {
                        case FlashMode.On:
                            cam.FlashMode = Microsoft.Devices.FlashMode.On;
                            break;
                        case FlashMode.Auto:
                            cam.FlashMode = Microsoft.Devices.FlashMode.Auto;
                            break;
                        case FlashMode.Off:
                            cam.FlashMode = Microsoft.Devices.FlashMode.Off;
                            break;
                    }
                });
            }
        }

        protected override void OnOrientationChanged(OrientationChangedEventArgs e)
        {
            if (cam != null)
            {
                // LandscapeRight rotation when camera is on back of device.
                int landscapeRightRotation = 180;

                // Change LandscapeRight rotation for front-facing camera.
                if (cam.CameraType == CameraType.FrontFacing) landscapeRightRotation = -180;

                // Rotate video brush from camera.
                if (e.Orientation == PageOrientation.LandscapeRight)
                {
                    // Rotate for LandscapeRight orientation.
                    CameraBrush.RelativeTransform =
                        new CompositeTransform {CenterX = 0.5, CenterY = 0.5, Rotation = landscapeRightRotation};
                }
                else
                {
                    // Rotate for standard landscape orientation.
                    CameraBrush.RelativeTransform =
                        new CompositeTransform {CenterX = 0.5, CenterY = 0.5, Rotation = 0};
                }
            }

            base.OnOrientationChanged(e);
        }

        private void CheckMemoryUsage()
        {
            var memoryUsageLimit = DeviceStatus.ApplicationMemoryUsageLimit/1000000;
            var memoryUsed = DeviceStatus.ApplicationCurrentMemoryUsage/1000000;

            //MessageBox.Show("Total Memory: " + memoryUsageLimit + "\nUsed Memory: " + memoryUsed);

            if (memoryUsageLimit - memoryUsed < 80)
            {
                MessageBox.Show("Your phone memory is Low. We suggest you don't use any Color Effects.",
                    "Memory Caution", MessageBoxButton.OK);
            }
            else if (memoryUsageLimit - memoryUsed < 25)
            {
                MessageBox.Show(
                    "Your phone memory is too Low to add Effects, please restart the App to prevent in-app crashes.",
                    "Memory Caution", MessageBoxButton.OK);
            }
        }

        private void cam_CaptureImageAvailable(object sender, ContentReadyEventArgs e)
        {
            string fileName = "JeyLapse-PREVIEW_" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".jpg";

            try
            {
                // Write message to the UI thread.
                Deployment.Current.Dispatcher.BeginInvoke(delegate
                {
                    wbmp = temp.FromStream(e.ImageStream);

                    ImageEditor.FxCollection.Apply(wbmp);

                    PreviewImage.Source = wbmp;

                    var pic = wbmp.SaveToMediaLibrary(fileName, true);
                    wbmp = null;

                    e.ImageStream.Close();

                    BitmapImage img = new BitmapImage();
                    img.SetSource(pic.GetPreviewImage());

                    PreviewImage.Source = img;

                    GC.Collect();

                    CheckMemoryUsage();
                });
            }
            catch
            {
            }
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            session = helper.SavedSession;

            GetEffects();
        }

        private void _PreviewButton_Click(object sender, RoutedEventArgs e)
        {
            SetEffects();

            Capture();
        }

        private void GetEffects()
        {
            foreach (IDoEffect doEffect in _IDoEffects.Children)
            {
                doEffect.SetEffect(ImageEditor.FxCollection);
            }
        }

        private void SetEffects()
        {
            ImageEditor.FxCollection = new FXCollection {Preview = true};

            foreach (IDoEffect doEffect in _IDoEffects.Children)
            {
                var fx = doEffect.GetEffect();
                if (fx != null)
                    ImageEditor.FxCollection.AddEffect(fx);
            }

            //ImageEditor.FxCollection.AddEffect(new WideScreenFX());
        }

        private void Capture()
        {
            if (cam != null)
            {
                try
                {
                    // Start image capture.
                    cam.CaptureImage();
                }
                catch (Exception ex)
                {
                    Dispatcher.BeginInvoke(delegate { });
                }
            }
        }

        private void _IconClose_Tap(object sender, GestureEventArgs e)
        {
            VisualStateManager.GoToState(this, "Capturing", true);
        }

        private void _ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            SetEffects();
            try
            {
                NavigationService.GoBack();
            }
            catch (Exception)
            {
            }
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            var result =
                MessageBox.Show(
                    "If you do not apply your settings they'll be lost. Are you sure you Don't want to save?",
                    "Back", MessageBoxButton.OKCancel);

            if (result == MessageBoxResult.Cancel)
                e.Cancel = true;

            base.OnBackKeyPress(e);
        }
    }
}