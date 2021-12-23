using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using ModernFlyouts.Settings;

namespace ModernFlyouts.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private SettingsWindow SettingsUIWindow;

        public bool ShowOobe { get; set; }

        public Type StartupPage { get; set; } = typeof(ModernFlyouts.Settings.Views.GeneralSettings);


        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            // TODO WTS: Add your app in the app center and set your secret here. More at https://docs.microsoft.com/appcenter/sdk/getting-started/uwp
            AppCenter.Start("{Your App Secret}", typeof(Analytics), typeof(Crashes));
        }

/////////////////////
        public void OpenSettingsWindow(Type type)
        {
            if (SettingsUIWindow == null)
            {
                SettingsUIWindow = new SettingsWindow();
            }
            else if (SettingsUIWindow.WindowState == WindowState.Minimized)
            {
                SettingsUIWindow.WindowState = WindowState.Normal;
            }

            SettingsUIWindow.Show();
            SettingsUIWindow.NavigateToSection(type);
        }

        private void InitHiddenSettingsWindow()
        {
            SettingsUIWindow = new SettingsWindow();

            Utils.ShowHide(SettingsUIWindow);
            Utils.CenterToScreen(SettingsUIWindow);
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            if (!ShowOobe)
            {
                SettingsUIWindow = new SettingsWindow();
                SettingsUIWindow.Show();
                SettingsUIWindow.NavigateToSection(StartupPage);
            }
            else
            {
                // Create the Settings window so that it's fully initialized and
                // it will be ready to receive the notification if the user opens
                // the Settings from the tray icon.
                InitHiddenSettingsWindow();

                //OobeWindow oobeWindow = new OobeWindow();
                //oobeWindow.Show();
            }
        }

    }
}