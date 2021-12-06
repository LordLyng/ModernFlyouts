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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1310:Field names should not contain underscore", Justification = "Interop naming consistency")]
        public const int SW_HIDE = 0;
    }
}
