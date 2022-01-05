using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using ModernFlyouts.Settings.Services;
using ModernFlyouts.Settings.ViewModels;
using ModernFlyouts.Settings.Helpers;
using Windows.Data.Json;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Automation.Peers;
using Windows.UI.Xaml.Controls;
using WinUI = Microsoft.UI.Xaml.Controls;

using Windows.ApplicationModel.Core;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace ModernFlyouts.Settings.Views
{
    /// <summary>
    /// Root page.
    /// </summary>
    public sealed partial class ShellPage : UserControl
    {
        /// <summary>
        /// Declaration for the ipc callback function.
        /// </summary>
        /// <param name="msg">message.</param>
        public delegate void IPCMessageCallback(string msg);

        /// <summary>
        /// Gets or sets a shell handler to be used to update contents of the shell dynamically from page within the frame.
        /// </summary>
        public static ShellPage ShellHandler { get; set; }

        /// <summary>
        /// Gets or sets iPC default callback function.
        /// </summary>
        public static IPCMessageCallback DefaultSndMSGCallback { get; set; }

        /// <summary>
        /// Gets view model.
        /// </summary>
        public ShellViewModel ViewModel { get; } = new ShellViewModel();

        /// <summary>
        /// Gets a collection of functions that handle IPC responses.
        /// </summary>
        public List<System.Action<JsonObject>> IPCResponseHandleList { get; } = new List<System.Action<JsonObject>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ShellPage"/> class.
        /// Shell page constructor.
        /// </summary>
        public ShellPage()
        {
            InitializeComponent();

            DataContext = ViewModel;
            ShellHandler = this;
            ViewModel.Initialize(shellFrame, navigationView, KeyboardAccelerators);
            shellFrame.Navigate(typeof(GeneralSettingsPage));
        }

        public static int SendDefaultIPCMessage(string msg)
        {
            DefaultSndMSGCallback?.Invoke(msg);
            return 0;
        }

        /// <summary>
        /// Set Default IPC Message callback function.
        /// </summary>
        /// <param name="implementation">delegate function implementation.</param>
        public static void SetDefaultSndMessageCallback(IPCMessageCallback implementation)
        {
            DefaultSndMSGCallback = implementation;
        }
        public static void Navigate(Type type)
        {
            NavigationService.Navigate(type);
        }

        public void Refresh()
        {
            shellFrame.Navigate(typeof(GeneralSettings));
        }

        private bool navigationViewInitialStateProcessed; // avoid announcing initial state of the navigation pane.

        [SuppressMessage("Usage", "CA1801:Review unused parameters", Justification = "Params are required for event handler signature requirements.")]
#pragma warning disable CA1822 // Mark members as static
        private void NavigationView_PaneOpened(Microsoft.UI.Xaml.Controls.NavigationView sender, object args)
        {
            if (!navigationViewInitialStateProcessed)
            {
                navigationViewInitialStateProcessed = true;
                return;
            }

            var peer = FrameworkElementAutomationPeer.FromElement(sender);
            if (peer == null)
            {
                peer = FrameworkElementAutomationPeer.CreatePeerForElement(sender);
            }

            if (AutomationPeer.ListenerExists(AutomationEvents.MenuOpened))
            {
                var loader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView();
                peer.RaiseNotificationEvent(
                    AutomationNotificationKind.ActionCompleted,
                    AutomationNotificationProcessing.ImportantMostRecent,
                    loader.GetString("Shell_NavigationMenu_Announce_Open"),
                    "navigationMenuPaneOpened");
            }
        }

        [SuppressMessage("Usage", "CA1801:Review unused parameters", Justification = "Params are required for event handler signature requirements.")]
        private void NavigationView_PaneClosed(Microsoft.UI.Xaml.Controls.NavigationView sender, object args)
        {
            if (!navigationViewInitialStateProcessed)
            {
                navigationViewInitialStateProcessed = true;
                return;
            }

            var peer = FrameworkElementAutomationPeer.FromElement(sender);
            if (peer == null)
            {
                peer = FrameworkElementAutomationPeer.CreatePeerForElement(sender);
            }

            if (AutomationPeer.ListenerExists(AutomationEvents.MenuClosed))
            {
                var loader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView();
                peer.RaiseNotificationEvent(
                    AutomationNotificationKind.ActionCompleted,
                    AutomationNotificationProcessing.ImportantMostRecent,
                    loader.GetString("Shell_NavigationMenu_Announce_Collapse"),
                    "navigationMenuPaneClosed");
            }
        }


        private async void FeedbackItem_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri("https://aka.ms/ModernFlyoutsGiveFeedback"));
        }


        public string GetAppTitleFromSystem()
        {
            return Windows.ApplicationModel.Package.Current.DisplayName;
        }

    }
}