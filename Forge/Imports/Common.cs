using System;

namespace Forge.Imports
{
    public enum MemoryProtection : uint
    {
        Execute = 0x00000010,
        ExecuteRead = 0x00000020,
        ExecuteReadWrite = 0x00000040,
        ExecuteWriteCopy = 0x00000080,
        NoAccess = 0x00000001,
        Readonly = 0x00000002,
        Readwrite = 0x00000004,
        WriteCopy = 0x00000008,
        Guard = 0x00000100,
        Nocache = 0x00000200,
        WriteCombine = 0x00000400,
        Commit = 0x8000000
    }

    [Flags]
    public enum AllocationType : uint
    {
        Commit = 0x1000, //const uint MEM_COMMIT           0x1000
        Reserve = 0x2000, //const uint MEM_RESERVE          0x2000
        Decommit = 0x4000, //const uint MEM_DECOMMIT         0x4000
        Release = 0x8000, //const uint MEM_RELEASE          0x8000
        Free = 0x10000, //const uint MEM_FREE            0x10000
        Private = 0x20000, //const uint MEM_PRIVATE         0x20000
        Mapped = 0x40000, //const uint MEM_MAPPED          0x40000
        Reset = 0x80000, //const uint MEM_RESET           0x80000
        TopDown = 0x100000, //const uint MEM_TOP_DOWN       0x100000
        WriteWatch = 0x200000, //const uint MEM_WRITE_WATCH    0x200000
        Physical = 0x400000, //const uint MEM_PHYSICAL       0x400000
        Rotate = 0x800000, //const uint MEM_ROTATE         0x800000
        LargePages = 0x20000000, //const uint MEM_LARGE_PAGES  0x20000000
        FourMbPages = 0x80000000 //const uint MEM_4MB_PAGES    0x80000000
    }

    [Flags]
    public enum ProcessAccessFlags
    {
        All = 0x001F0FFF,
        Terminate = 0x00000001,
        CreateThread = 0x00000002,
        VmOperation = 0x00000008,
        VmRead = 0x00000010,
        VmWrite = 0x00000020,
        DupHandle = 0x00000040,
        SetInformation = 0x00000200,
        QueryInformation = 0x00000400,
        Synchronize = 0x00100000
    }
}