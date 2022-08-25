using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CelupartsPoC
{
    public class RequestNotification
    {
        [Key]
        public int IdRequestNotification { get; set; }

        public int? IdRequest { get; set; }
        [ForeignKey("IdRequest")]
        [JsonIgnore]
        public virtual RequestWithEquipments? Request { get; set; }

        public string Message { get; set; } = string.Empty;
        public bool HideNotification { get; set; } = false;
        public string NotificationType { get; set; } = string.Empty;
    }
}
