namespace ModernFlyouts.Settings.Interfaces
{
    public interface ISettingsRepository<T>
    {
        T SettingsConfig { get; set; }
    }
}
