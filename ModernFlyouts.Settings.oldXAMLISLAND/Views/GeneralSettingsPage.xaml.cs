using System;

using ModernFlyouts.Settings.ViewModels;
using ModernFlyouts.Settings;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

using ModernFlyouts.Settings.Helpers;
using ModernFlyouts.Settings.Services;
using ModernFlyouts.Settings.Utilities;

using Windows.ApplicationModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Windows.ApplicationModel.Resources;
using Windows.Data.Json;
using Windows.UI.Core;


namespace ModernFlyouts.Settings.Views
{
    // TODO WTS: Add other settings as necessary. For help see https://github.com/Microsoft/WindowsTemplateStudio/blob/release/docs/UWP/pages/settings-codebehind.md
    // TODO WTS: Change the URL for your privacy policy in the Resource File, currently set to https://YourPrivacyUrlGoesHere

    public sealed partial class GeneralSettingsPage : Page
    {
        public GeneralSettingsViewModel ViewModel { get; set; }

        public GeneralSettingsPage()
        {
            InitializeComponent();

            // Load string resources
            ResourceLoader loader = ResourceLoader.GetForViewIndependentUse();
            var settingsUtils = new SettingsUtils();

            DataContext = ViewModel;
        }

        public static int UpdateUIThemeMethod(string themeName)
        {
            switch (themeName?.ToUpperInvariant())
            {
                case "LIGHT":
                    ShellPage.ShellHandler.RequestedTheme = ElementTheme.Light;
                    break;
                case "DARK":
                    ShellPage.ShellHandler.RequestedTheme = ElementTheme.Dark;
                    break;
                case "SYSTEM":
                    ShellPage.ShellHandler.RequestedTheme = ElementTheme.Default;
                    break;
                default:
                    // TODO WTS: Replace Logger with appcentre logging analysis
                    //Logger.LogError($"Unexpected theme name: {themeName}");
                    break;
            }

            return 0;
        }

        private void OpenColorsSettings_Click(object sender, RoutedEventArgs e)
        {
            Helpers.StartProcessHelper.Start(Helpers.StartProcessHelper.ColorsSettings);
        }
    }
}

    //    public GeneralSettings()
    //    {
    //        InitializeComponent();
    //    }

//    private ElementTheme _elementTheme = ThemeSelectorService.Theme;

//    public ElementTheme ElementTheme
//    {
//        get { return _elementTheme; }

//        set { Set(ref _elementTheme, value); }
//    }

//    private string _versionDescription;

//    public string VersionDescription
//    {
//        get { return _versionDescription; }

//        set { Set(ref _versionDescription, value); }
//    }

//    protected override async void OnNavigatedTo(NavigationEventArgs e)
//    {
//        await InitializeAsync();
//    }

//    private async Task InitializeAsync()
//    {
//        VersionDescription = GetVersionDescription();
//        await Task.CompletedTask;
//    }

//    private string GetVersionDescription()
//    {
//        var appName = "AppDisplayName".GetLocalized();
//        var package = Package.Current;
//        var packageId = package.Id;
//        var version = packageId.Version;

//        return $"{appName} - {version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
//    }

//    private async void ThemeChanged_CheckedAsync(object sender, RoutedEventArgs e)
//    {
//        var param = (sender as RadioButton)?.CommandParameter;

//        if (param != null)
//        {
//            await ThemeSelectorService.SetThemeAsync((ElementTheme)param);
//        }
//    }

//    public event PropertyChangedEventHandler PropertyChanged;

//    private void Set<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
//    {
//        if (Equals(storage, value))
//        {
//            return;
//        }

//        storage = value;
//        OnPropertyChanged(propertyName);
//    }

//    private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
//}
