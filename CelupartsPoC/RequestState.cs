using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CelupartsPoC
{
    public class RequestState
    {
        [Key]
        public int IdRequestState { get; set; }
        public int IdRequest { get; set; }

        [ForeignKey("IdRequest")]
        [JsonIgnore]
        public virtual Request? Request { get; set; }
        public string PaymentStatus { get; set; } = string.Empty;
        public bool ProductReturned { get; set; }
    }
}
