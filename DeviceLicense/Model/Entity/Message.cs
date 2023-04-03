using System.ComponentModel.DataAnnotations;

namespace DeviceLicense.Model.Entity
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        public string ResponseMessage { get; set; }
    }
}
