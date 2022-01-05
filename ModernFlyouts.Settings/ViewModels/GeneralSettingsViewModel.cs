﻿using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO.Abstractions;
using System.Runtime.CompilerServices;
using ModernFlyouts.Settings.Helpers;
using ModernFlyouts.Settings.Interfaces;
using ModernFlyouts.Settings.Utilities;
using ModernFlyouts.Settings.ViewModels.Commands;
using ModernFlyouts.Settings;

using System.Threading.Tasks;
using System.Windows.Input;

using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

using ModernFlyouts.Settings.Services;

using Windows.ApplicationModel;
using Windows.UI.Xaml;


namespace ModernFlyouts.Settings.ViewModels
{
    public class GeneralSettingsViewModel : Observable
    {
        private GeneralSettings GeneralSettingsConfig { get; set; }

        public Func<string, int> UpdateUIThemeCallBack { get; }

        public Func<string, int> SendConfigMSG { get; }

        private string _settingsConfigFileFolder = string.Empty;

        public GeneralSettingsViewModel(ISettingsRepository<GeneralSettings> settingsRepository, Func<string, int> updateTheme, string configFileSubfolder = "", Action dispatcherAction = null)
        {

            // To obtain the general settings configuration of ModernFlyouts if it exists, else to create a new file and return the default configurations.
            if (settingsRepository == null)
            {
                throw new ArgumentNullException(nameof(settingsRepository));
            }

            GeneralSettingsConfig = settingsRepository.SettingsConfig;

            // set the callback function value to update the UI theme.
            UpdateUIThemeCallBack = updateTheme;

            UpdateUIThemeCallBack(GeneralSettingsConfig.Theme);

            // Update Settings file folder:
            _settingsConfigFileFolder = configFileSubfolder;

            // Using Invariant here as these are internal strings and fxcop
            // expects strings to be normalized to uppercase. While the theme names
            // are represented in lowercase everywhere else, we'll use uppercase
            // normalization for switch statements
            switch (GeneralSettingsConfig.Theme.ToUpperInvariant())
            {
                case "DARK":
                    _themeIndex = 0;
                    break;
                case "LIGHT":
                    _themeIndex = 1;
                    break;
                case "SYSTEM":
                    _themeIndex = 2;
                    break;
            }

            _startup = GeneralSettingsConfig.Startup;
        }

        private bool _startup;
        private int _themeIndex;

        // Gets or sets a value indicating whether run ModernFlyouts on start-up.
        public bool Startup
        {
            get
            {
                return _startup;
            }

            set
            {
                if (_startup != value)
                {
                    _startup = value;
                    GeneralSettingsConfig.Startup = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int ThemeIndex
        {
            get
            {
                return _themeIndex;
            }

            set
            {
                if (_themeIndex != value)
                {
                    switch (value)
                    {
                        case 0: GeneralSettingsConfig.Theme = "dark"; break;
                        case 1: GeneralSettingsConfig.Theme = "light"; break;
                        case 2: GeneralSettingsConfig.Theme = "system"; break;
                    }

                    _themeIndex = value;

                    try
                    {
                        UpdateUIThemeCallBack(GeneralSettingsConfig.Theme);
                    }
#pragma warning disable CA1031 // Do not catch general exception types
                    catch (Exception e)
#pragma warning restore CA1031 // Do not catch general exception types
                    {
                        //Logger.LogError("Exception encountered when changing Settings theme", e);
                    }

                    NotifyPropertyChanged();
                }
            }
        }

        // FxCop suggests marking this member static, but it is accessed through
        // an instance in autogenerated files (GeneralPage.g.cs) and will break
        // the file if modified
#pragma warning disable CA1822 // Mark members as static
        public string ModernFlyoutsVersion
#pragma warning restore CA1822 // Mark members as static
        {
            get
            {
                return Helper.GetProductVersion();
            }
        }

        public void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            // Notify UI of property change
            OnPropertyChanged(propertyName);
            OutGoingGeneralSettings outsettings = new OutGoingGeneralSettings(GeneralSettingsConfig);

            SendConfigMSG(outsettings.ToString());
        }


    // TODO WTS: Add other settings as necessary. For help see https://github.com/Microsoft/WindowsTemplateStudio/blob/release/docs/UWP/pages/settings.md

        //private ElementTheme _elementTheme = ThemeSelectorService.Theme;

        //public ElementTheme ElementTheme
        //{
        //    get { return _elementTheme; }

        //    set { SetProperty(ref _elementTheme, value); }
        //}

        //private string _versionDescription;

        //public string VersionDescription
        //{
        //    get { return _versionDescription; }

        //    set { SetProperty(ref _versionDescription, value); }
        //}

        //private ICommand _switchThemeCommand;

        //public ICommand SwitchThemeCommand
        //{
        //    get
        //    {
        //        if (_switchThemeCommand == null)
        //        {
        //            _switchThemeCommand = new RelayCommand<ElementTheme>(
        //                async (param) =>
        //                {
        //                    ElementTheme = param;
        //                    await ThemeSelectorService.SetThemeAsync(param);
        //                });
        //        }

        //        return _switchThemeCommand;
        //    }
        //}

        //public GeneralSettingsViewModel()
        //{
        //}

        //public async Task InitializeAsync()
        //{
        //    VersionDescription = GetVersionDescription();
        //    await Task.CompletedTask;
        //}

        //private string GetVersionDescription()
        //{
        //    var appName = "AppDisplayName".GetLocalized();
        //    var package = Package.Current;
        //    var packageId = package.Id;
        //    var version = packageId.Version;

        //    return $"{appName} - {version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
        //}




    }
}