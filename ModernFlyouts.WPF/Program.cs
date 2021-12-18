namespace ModernFlyouts.WPF
{
  public class Program
  {
    [System.STAThread()]
    public static void Main()
    {
      using (new ModernFlyouts.Settings.App())
      {
                App app = new App();
        app.InitializeComponent();
        app.Run();
      }
    }
  }
}