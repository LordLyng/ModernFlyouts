﻿// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Threading;
using ModernFlyouts.Settings.Library;
using ModernFlyouts.Settings.OOBE.Enums;
using ModernFlyouts.Settings.OOBE.ViewModel;
using ModernFlyouts.Settings.Views;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace ModernFlyouts.Settings.OOBE.Views
{
    public sealed partial class OobeColorPicker : Page
    {
        public OobePowerToysModule ViewModel { get; set; }

        public OobeColorPicker()
        {
            this.InitializeComponent();
            ViewModel = new OobePowerToysModule(OobeShellPage.OobeShellHandler.Modules[(int)PowerToysModulesEnum.ColorPicker]);
            DataContext = ViewModel;
        }

        private void Start_ColorPicker_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (OobeShellPage.ColorPickerSharedEventCallback != null)
            {
                using (var eventHandle = new EventWaitHandle(false, EventResetMode.AutoReset, OobeShellPage.ColorPickerSharedEventCallback()))
                {
                    eventHandle.Set();
                }
            }

            ViewModel.LogRunningModuleEvent();
        }

        private void SettingsLaunchButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (OobeShellPage.OpenMainWindowCallback != null)
            {
                OobeShellPage.OpenMainWindowCallback(typeof(ColorPickerPage));
            }

            ViewModel.LogOpeningSettingsEvent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel.LogOpeningModuleEvent();
            HotkeyControl.Keys = SettingsRepository<ColorPickerSettings>.GetInstance(new SettingsUtils()).SettingsConfig.Properties.ActivationShortcut.GetKeysList();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            ViewModel.LogClosingModuleEvent();
        }
    }
}
