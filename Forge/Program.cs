using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Fiddler;

namespace Forge
{
    class Program
    {
        static void FiddlerApplication_AfterSessionComplete(Session ses)
        {
            if (ses.PathAndQuery != "/webui/GlueManager.js")
                return;

            ses.utilCreateResponseAndBypassServer();

            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "Forge.GlueManager.js";

            foreach (var name in assembly.GetManifestResourceNames())
            {
                Console.WriteLine(name);
            }

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                ses.LoadResponseFromStream(stream, null);
            }

            FiddlerApplication.Shutdown();
            Console.WriteLine("Enabled Offline Mode.");
        }

        [Obsolete]
        static void Main(string[] args)
        {
            try
            {
                var builder = new FiddlerCoreStartupSettingsBuilder();
                var settings = builder
                    .CaptureLocalhostTraffic()
                    .MonitorAllConnections()
                    .RegisterAsSystemProxy()
                    .Build();

                FiddlerApplication.BeforeRequest += FiddlerApplication_AfterSessionComplete;
                FiddlerApplication.Startup(settings);
                Console.WriteLine("Fiddler Proxy Enabled");

                var client = Instance.Start(@"Warcraft III.exe -launch -hd 1 -locale enUS", false);

                while (client.process.HasExited == false)
                {
                    byte[] hd_on = client.memory.Read(IntPtr.Zero + 0x2ACFBA9, 1);
                    if (hd_on[0] != 1)
                    {
                        Console.WriteLine("Forcing HD mode.");
                        client.memory.Write(IntPtr.Zero + 0x2ACFBA9, new byte[] { 0x01 }, false);
                    }
                }
            }
            finally
            {
                Console.WriteLine("Disabling Fiddler Proxy");
                FiddlerApplication.Shutdown();
            }
        }
    }
}
