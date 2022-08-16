using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CelupartsPoC
{
    public class RequestStatus
    {
        [Key]
        public int IdRequestStatus { get; set; }
        public int IdRequest { get; set; }

        [ForeignKey("IdRequest")]
        [JsonIgnore]
        public virtual RequestWithEquipments? Request { get; set; }
        public string Status { get; set; } = string.Empty;
        public string PaymentStatus { get; set; } = string.Empty;
        public bool ProductReturned { get; set; }
        public bool ProductSold { get; set; }
    }
}
