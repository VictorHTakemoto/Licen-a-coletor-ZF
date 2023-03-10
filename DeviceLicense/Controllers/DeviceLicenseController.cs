using DeviceLicense.Model;
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
            // TODO: Integrar lib de criptografia

            Security lic = new Security();
            //string info = lic.hostname + "|" + lic.osbuild + "|" + lic.ossn + "|" + lic.mbsn + "|" + lic.biossn;
            //string modelencrypt = lic.codificarstring(coletor.velocitymodel, info);
            //string macencrypt = lic.codificarstring(coletor.velocitymac, info);
            //string snencrypt = lic.codificarstring(coletor.velocitysn, info);
            try
            {
                // Obter data do banco
                var logModel = _context.Devices.Where(model => model.DeviceModel == coletor.VelocityModel && model.DeviceMac == coletor.VelocityMac && model.DeviceSN == coletor.VelocitySN).FirstOrDefault();
                //Valida entrada dos dados do coletor
                if (logModel != null)
                {
                    if (logModel.Ativo)
                    {

                        var response = "Dispositivo autorizado";
                        return response;
                    }
                    else
                    {
                        var response = "Coletor não ativo";
                        AtualizarLog(coletor.VelocityModel, coletor.VelocityMac, coletor.VelocitySN, response);
                        return response;
                    }
                }
                else
                {
                   
                    var response = "Coletor não autorizado";
                    AtualizarLog(coletor.VelocityModel, coletor.VelocityMac, coletor.VelocitySN, response);
                    return response;
                }
            }
            catch (Exception ex)
            {
                
                var response = "Erro Inesperado: " + ex;
                AtualizarLog(coletor.VelocityModel, coletor.VelocityMac, coletor.VelocitySN, response);
                return response;
            }
        }

        //Salva log no banco se o coletor não for autorizado
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
