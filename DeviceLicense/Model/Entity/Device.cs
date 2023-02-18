using System.ComponentModel.DataAnnotations;

namespace DeviceLicense.Model.Entity
{
    public class Device
    {
        [Key]
        public int Id { get; set; } 
        public string DeviceModel { get; set; } 
        public string DeviceMac { get; set; }
        public string DeviceSN { get; set; }
        public bool Ativo { get; set; }
    }
}
