using DeviceLicense.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Newtonsoft.Json;
using Microsoft.IdentityModel.Tokens;
using System.Numerics;
using static System.Net.Mime.MediaTypeNames;
using DeviceLicense.Model.CollectorData;

namespace DeviceLicense.Service
{
    class WindowsService
    {
        public WindowsService() 
        { 
        }

        public void start(string[] args)
        {
            try
            {
                //Console.WriteLine("Deu certo!!!");
                //var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

                var builder = WebApplication.CreateBuilder(args);

                // Add services to the container.
                //Conexao com o Banco de dados
                var connectionString = builder.Configuration.GetConnectionString("Producao");
                builder.Services.AddDbContext<DeviceDbContext>(options =>
                    options.UseSqlServer(connectionString));

                builder.Services.AddControllers();
                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();

                //var teste = File.ReadAllText(Application.StartupPath + "\\Teste");
                //var teste = _context.Devices.Find(1);
                //Console.WriteLine(teste.DeviceMac);

                //Define os dominios na lista de permissao do CORS
                builder.Services.AddCors(options =>
                {
                    options.AddPolicy(name: "AllowAnyOrigin",
                        builder =>
                        {
                            //policy.WithOrigins("http://localhost", "http://localhost:7800", "http://127.0.0.1:5500").AllowAnyHeader().AllowAnyMethod();
                            builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                        });
                });

                //Define o IP da maquina como como bind da API e portas
                builder.WebHost.ConfigureKestrel(options =>
                {
                    options.ListenAnyIP(5224);
                    options.ListenAnyIP(7019, ListenOptions => ListenOptions.UseHttps());
                });

                var app = builder.Build();

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseMigrationsEndPoint();
                    app.UseExceptionHandler("/Error");
                    app.UseHsts();
                    using var scope = app.Services.CreateScope();
                    //Deploy do Banco com add-migration
                    var db = scope.ServiceProvider.GetRequiredService<DeviceDbContext>();
                    db.Database.Migrate();

                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

                app.UseCors("AllowAnyOrigin");

                app.UseHttpsRedirection();

                app.UseAuthorization();

                app.MapControllers();

                app.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exceção gerada: " + ex.Message);
            }
        }
        public void stop()
        {
            Console.WriteLine("API encerrada");
        }
    }
}
