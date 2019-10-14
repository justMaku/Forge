using Forge.Imports;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Forge
{
    public class Memory
    {
        private readonly Process process;

        public Memory(Process process)
        {
            this.process = process;
        }

        #region Read/Write

        public bool Write(IntPtr address, byte[] data, bool requiresProtectionChange = false)
        {
            var processHandle = Kernel32.OpenProcess(ProcessAccessFlags.All, false, process.Id);
            var memoryBase = this.process.MainModule.BaseAddress;
            var rebased = (ulong)memoryBase + (ulong)address;

            var oldProtection = MemoryProtection.NoAccess;
            if (requiresProtectionChange)
            {
                Kernel32.VirtualProtect((IntPtr)rebased, data.Length, MemoryProtection.ExecuteReadWrite, out oldProtection);
            }

            var written = Kernel32.WriteProcessMemory(processHandle, (IntPtr)rebased, data, (IntPtr)data.Length, out var _);

            if (requiresProtectionChange)
            {
                Kernel32.VirtualProtect((IntPtr)rebased, data.Length, oldProtection, out oldProtection);
            }

            Kernel32.CloseHandle(processHandle);

            return written;
        }

        public byte[] Read(IntPtr address, Int32 length)
        {
            var memoryBase = this.process.MainModule.BaseAddress;
            var memorySize = this.process.MainModule.ModuleMemorySize;

            var processHandle = Kernel32.OpenProcess(ProcessAccessFlags.All, false, process.Id);
            var rebased = (ulong)memoryBase + (ulong)address;

            var copyBuffer = new byte[(int)length];
            var couldRead = Kernel32.ReadProcessMemory(processHandle, (IntPtr)rebased, copyBuffer, (IntPtr)length, out var numRead);

            Kernel32.CloseHandle(processHandle);

            return copyBuffer;
        }

        public byte[] ReadAll()
        {
            var memorySize = this.process.MainModule.ModuleMemorySize;
            return this.Read(IntPtr.Zero, memorySize);
        }

        #endregion Read/Write
    }
}