using DeviceLicense.Model.Entity;
using Microsoft.Identity.Client;
using ONM.SysCentric;
namespace DeviceLicense.Model.SecurityDevice
{
    public class EncryptDevice
    {
        public void Info()
        {
            Security lic = new Security();
            Device device = new Device();

            string info = lic.Hostname + "|" + lic.OSBuild + "|" + lic.OSSN + "|" + lic.MBSN + "|" + lic.BiosSN;
            string modelEncrypt = lic.CodificarString(device.DeviceModel, info);
            string macEncrypt = lic.CodificarString(device.DeviceMac, info);
            string snEncrypt = lic.CodificarString(device.DeviceSN, info);
            //Console.WriteLine("Modelo: " + modelEncrypt + "\r\nMac: " + macEncrypt + "\r\nSerial: " + snEncrypt);
        }
    }

}
