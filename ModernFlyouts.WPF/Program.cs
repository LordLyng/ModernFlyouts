using System;
using System.Collections.Generic;
using System.Text;

namespace ModernFlyouts.WPF
{
    public class Program
    {
        public const string AppName = "ModernFlyouts";

        [System.STAThreadAttribute()]
        public static void Main()
        {
            using (new ModernFlyouts.Settings.App())
            {
                ModernFlyouts.WPF.App app = new ModernFlyouts.WPF.App();
                app.InitializeComponent();
                app.Run();
            }
        }
    }
}
