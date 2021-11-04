using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ModernFlyouts.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
    }
}



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
