using Forge.Imports;
using System;
using System.Diagnostics;

namespace Forge
{
    public class Instance
    {
        public readonly Process process;
        public readonly Memory memory;

        public static Instance Start(string path, bool suspended)
        {
            var flags = suspended ? Kernel32.ProcessCreationFlags.CreateSuspended : Kernel32.ProcessCreationFlags.None;
            var success = Kernel32.CreateProcess(null, path, IntPtr.Zero, IntPtr.Zero, false, flags, IntPtr.Zero, null, out var _, out var processInformation);

            var process = Process.GetProcessById((int)processInformation.ProcessId);

            return new Instance(process);
        }

        public Instance(Process process)
        {
            this.process = process;
            this.memory = new Memory(process);
        }

        public void Kill()
        {
            this.process.Kill();
        }
    }
}