using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;

namespace Forge.Imports
{
    public static class Kernel32
    {
        [Flags]
        public enum LoadLibraryExFlags : uint
        {
            DontResolveDllReferences = 0x00000001, //const uint DONT_RESOLVE_DLL_REFERENCES         0x00000001
            LoadLibraryAsDatafile = 0x00000002, //const uint LOAD_LIBRARY_AS_DATAFILE            0x00000002
            LoadLibraryWithAlteredSearchPath = 0x00000008, //const uint LOAD_WITH_ALTERED_SEARCH_PATH       0x00000008
            LoadIgnoreCodeAuthzLevel = 0x00000010, //const uint LOAD_IGNORE_CODE_AUTHZ_LEVEL        0x00000010
            LoadLibraryAsImageResource = 0x00000020, //const uint LOAD_LIBRARY_AS_IMAGE_RESOURCE      0x00000020
            LoadLibraryAsDatafileExclusive = 0x00000040 //const uint LOAD_LIBRARY_AS_DATAFILE_EXCLUSIVE  0x00000040
        }

        [Flags]
        public enum ProcessCreationFlags : uint
        {
            None = 0x00000000,

            DebugProcess = 0x00000001,
            DebugOnlyThisProcess = 0x00000002,
            CreateSuspended = 0x00000004,
            DetachedProcess = 0x00000008,
            CreateNewConsole = 0x00000010,

            CreateNewProcessGroup = 0x00000200,
            CreateUnicodeEnvironment = 0x00000400,
            CreateSeparateWowVDM = 0x00000800,
            CreateSharedWowVDM = 0x00001000,

            InheritParentAffinity = 0x00010000,
            CreateProtectedProcess = 0x00040000,
            ExtendedStartupInfoPresent = 0x00080000,

            CreateBreakawayFromJob = 0x01000000,
            CreatePreserveCodeAuthzLevel = 0x02000000,
            CreateDefaultErrorMode = 0x04000000,
            CreateNoWindow = 0x08000000
        }

        [Flags]
        public enum ThreadWaitValue : uint
        {
            Object0 = 0x00000000,
            Abandoned = 0x00000080,
            Timeout = 0x00000102,
            Failed = 0xFFFFFFFF,
            Infinite = 0xFFFFFFFF
        }

        [DllImport("kernel32.dll", EntryPoint = "CreateProcessA", SetLastError = true)]
        public static extern bool CreateProcess(string lpApplicationName, string lpCommandLine,
            IntPtr lpProcessAttributes, IntPtr lpThreadAttributes, bool bInheritHandles,
            ProcessCreationFlags dwCreationFlags,
            IntPtr lpEnvironment, string lpCurrentDirectory, out StartupInfo lpStartupInfo,
            out ProcessInformation lpProcessInformation);

        [DllImport("kernel32.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer,
            IntPtr nSize,
            out int lpNumberOfBytesWritten);

        [DllImport("kernel32.dll")]
        public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer,
            IntPtr nSize,
            out UIntPtr lpNumberOfBytesWritten);

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(ProcessAccessFlags processAccess, bool bInheritHandle, int processId);

        [DllImport("kernel32.dll")]
        public static extern uint VirtualQueryEx(IntPtr hProcess, IntPtr lpAddress,
            out MemoryBasicInformation lpBuffer,
            int dwLength);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        public static extern IntPtr VirtualAllocEx(
            IntPtr hProcess,
            IntPtr lpAddress,
            IntPtr dwSize,
            AllocationType flAllocationType,
            MemoryProtection flProtect);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool VirtualFreeEx(
            IntPtr hProcess,
            IntPtr lpAddress,
            uint dwSize,
            AllocationType dwFreeType);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool VirtualProtect(IntPtr lpAddress, int dwSize, MemoryProtection flNewProtect, out MemoryProtection lpflOldProtect);

        [DllImport("kernel32.dll")]
        public static extern bool VirtualProtectEx(IntPtr hProcess, IntPtr lpAddress, IntPtr dwSize, MemoryProtection newProtection, out MemoryProtection oldProtection);

        [DllImport("kernel32.dll", SetLastError = true)]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CloseHandle(IntPtr hObject);

        [DllImport("kernel32.dll", EntryPoint = "CreateRemoteThread", SetLastError = true)]
        public static extern IntPtr CreateRemoteThread(
            IntPtr hProcess,
            IntPtr lpThreadAttributes,
            uint dwStackSize,
            IntPtr lpStartAddress,
            IntPtr lpParameter,
            uint dwCreationFlags,
            [Out] IntPtr lpThreadId);

        [DllImport("kernel32.dll", EntryPoint = "GetModuleHandleW", SetLastError = true)]
        public static extern IntPtr GetModuleHandle(
            [MarshalAs(UnmanagedType.LPWStr)] string lpModuleName);

        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

        [DllImport("kernel32", EntryPoint = "WaitForSingleObject")]
        public static extern ThreadWaitValue WaitForSingleObject(IntPtr hObject, uint dwMilliseconds);

        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetExitCodeThread(IntPtr hThread, out IntPtr lpExitCode);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr LoadLibraryEx(string lpFileName, IntPtr hReservedNull, LoadLibraryExFlags dwFlags);

        [DllImport("kernel32.dll")]
        public static extern int GetCurrentThreadId();

        [StructLayout(LayoutKind.Sequential)]
        public struct MemoryBasicInformation
        {
            public IntPtr BaseAddress;
            public IntPtr AllocationBase;
            public MemoryProtection AllocationProtection;
            public IntPtr RegionSize;
            public uint State;
            public MemoryProtection AccessProtection;
            public uint Type;

            public static int Size => Marshal.SizeOf(typeof(MemoryBasicInformation));
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct StartupInfo
        {
            public uint Cb;
            public string Reserved;
            public string Desktop;
            public string Title;
            public uint X;
            public uint Y;
            public uint XSize;
            public uint YSize;
            public uint XCountChars;
            public uint YCountChars;
            public uint FillAttribute;
            public uint Flags;
            public short ShowWindow;
            public short Reserved2;
            public IntPtr ReservedHandle;
            public IntPtr StdInputHandle;
            public IntPtr StdOutputHandle;
            public IntPtr StdErrorHandle;

            public static int Size => Marshal.SizeOf(typeof(StartupInfo));
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ProcessInformation
        {
            public IntPtr ProcessHandle;
            public IntPtr ThreadHandle;
            public uint ProcessId;
            public uint ThreadId;

            public static int Size => Marshal.SizeOf(typeof(ProcessInformation));
        }
    }
}