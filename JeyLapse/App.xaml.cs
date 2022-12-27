using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Navigation;
using System.Xml.Linq;
using JeyLapse.Resources;
using JeyLapse.Session;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace JeyLapse
{
    public partial class App : Application
    {
        #region Data

        private const string DBConnectionString = "Data Source=isostore:/JeyLapseProfile.sdf";

        private static SessionProfileDataContext _db;

        public static SessionProfileDataContext DB
        {
            get
            {
                if (_db == null)
                    _db = new SessionProfileDataContext(DBConnectionString);

                return _db;
            }
        }

        #endregion

        /// <summary>
        ///     Constructor for the Application object.
        /// </summary>
        public App()
        {
            SessionProfileDataContext db = DB;

            // Global handler for uncaught exceptions.
            UnhandledException += Application_UnhandledException;

            // Standard XAML initialization
            InitializeComponent();

            // Phone-specific initialization
            InitializePhoneApplication();

            // Language display initialization
            InitializeLanguage();

            // Show graphics profiling information while debugging.
            if (Debugger.IsAttached)
            {
                // Display the current frame rate counters.
                Current.Host.Settings.EnableFrameRateCounter = true;

                // Show the areas of the app that are being redrawn in each frame.
                //Application.Current.Host.Settings.EnableRedrawRegions = true;

                // Enable non-production analysis visualization mode,
                // which shows areas of a page that are handed off to GPU with a colored overlay.
                //Application.Current.Host.Settings.EnableCacheVisualization = true;

                // Prevent the screen from turning off while under the debugger by disabling
                // the application's idle detection.
                // Caution:- Use this under debug mode only. Application that disables user idle detection will continue to run
                // and consume battery power when the user is not using the phone.
                PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Disabled;
            }

            if (db.DatabaseExists() == false)
            {
                Debug.WriteLine("Initializing Database");

                db.CreateDatabase();

                ObservableCollection<SessionProfileViewModel> settings = LoadProgressDataFromXML();

                foreach (SessionProfileViewModel spvm in settings)
                {
                    db.Items.InsertOnSubmit(new SessionProfile
                    {
                        Key = spvm.Key,
                        Value = spvm.Value
                    });
                }

                db.SubmitChanges();
            }
            else
            {
                Debug.WriteLine("Database already exists");

                // Check ProgressDataVersion
                var PDVSetting = (from SessionProfile ps in db.Items
                    where ps.Key == "ProfileDataVersion"
                    select ps).First();

                int _pdversion = Int32.Parse(PDVSetting.Value);

                if (_pdversion < 2)
                {
                    db.Items.InsertOnSubmit(new SessionProfile
                    {
                        Key = "PowerSaver",
                        Value = "0"
                    });

                    PDVSetting.Value = "2";

                    db.SubmitChanges();
                }

                if (_pdversion < 3)
                {
                    db.Items.InsertOnSubmit(new SessionProfile
                    {
                        Key = "Key",
                        Value = ""
                    });

                    PDVSetting.Value = "3";

                    db.SubmitChanges();
                }

                if (_pdversion < 4)
                {
                    db.Items.InsertOnSubmit(new SessionProfile
                    {
                        Key = "WideScreen",
                        Value = "0"
                    });

                    PDVSetting.Value = "4";

                    db.SubmitChanges();
                }

                if (_pdversion < 5)
                {
                    db.Items.InsertOnSubmit(new SessionProfile
                    {
                        Key = "UnderLock",
                        Value = "0"
                    });

                    PDVSetting.Value = "5";

                    db.SubmitChanges();
                }
            }
        }

        /// <summary>
        ///     Provides easy access to the root frame of the Phone Application.
        /// </summary>
        /// <returns>The root frame of the Phone Application.</returns>
        public static PhoneApplicationFrame RootFrame { get; private set; }

        private ObservableCollection<SessionProfileViewModel> LoadProgressDataFromXML()
        {
            var items = new ObservableCollection<SessionProfileViewModel>();

            XDocument xdoc = XDocument.Load("Session/DefaultSessionProfile.xml");
            var dataEnum = xdoc.Descendants("SessionProfile");

            foreach (XElement setting in dataEnum)
            {
                SessionProfileViewModel spvm = new SessionProfileViewModel();

                spvm.Key = setting.Attribute("Key").Value;
                spvm.Value = setting.Attribute("Value").Value;

                items.Add(spvm);
            }

            return items;
        }

        // Code to execute when the application is launching (eg, from Start)
        // This code will not execute when the application is reactivated
        private void Application_Launching(object sender, LaunchingEventArgs e)
        {
        }

        // Code to execute when the application is activated (brought to foreground)
        // This code will not execute when the application is first launched
        private void Application_Activated(object sender, ActivatedEventArgs e)
        {
        }

        // Code to execute when the application is deactivated (sent to background)
        // This code will not execute when the application is closing
        private void Application_Deactivated(object sender, DeactivatedEventArgs e)
        {
        }

        // Code to execute when the application is closing (eg, user hit Back)
        // This code will not execute when the application is deactivated
        private void Application_Closing(object sender, ClosingEventArgs e)
        {
        }

        // Code to execute if a navigation fails
        private void RootFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            if (Debugger.IsAttached)
            {
                // A navigation has failed; break into the debugger
                Debugger.Break();
            }
        }

        // Code to execute on Unhandled Exceptions
        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (Debugger.IsAttached)
            {
                // An unhandled exception has occurred; break into the debugger
                Debugger.Break();
            }
        }

        // Initialize the app's font and flow direction as defined in its localized resource strings.
        //
        // To ensure that the font of your application is aligned with its supported languages and that the
        // FlowDirection for each of those languages follows its traditional direction, ResourceLanguage
        // and ResourceFlowDirection should be initialized in each resx file to match these values with that
        // file's culture. For example:
        //
        // AppResources.es-ES.resx
        //    ResourceLanguage's value should be "es-ES"
        //    ResourceFlowDirection's value should be "LeftToRight"
        //
        // AppResources.ar-SA.resx
        //     ResourceLanguage's value should be "ar-SA"
        //     ResourceFlowDirection's value should be "RightToLeft"
        //
        // For more info on localizing Windows Phone apps see http://go.microsoft.com/fwlink/?LinkId=262072.
        //
        private void InitializeLanguage()
        {
            try
            {
                // Set the font to match the display language defined by the
                // ResourceLanguage resource string for each supported language.
                //
                // Fall back to the font of the neutral language if the Display
                // language of the phone is not supported.
                //
                // If a compiler error is hit then ResourceLanguage is missing from
                // the resource file.
                RootFrame.Language = XmlLanguage.GetLanguage(AppResources.ResourceLanguage);

                // Set the FlowDirection of all elements under the root frame based
                // on the ResourceFlowDirection resource string for each
                // supported language.
                //
                // If a compiler error is hit then ResourceFlowDirection is missing from
                // the resource file.
                FlowDirection flow =
                    (FlowDirection) Enum.Parse(typeof (FlowDirection), AppResources.ResourceFlowDirection);
                RootFrame.FlowDirection = flow;
            }
            catch
            {
                // If an exception is caught here it is most likely due to either
                // ResourceLangauge not being correctly set to a supported language
                // code or ResourceFlowDirection is set to a value other than LeftToRight
                // or RightToLeft.

                if (Debugger.IsAttached)
                {
                    Debugger.Break();
                }

                throw;
            }
        }

        #region Phone application initialization

        // Avoid double-initialization
        private bool phoneApplicationInitialized;

        // Do not add any additional code to this method
        private void InitializePhoneApplication()
        {
            if (phoneApplicationInitialized)
                return;

            // Create the frame but don't set it as RootVisual yet; this allows the splash
            // screen to remain active until the application is ready to render.
            RootFrame = new PhoneApplicationFrame();
            RootFrame.Navigated += CompleteInitializePhoneApplication;

            // Handle navigation failures
            RootFrame.NavigationFailed += RootFrame_NavigationFailed;

            // Handle reset requests for clearing the backstack
            RootFrame.Navigated += CheckForResetNavigation;

            // Ensure we don't initialize again
            phoneApplicationInitialized = true;
        }

        // Do not add any additional code to this method
        private void CompleteInitializePhoneApplication(object sender, NavigationEventArgs e)
        {
            // Set the root visual to allow the application to render
            if (RootVisual != RootFrame)
                RootVisual = RootFrame;

            // Remove this handler since it is no longer needed
            RootFrame.Navigated -= CompleteInitializePhoneApplication;
        }

        private void CheckForResetNavigation(object sender, NavigationEventArgs e)
        {
            // If the app has received a 'reset' navigation, then we need to check
            // on the next navigation to see if the page stack should be reset
            if (e.NavigationMode == NavigationMode.Reset)
                RootFrame.Navigated += ClearBackStackAfterReset;
        }

        private void ClearBackStackAfterReset(object sender, NavigationEventArgs e)
        {
            // Unregister the event so it doesn't get called again
            RootFrame.Navigated -= ClearBackStackAfterReset;

            // Only clear the stack for 'new' (forward) and 'refresh' navigations
            if (e.NavigationMode != NavigationMode.New && e.NavigationMode != NavigationMode.Refresh)
                return;

            // For UI consistency, clear the entire page stack
            while (RootFrame.RemoveBackEntry() != null)
            {
                ; // do nothing
            }
        }

        #endregion
    }
}