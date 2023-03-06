using Azure.Core.Pipeline;
using System.ComponentModel.DataAnnotations;
using System.Reflection.PortableExecutable;

namespace DeviceLicense.Model.Entity
{
    public class Logs
    {
        [Key]
        public Guid Id { get; set; }
        public string Model { get; set; }
        public string Mac { get; set; }
        public string SerialNumber { get; set; }
        public DateTime EventTime { get; set; }
        public string Message { get; set; }
    }
}
