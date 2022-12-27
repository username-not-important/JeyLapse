using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Threading;
using Windows.ApplicationModel.Store;
using Windows.Phone.Devices.Power;
using JeyLapse.Editing;
using JeyLapse.Operations;
using JeyLapse.Session;
using JLEditorLib.Effects.Size;
using Microsoft.Devices;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Xna.Framework.Media;
using FlashMode = JeyLapse.Session.FlashMode;

namespace JeyLapse
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        private PhotoCamera cam;
        private SessionProfileHelper helper = new SessionProfileHelper();

        private MediaLibrary library = new MediaLibrary();
        private PowerSaverAgent psAgent;
        private int savedCounter;
        private Session.Session session;
        private bool sessionStarted;
        private DispatcherTimer t = new DispatcherTimer();
        private TimeSpan ts;

        private bool onLock = false;

        public MainPage()
        {
            InitializeComponent();

            _jeyART.Opacity = _jeylapse.Opacity = CameraCanvas.Opacity = RightPanel.Opacity = 0.0;

            var frame = App.RootFrame;
            frame.Obscured += frame_Obscured;
            frame.Unobscured += frame_Unobscured;
        }

        void frame_Obscured(object sender, ObscuredEventArgs e)
        {
            if (!e.IsLocked)
                return;

            onLock = true;
            Storyboard blinkStoryboard = Resources["EyeBlinkStoryboard"] as Storyboard;
            blinkStoryboard.Stop();
        }

        void frame_Unobscured(object sender, EventArgs e)
        {
            onLock = false;
            Storyboard blinkStoryboard = Resources["EyeBlinkStoryboard"] as Storyboard;
            blinkStoryboard.Begin();
        }

        public PowerSaverAgent PowerAgent
        {
            get { return psAgent; }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ImageEditor.FxCollection.Preview = false;

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

                // Event is fired when the capture sequence is complete.
                cam.CaptureCompleted += cam_CaptureCompleted;

                // Event is fired when the capture sequence is complete and an image is available.
                cam.CaptureImageAvailable += cam_CaptureImageAvailable;

                cam.CaptureStarted += cam_CaptureStarted;

                // The event is fired when the viewfinder is tapped (for focus).
                CameraCanvas.Tap += focus_Tapped;

                // The event is fired when the shutter button receives a half press.
                CameraButtons.ShutterKeyHalfPressed += OnButtonHalfPress;

                // The event is fired when the shutter button is released.
                CameraButtons.ShutterKeyReleased += OnButtonRelease;

                //Set the VideoBrush source to the camera.
                CameraBrush.SetSource(cam);
            }
            else
            {
                // The camera is not supported on the device.
                Dispatcher.BeginInvoke(delegate
                {
                    // Write message.
                    MessageBox.Show("No camera is supported on this device.");
                });
            }
        }

        private void cam_CaptureStarted(object sender, EventArgs e)
        {
            Dispatcher.BeginInvoke(delegate
            {
                Storyboard processStoryboard = Resources["ProcessingStoryboard"] as Storyboard;
                processStoryboard.Begin();
            });
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
                cam.CaptureStarted -= cam_CaptureStarted;


                CameraButtons.ShutterKeyHalfPressed -= OnButtonHalfPress;
                CameraButtons.ShutterKeyReleased -= OnButtonRelease;
            }
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

                            if (session.WideScreen)
                            {
                                CameraCanvas.Width = 1.77777777 * CameraCanvas.ActualHeight;
                                CameraBrush.Stretch = Stretch.UniformToFill;
                            }
                            else
                            {
                                double ratio = CameraCanvas.ActualHeight / res.Height;
                                CameraCanvas.Width = ratio * res.Width;
                            }

                            break;
                        }
                    }

                    psAgent = new PowerSaverAgent(session.PowerSaver);
                    psAgent.PropertyChanged += psAgent_PropertyChanged;

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

                    _SettingDuration.Text = session.Duration.TotalMinutes + " (m)";
                    _SettingFlash.Text = session.Flash == FlashMode.On
                        ? "On"
                        : (session.Flash == FlashMode.Off ? "Off" : "Auto");

                    _SettingInterval.Text = session.Interval.TotalSeconds.ToString();

                    _TextRes.Text = cam.Resolution.Width + "x" + (session.WideScreen ? (int)(cam.Resolution.Height / 1.777777) : cam.Resolution.Height);

                    _SettingEffects.Text = ImageEditor.FxCollection.Count + " Effects";

                    Battery.GetDefault().RemainingChargePercentChanged += MainPage_RemainingChargePercentChanged;
                    MainPage_RemainingChargePercentChanged(this, null);
                });
            }
        }

        private void MainPage_RemainingChargePercentChanged(object sender, object e)
        {
            Dispatcher.BeginInvoke(delegate
            {
                _TextBattery.Text = Battery.GetDefault().RemainingChargePercent + "%";
            });
        }

        private void psAgent_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            _BlackGrid.Visibility = psAgent.BlackScreenVisibility;
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
                        new CompositeTransform { CenterX = 0.5, CenterY = 0.5, Rotation = landscapeRightRotation };
                }
                else
                {
                    // Rotate for standard landscape orientation.
                    CameraBrush.RelativeTransform =
                        new CompositeTransform { CenterX = 0.5, CenterY = 0.5, Rotation = 0 };
                }
            }

            base.OnOrientationChanged(e);
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
                    //Dispatcher.BeginInvoke(delegate { });
                }
            }
        }

        private void cam_CaptureCompleted(object sender, CameraOperationCompletedEventArgs e)
        {
            // Increments the savedCounter variable used for generating JPEG file names.
            savedCounter++;

            if (!onLock)
                Dispatcher.BeginInvoke(delegate
                {
                    Storyboard processStoryboard = Resources["ProcessingFinishedStoryboard"] as Storyboard;
                    processStoryboard.Begin();
                });
        }

        private void cam_CaptureImageAvailable(object sender, ContentReadyEventArgs e)
        {
            string fileName = "JeyLapse_" + session.Key + "_" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + savedCounter + ".jpg";

            try
            {
                // Write message to the UI thread.
                Deployment.Current.Dispatcher.BeginInvoke(delegate
                {
                    if (ImageEditor.FxCollection.Count == 0 && !session.WideScreen)
                        library.SavePictureToCameraRoll(fileName, e.ImageStream);
                    else
                    {
                        WriteableBitmap wbmp = BitmapFactory.New(1, 1).FromStream(e.ImageStream);

                        if (session.WideScreen)
                        {
                            WideScreenFX fx = new WideScreenFX();
                            wbmp = fx.Apply(wbmp);
                        }

                        ImageEditor.FxCollection.Apply(wbmp);
                        wbmp.SaveToMediaLibrary(fileName, true);
                    }

                    e.ImageStream.Close();

                    if (!onLock)
                        SavedStoryboard.Begin();
                });
            }
            catch (Exception)
            {

            }
        }

        private void focus_Tapped(object sender, GestureEventArgs e)
        {
            if (sessionStarted)
                return;

            if (cam != null)
            {
                if (cam.IsFocusAtPointSupported)
                {
                    try
                    {
                        // Determine location of tap.
                        Point tapLocation = e.GetPosition(CameraCanvas);

                        // Position focus brackets with estimated offsets.
                        //focusBrackets.SetValue(Canvas.LeftProperty, tapLocation.X - 30);
                        //focusBrackets.SetValue(Canvas.TopProperty, tapLocation.Y - 28);

                        // Determine focus point.
                        double focusXPercentage = tapLocation.X / CameraCanvas.ActualWidth;
                        double focusYPercentage = tapLocation.Y / CameraCanvas.ActualHeight;

                        // Show focus brackets and focus at point
                        //focusBrackets.Visibility = Visibility.Visible;
                        cam.FocusAtPoint(focusXPercentage, focusYPercentage);

                    }
                    catch (Exception focusError)
                    {

                    }
                }
            }
        }

        // Provide auto-focus with a half button press using the hardware shutter button.
        private void OnButtonHalfPress(object sender, EventArgs e)
        {
            if (sessionStarted)
                return;

            if (cam != null)
            {
                // Focus when a capture is not in progress.
                try
                {
                    cam.Focus();
                }
                catch (Exception focusError)
                {
                }
            }
        }

        // Cancel the focus if the half button press is released using the hardware shutter button.
        private void OnButtonRelease(object sender, EventArgs e)
        {
            if (cam != null)
            {
                try
                {
                    cam.CancelFocus();
                }
                catch (Exception)
                {
                }
            }
        }

        private void _ButtonStart_Click(object sender, RoutedEventArgs e)
        {
            psAgent.IsEnabled = true;

            PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Disabled;

            PhoneApplicationService.Current.ApplicationIdleDetectionMode = session.UnderLock ? IdleDetectionMode.Disabled : IdleDetectionMode.Enabled;

            _IconEffects.Visibility = ImageEditor.FxCollection.Count != 0 ? Visibility.Visible : Visibility.Collapsed;

            sessionStarted = true;
            VisualStateManager.GoToState(this, "Capturing", true);

            if (t == null)
                t = new DispatcherTimer();
            else
            {
                t.Stop();
            }

            ts = new TimeSpan();
            _TextTimer.Text = "00:00:00";
            _TextFrame.Text = "0";

            t.Interval = TimeSpan.FromSeconds(1);

            t.Tick += t_Tick;

            t.Start();
        }

        private void t_Tick(object sender, EventArgs e)
        {
            if (session.Duration.TotalSeconds != 0 && ts.TotalSeconds >= session.Duration.TotalSeconds)
            {
                _ButtonStop_Click(this, new RoutedEventArgs());
                MessageBox.Show("Session Complete ;)");
                return;
            }

            if (sessionStarted)
            {
                psAgent.Tick();
            }

            if ((ts.TotalSeconds + 1) % session.Interval.TotalSeconds == 0)
            {
                Capture();

                _TextFrame.Text = "#" + (savedCounter + 1);
            }

            ts = ts.Add(TimeSpan.FromSeconds(1));
            _TextTimer.Text = ts.ToString("hh\\:mm\\:ss");
        }

        private void _ButtonStop_Click(object sender, RoutedEventArgs e)
        {
            psAgent.IsEnabled = false;

            PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Enabled;

            try
            {
                PhoneApplicationService.Current.ApplicationIdleDetectionMode = IdleDetectionMode.Enabled;
            }
            catch (Exception)
            {

            }

            t.Stop();
            t.Tick -= t_Tick;

            savedCounter = 0;

            sessionStarted = false;
            VisualStateManager.GoToState(this, "Stopped", true);
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            Storyboard blinkStoryboard = Resources["EyeBlinkStoryboard"] as Storyboard;
            blinkStoryboard.Begin();

            Storyboard startupStoryboard = Resources["PageStartupStoryboard"] as Storyboard;
            startupStoryboard.Begin();

            session = helper.SavedSession;
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            if (sessionStarted)
            {
                var prompt = MessageBox.Show("This will stop the current session, are you sure?", "Back",
                    MessageBoxButton.OKCancel);

                if (prompt == MessageBoxResult.Cancel)
                    e.Cancel = true;
            }

            base.OnBackKeyPress(e);
        }

        private void _BlackGrid_Tap(object sender, GestureEventArgs e)
        {
            psAgent.TouchBlack();
        }
    }
}