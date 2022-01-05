using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace ModernFlyouts.Settings.Utilities
{
    internal static class NativeMethods
    {
        [DllImport("user32.dll")]
        public static extern bool AllowSetForegroundWindow(int dwProcessId);

        public const int SWRESTORE = 9;

        [System.Runtime.InteropServices.DllImport("User32.dll")]
        public static extern bool SetForegroundWindow(IntPtr handle);

        [System.Runtime.InteropServices.DllImport("User32.dll")]
        public static extern bool ShowWindow(IntPtr handle, int nCmdShow);

        [System.Runtime.InteropServices.DllImport("User32.dll")]
        public static extern bool IsIconic(IntPtr handle);
    }
}
