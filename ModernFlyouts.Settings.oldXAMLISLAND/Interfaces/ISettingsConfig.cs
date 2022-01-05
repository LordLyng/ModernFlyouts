namespace ModernFlyouts.Settings.Interfaces
{
    // Common interface to be implemented by all the objects which get and store settings properties.
    public interface ISettingsConfig
    {
        string ToJsonString();

        string GetModuleName();

        bool UpgradeSettingsConfiguration();
    }
}
