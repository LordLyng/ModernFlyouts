namespace ModernFlyouts.Settings
{
    public interface ISettingsPath
    {
        bool SettingsFolderExists(string powertoy);

        void CreateSettingsFolder(string powertoy);

        void DeleteSettings(string powertoy = "");

        string GetSettingsPath(string powertoy, string fileName);
    }
}
