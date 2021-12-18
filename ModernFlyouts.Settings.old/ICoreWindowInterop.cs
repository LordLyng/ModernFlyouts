using System.Runtime.InteropServices;

namespace ModernFlyouts.Settings
{
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("45D64A29-A63E-4CB6-B498-5781D298CB4F")]
    internal interface ICoreWindowInterop
    {
        System.IntPtr WindowHandle { get; }

        void MessageHandled(bool value);
    }
}
