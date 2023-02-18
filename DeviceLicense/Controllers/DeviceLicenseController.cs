using DeviceLicense.Model;
using DeviceLicense.Model.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.WebEncoders.Testing;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;

namespace DeviceLicense.Controllers
{
    //Define a rota
    [ApiController]
    [Route("[controller]")]
    public class DeviceLicenseController : ControllerBase
    {
        
        //Define a instância do Banco
        private readonly DeviceDbContext _context;
        public DeviceLicenseController(DeviceDbContext context)
        {
            _context = context;
        }
        [HttpPost(Name = "PostDeviceLicense")]
        public string Validation(ColetorSeparacao coletor)
        {
            try
            {
                // Obter data do banco
                var logModel = _context.Devices.Where(model => model.DeviceModel == coletor.VelocityModel && model.DeviceMac == coletor.VelocityMac && model.DeviceSN == coletor.VelocitySN).FirstOrDefault();
                if (logModel != null)
                {
                    if (logModel.Ativo)
                    {
                        //var resultado = new
                        //{
                        //    message = "Dispositivo autorizado"
                        //};
                        return "Dispositivo autorizado";
                    }
                    else
                    {
                        //var resultado = new
                        //{                       
                        //    message = "Coletor não ativo"
                        //};
                        return "Coletor não ativo";
                    }
                }
                else
                {
                    //var resultado = new
                    //{
                    //    message = "Coletor não autorizado"
                    //};
                    return "Coletor não autorizado";
                }
            }
            catch (Exception ex)
            {
                //var resultado = new
                //{
                //    isFalse = "false",
                //    message = "Erro: "
                //};
                return "Erro Inesperado: " + ex;
            }
        }
    }
}
