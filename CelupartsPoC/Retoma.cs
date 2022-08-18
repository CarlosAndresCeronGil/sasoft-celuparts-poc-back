using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CelupartsPoC
{
    public class Retoma
    {
        [Key]
        public int IdRetoma { get; set; }

        public int IdRequest { get; set; }
        [ForeignKey("IdRequest")]
        [JsonIgnore]
        public virtual RequestWithEquipments? Request { get; set; }

        public int? IdTechnician { get; set; }
        [ForeignKey("IdTechnician")]
        public virtual Technician? Technician { get; set; }

        public string RetomaQuote { get; set; } = string.Empty;
        public string DeviceDiagnostic { get; set; } = string.Empty;
        public virtual List<RetomaPayment> RetomaPayments { get; set; } = new List<RetomaPayment>();
    }
}
