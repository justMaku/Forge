using System;
using System.Runtime.InteropServices;

namespace Forge.Imports
{
    public static class NtDll
    {
        [Flags]
        public enum SectionAccess : uint
        {
            StandardRightsRequired = 0x000F0000,

            Query = 0x0001,
            MapWrite = 0x0002,
            MapRead = 0x0004,
            MapExecute = 0x0008,
            ExtendSize = 0x0010,

            All = StandardRightsRequired | Query | MapWrite | MapRead | MapExecute | ExtendSize
        }

        [DllImport("ntdll.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern uint NtUnmapViewOfSection(IntPtr processHandle, IntPtr baseAddress);

        [DllImport("ntdll.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern uint NtMapViewOfSection(
            IntPtr sectionHandle,
            IntPtr processHandle,
            ref IntPtr viewBase,
            uint zeroBits,
            IntPtr commitSize,
            ref IntPtr sectionOffset,
            ref IntPtr viewSize,
            int inheritDisposition,
            uint allocationType,
            MemoryProtection win32Protect);

        [DllImport("ntdll.dll", SetLastError = false)]
        public static extern IntPtr NtSuspendProcess(IntPtr processHandle);

        [DllImport("ntdll.dll", SetLastError = true)]
        public static extern IntPtr NtResumeProcess(IntPtr processHandle);

        [DllImport("ntdll.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern uint NtCreateSection(ref IntPtr pSectionHandle, SectionAccess desiredAccess, IntPtr zero,
            ref ulong pMaxSize, MemoryProtection pageAttributes, MemoryProtection sectionAttributes, IntPtr fileHandle);
    }
}