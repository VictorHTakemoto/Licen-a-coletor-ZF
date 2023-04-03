using DeviceLicense.Model;
using DeviceLicense.Service;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Topshelf;

HostFactory.Run(p =>
            {
                p.Service<WindowsService>(s =>
                {
                    s.ConstructUsing(st => new WindowsService());
                    s.WhenStarted(st => st.start(args));
                    s.WhenStopped(st => st.stop());
                });
                p.RunAsLocalSystem();
                p.SetDescription("API Licen�a Coletor de Dados");
                p.SetDisplayName("API Licen�a");
                p.SetServiceName("Licen�a Coletor");
            });
