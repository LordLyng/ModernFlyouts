using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Windows.Data.Json;
using System.Windows.Shapes;

using System.ComponentModel;
using System.Drawing;
using System.Windows.Interop;
using ModernFlyouts.Settings.Views;
using Microsoft.Toolkit.Wpf.UI.XamlHost;
using ModernFlyouts.Settings.Helpers;
using ModernFlyouts.Settings;
using Windows.ApplicationModel.Resources;

namespace ModernFlyouts.WPF
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private static Window inst;

        private bool isOpen = true;

        public SettingsWindow()
        {
            InitializeComponent();

            ResourceLoader loader = ResourceLoader.GetForViewIndependentUse();
            Title = loader.GetString("SettingsWindow_Title");
          
        }

        public static void CloseHiddenWindow()
        {
            if (inst != null && inst.Visibility == Visibility.Hidden)
            {
                inst.Close();
            }
        }

        private void WindowsXamlHost_ChildChanged(object sender, EventArgs e)
        {
            // If sender is null, it could lead to a NullReferenceException. This might occur on restarting as admin (check https://github.com/microsoft/PowerToys/issues/7393 for details)
            if (sender == null)
            {
                return;
            }

            // Hook up x:Bind source.
            WindowsXamlHost windowsXamlHost = sender as WindowsXamlHost;
            ShellPage shellPage = windowsXamlHost.GetUwpInternalObject() as ShellPage;


            // XAML Islands: If the window is open, explicitly force it to be shown to solve the blank dialog issue https://github.com/microsoft/PowerToys/issues/3384
            if (isOpen)
            {
                try
                {
                    Show();
                }
                catch (InvalidOperationException)
                {
                }
            }
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

            // XAML Islands: If the window is closed while minimized, exit the process. Required to avoid process not terminating issue - https://github.com/microsoft/PowerToys/issues/4430
            if (WindowState == WindowState.Minimized)
            {
                // Run Environment.Exit on a separate task to avoid performance impact
                System.Threading.Tasks.Task.Run(() => { Environment.Exit(0); });
            }
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            inst = (Window)sender;
        }

        private void MainWindow_Activated(object sender, EventArgs e)
        {
            if (((Window)sender).Visibility == Visibility.Hidden)
            {
                ((Window)sender).Visibility = Visibility.Visible;
            }
        }
    }
}




//using System;
//using System.Windows;
//using System.Windows.Input;

//using Microsoft.Toolkit.Wpf.UI.XamlHost;

//using ModernFlyouts.Flyouts;

//using Windows.UI.Xaml.Controls;

//namespace TaskbarFlyout.Wpf
//{
//    public partial class MainWindow : Window
//    {
//        private readonly FlyoutWindow _flyoutWindow;

//        public MainWindow()
//        {
//            InitializeComponent();

//            _flyoutWindow = new FlyoutWindow();
//            _flyoutWindow.Show();
//        }

//        private void DragWindow(object sender, MouseButtonEventArgs e)
//        {
//            DragMove();
//        }

//        private void OnCloseButtonControlLoaded(object sender, EventArgs e)
//        {
//            var control = (sender as WindowsXamlHost).Child as CloseButtonControl;

//            if (control == null)
//            {
//                return;
//            }

//          (control.Content as Button).Click += (_, __) =>
//          {
//              Application.Current.Shutdown();
//          };
//        }

//        private void OnMainControlLoaded(object sender, EventArgs e)
//        {
//            var control = (sender as WindowsXamlHost).Child as MainControl;

//            if (control == null)
//            {
//                return;
//            }

//            control.ShowHideFlyoutWindowButton.Click += (_, __) =>
//            {
//                var isChecked = (bool)control.ShowHideFlyoutWindowButton.IsChecked;
//                _flyoutWindow.Visibility = isChecked ? Visibility.Visible : Visibility.Hidden;
//            };

//            control.ShowFlyoutButton.Click += (_, __) =>
//            {
//                _flyoutWindow.ShowFlyout();
//            };

//            control.CenterFlyoutWindowButton.Click += (_, __) =>
//            {
//                var screenWidth = SystemParameters.PrimaryScreenWidth;
//                var screenHeight = SystemParameters.PrimaryScreenHeight;
//                var windowWidth = _flyoutWindow.Width;
//                var windowHeight = _flyoutWindow.Height;

//                _flyoutWindow.Left = (screenWidth / 2) - (windowWidth / 2);
//                _flyoutWindow.Top = (screenHeight / 2) - (windowHeight / 2);
//            };
//        }
//    }
//}