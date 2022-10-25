using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CelupartsPoC
{
    public class RequestHistory
    {
        [Key]
        public int IdRequestHistory { get; set; }

        public int? IdRequest { get; set; }
        [ForeignKey("IdRequest")]
        [JsonIgnore]
        public virtual RequestWithEquipments? Request { get; set; }

        public string Status { get; set; } = string.Empty;
        public DateTime? Date { get; set; }
    }
}
