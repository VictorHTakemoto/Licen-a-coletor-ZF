using DeviceLicense.Model;
using DeviceLicense.Model.CollectorData;
using Microsoft.AspNetCore.Mvc;
using ONM.SysCentric.Sec;
using System.Runtime.InteropServices;
using System.Security.Permissions;

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
            //Criptografa entrada de dados da API.
            Security lic = new();
            string modelEncrypt = lic.CodificarString(coletor.VelocityModel);
            string macEncrypt = lic.CodificarString(coletor.VelocityMac);
            var snEncrypt = lic.CodificarString(coletor.VelocitySN);

            try
            {
                // Obter data do banco.
                var logModel = _context.Devices.Where(model => model.DeviceModel == modelEncrypt && model.DeviceMac == macEncrypt && model.DeviceSN == snEncrypt).FirstOrDefault();
                //Valida entrada dos dados do coletor.
                if (logModel != null)
                {
                    if (logModel.Ativo)
                    {
                        var response = _context.Messages.Find(1).ResponseMessage;
                        return response;
                    }
                    else
                    {
                        var response = _context.Messages.Find(2).ResponseMessage;
                        AtualizarLog(coletor.VelocityModel, coletor.VelocityMac, coletor.VelocitySN, response);
                        return response;
                    }
                }
                else
                {
                   
                    var response = _context.Messages.Find(3).ResponseMessage;
                    AtualizarLog(coletor.VelocityModel, coletor.VelocityMac, coletor.VelocitySN, response);
                    return response;
                }
            }
            catch (Exception ex)
            {
                
                var response = _context.Messages.Find(4).ResponseMessage + ex;
                AtualizarLog(coletor.VelocityModel, coletor.VelocityMac, coletor.VelocitySN, response);
                return response;
            }
        }

        //Salva log no banco se o coletor não for autorizado.
        #region
        void AtualizarLog(string model, string mac, string sn, string message)
        {
            _context.Logs.Add(new()
            {
                Id = Guid.NewGuid(),
                Model = model,
                Mac = mac,
                SerialNumber = sn,
                EventTime = DateTime.Now,
                Message = message
            });
            _context.SaveChanges();
        }
        #endregion
    }
}
