using System;
using System.Diagnostics;
using System.Windows;

namespace ModernFlyouts.WPF
{
  public class Program
  {
    [STAThread]
    public static void Main()
    {
        using (new Settings.App())
        {
                App app = new App();
        app.InitializeComponent();
        app.Run();
        }
    }
  }
}