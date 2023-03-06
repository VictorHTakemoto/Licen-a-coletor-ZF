using DeviceLicense.Model;
using DeviceLicense.Service;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Topshelf;

namespace DeviceLicense
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(p =>
            {
                p.Service<WindowsService>(s =>
                {
                    s.ConstructUsing(st => new WindowsService());
                    s.WhenStarted(st => st.start(args));
                    s.WhenStopped(st => st.stop());
                });
                p.RunAsLocalSystem();
                p.SetDescription("API Licenciamento Dispositivo");
                p.SetDisplayName("Licenciamento Dispositivo");
                p.SetServiceName("Licenciamento");
            });
        }
    }
}
