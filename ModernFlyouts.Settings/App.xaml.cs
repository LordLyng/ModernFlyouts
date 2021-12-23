using System;
using System.Globalization;
using System.Reflection;
using Microsoft.Toolkit.Win32.UI.XamlHost;
using ModernFlyouts.Settings.Helpers;
using ModernFlyouts.Settings.Services;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;

namespace ModernFlyouts.Settings
{
    public sealed partial class App : XamlApplication
    {
        private Lazy<ActivationService> _activationService;

        private ActivationService ActivationService
        {
            get { return _activationService.Value; }
        }

        public App()
        {
            Initialize();

            // Hide the Xaml Island window
            var coreWindow = Windows.UI.Core.CoreWindow.GetForCurrentThread();
            var coreWindowInterop = Interop.GetInterop(coreWindow);
            NativeMethods.ShowWindow(coreWindowInterop.WindowHandle, Interop.SW_HIDE);

            UnhandledException += OnAppUnhandledException;

            //// Deferred execution until used. Check https://docs.microsoft.com/dotnet/api/system.lazy-1 for further info on Lazy<T> class.
            //_activationService = new Lazy<ActivationService>(CreateActivationService);
        }

        protected override async void OnLaunched(LaunchActivatedEventArgs args)
        {
            if (!args.PrelaunchActivated)
            {
                await ActivationService.ActivateAsync(args);
            }
        }

        protected override async void OnActivated(IActivatedEventArgs args)
        {
            await ActivationService.ActivateAsync(args);
        }

        private void OnAppUnhandledException(object sender, Windows.UI.Xaml.UnhandledExceptionEventArgs e)
        {
            // TODO WTS: Please log and handle the exception as appropriate to your scenario
            // For more info see https://docs.microsoft.com/uwp/api/windows.ui.xaml.application.unhandledexception
        }


        private ActivationService CreateActivationService()
        {
            return new ActivationService(this, typeof(Views.ShellPage), new Lazy<UIElement>(CreateShell));
        }

        private UIElement CreateShell()
        {
            return new Views.ShellPage();
        }
        //public static string AppVersion
        //{
        //    get => Assembly.GetExecutingAssembly().GetName().Version.ToString();
        //}
        public string AppVersion
        {
            get
            {
                var AppVersion = Package.Current.Id.Version;
                return string.Format($"{AppVersion.Major}.{AppVersion.Minor}.{AppVersion.Build}.{AppVersion.Revision}");
            }
        }


        //        private MainWindow settingsWindow;

        //        public bool ShowOobe { get; set; }

        //        public Type StartupPage { get; set; } = typeof(Microsoft.PowerToys.Settings.UI.Views.GeneralPage);

        //        public void OpenSettingsWindow(Type type)
        //        {
        //            if (settingsWindow == null)
        //            {
        //                settingsWindow = new MainWindow();
        //            }
        //            else if (settingsWindow.WindowState == WindowState.Minimized)
        //            {
        //                settingsWindow.WindowState = WindowState.Normal;
        //            }

        //            settingsWindow.Show();
        //            settingsWindow.NavigateToSection(type);
        //        }

        //        private void InitHiddenSettingsWindow()
        //        {
        //            settingsWindow = new MainWindow();

        //            Utils.ShowHide(settingsWindow);
        //            Utils.CenterToScreen(settingsWindow);
        //        }

        //        private void Application_Startup(object sender, StartupEventArgs e)
        //        {
        //            if (!ShowOobe)
        //            {
        //                settingsWindow = new MainWindow();
        //                settingsWindow.Show();
        //                settingsWindow.NavigateToSection(StartupPage);
        //            }
        //        }


    }
}




//namespace ModernFlyouts.Settings
//{
//    public sealed partial class App : XamlApplication
//{
//        private Lazy<ActivationService> _activationService;

//        private ActivationService ActivationService
//        {
//            get { return _activationService.Value; }
//        }

//        public App()
//        {
//            InitializeComponent();

//            // TODO WTS: Add your app in the app center and set your secret here. More at https://docs.microsoft.com/appcenter/sdk/getting-started/uwp
//            AppCenter.Start("{Your App Secret}", typeof(Analytics), typeof(Crashes));
//            UnhandledException += OnAppUnhandledException;

//            // Deferred execution until used. Check https://docs.microsoft.com/dotnet/api/system.lazy-1 for further info on Lazy<T> class.
//            _activationService = new Lazy<ActivationService>(CreateActivationService);
//        }

//        protected override async void OnLaunched(LaunchActivatedEventArgs args)
//        {
//            if (!args.PrelaunchActivated)
//            {
//                await ActivationService.ActivateAsync(args);
//            }
//        }

//        protected override async void OnActivated(IActivatedEventArgs args)
//        {
//            await ActivationService.ActivateAsync(args);
//        }

//        private void OnAppUnhandledException(object sender, Windows.UI.Xaml.UnhandledExceptionEventArgs e)
//        {
//            // TODO WTS: Please log and handle the exception as appropriate to your scenario
//            // For more info see https://docs.microsoft.com/uwp/api/windows.ui.xaml.application.unhandledexception
//        }

//        private ActivationService CreateActivationService()
//        {
//            return new ActivationService(this, typeof(Views.MainPage), new Lazy<UIElement>(CreateShell));
//        }

//        private UIElement CreateShell()
//        {
//            return new Views.ShellPage();
//        }
//        //public static string AppVersion
//        //{
//        //    get => Assembly.GetExecutingAssembly().GetName().Version.ToString();
//        //}
//        public string AppVersion
//        {
//            get
//            {
//                var AppVersion = Package.Current.Id.Version;
//                return string.Format($"{AppVersion.Major}.{AppVersion.Minor}.{AppVersion.Build}.{AppVersion.Revision}");
//            }
//        }
//    }
// }


//public static bool IsDarkTheme()
//{
//    var selectedTheme = SettingsRepository<GeneralSettings>.GetInstance(settingsUtils).SettingsConfig.Theme.ToUpper(CultureInfo.InvariantCulture);
//    var defaultTheme = new Windows.UI.ViewManagement.UISettings();
//    var uiTheme = defaultTheme.GetColorValue(Windows.UI.ViewManagement.UIColorType.Background).ToString(System.Globalization.CultureInfo.InvariantCulture);
//    return selectedTheme == "DARK" || (selectedTheme == "SYSTEM" && uiTheme == "#FF000000");
//}

//private static ISettingsUtils settingsUtils = new SettingsUtils();

//}


//    public sealed partial class App : Application
//    {
//        private Lazy<ActivationService> _activationService;

//        private ActivationService ActivationService
//        {
//            get { return _activationService.Value; }
//        }

//        public App()
//        {
//            InitializeComponent();

//            // TODO WTS: Add your app in the app center and set your secret here. More at https://docs.microsoft.com/appcenter/sdk/getting-started/uwp
//            AppCenter.Start("{Your App Secret}", typeof(Analytics), typeof(Crashes));
//            UnhandledException += OnAppUnhandledException;

//            // Deferred execution until used. Check https://docs.microsoft.com/dotnet/api/system.lazy-1 for further info on Lazy<T> class.
//            _activationService = new Lazy<ActivationService>(CreateActivationService);
//        }

//        protected override async void OnLaunched(LaunchActivatedEventArgs args)
//        {
//            if (!args.PrelaunchActivated)
//            {
//                await ActivationService.ActivateAsync(args);
//            }
//        }

//        protected override async void OnActivated(IActivatedEventArgs args)
//        {
//            await ActivationService.ActivateAsync(args);
//        }

//        private void OnAppUnhandledException(object sender, Windows.UI.Xaml.UnhandledExceptionEventArgs e)
//        {
//            // TODO WTS: Please log and handle the exception as appropriate to your scenario
//            // For more info see https://docs.microsoft.com/uwp/api/windows.ui.xaml.application.unhandledexception
//        }

//        private ActivationService CreateActivationService()
//        {
//            return new ActivationService(this, typeof(Views.MainPage), new Lazy<UIElement>(CreateShell));
//        }

//        private UIElement CreateShell()
//        {
//            return new Views.ShellPage();
//        }
//        //public static string AppVersion
//        //{
//        //    get => Assembly.GetExecutingAssembly().GetName().Version.ToString();
//        //}
//        public string AppVersion
//        {
//            get
//            {
//                var AppVersion = Package.Current.Id.Version;
//                return string.Format($"{AppVersion.Major}.{AppVersion.Minor}.{AppVersion.Build}.{AppVersion.Revision}");
//            }
//        }
//    }
//}





//using System;
//using System.Windows;
//using Microsoft.PowerToys.Settings.UI.Library.Telemetry.Events;
//using Microsoft.PowerToys.Telemetry;

//namespace PowerToys.Settings
//{
//    /// <summary>
//    /// Interaction logic for App.xaml.
//    /// </summary>
//    public partial class App : Application
//    {
//        private MainWindow settingsWindow;

//        public bool ShowOobe { get; set; }

//        public Type StartupPage { get; set; } = typeof(Microsoft.PowerToys.Settings.UI.Views.GeneralPage);

//        public void OpenSettingsWindow(Type type)
//        {
//            if (settingsWindow == null)
//            {
//                settingsWindow = new MainWindow();
//            }
//            else if (settingsWindow.WindowState == WindowState.Minimized)
//            {
//                settingsWindow.WindowState = WindowState.Normal;
//            }

//            settingsWindow.Show();
//            settingsWindow.NavigateToSection(type);
//        }

//        private void InitHiddenSettingsWindow()
//        {
//            settingsWindow = new MainWindow();

//            Utils.ShowHide(settingsWindow);
//            Utils.CenterToScreen(settingsWindow);
//        }

//        private void Application_Startup(object sender, StartupEventArgs e)
//        {
//            if (!ShowOobe)
//            {
//                settingsWindow = new MainWindow();
//                settingsWindow.Show();
//                settingsWindow.NavigateToSection(StartupPage);
//            }
//            else
//            {
//                PowerToysTelemetry.Log.WriteEvent(new OobeStartedEvent());

//                // Create the Settings window so that it's fully initialized and
//                // it will be ready to receive the notification if the user opens
//                // the Settings from the tray icon.
//                InitHiddenSettingsWindow();

//                OobeWindow oobeWindow = new OobeWindow();
//                oobeWindow.Show();
//            }
//        }
//    }
//}
