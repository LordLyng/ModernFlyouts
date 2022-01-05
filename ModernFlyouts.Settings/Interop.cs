using System.Runtime.InteropServices;

namespace ModernFlyouts.Settings
{
    internal static class Interop
    {
        public static ICoreWindowInterop GetInterop(this Windows.UI.Core.CoreWindow @this)
        {
            var unkIntPtr = Marshal.GetIUnknownForObject(@this);
            try
            {
                var interopObj = Marshal.GetTypedObjectForIUnknown(unkIntPtr, typeof(ICoreWindowInterop)) as ICoreWindowInterop;
                return interopObj;
            }
            finally
            {
                Marshal.Release(unkIntPtr);
                unkIntPtr = System.IntPtr.Zero;
            }
        }

        public const int SW_HIDE = 0;
    }
}
