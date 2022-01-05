namespace ModernFlyouts.Settings.Utilities
{
    public interface IIOProvider
    {
        bool FileExists(string path);

        bool DirectoryExists(string path);

        bool CreateDirectory(string path);

        void DeleteDirectory(string path);

        void WriteAllText(string path, string content);

        string ReadAllText(string path);
    }
}
