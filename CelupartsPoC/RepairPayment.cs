using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CelupartsPoC
{
    public class RepairPayment
    {
        [Key]
        public int IdRepairPayment { get; set; }

        public int IdRepair { get; set; }
        [ForeignKey("IdRepair")]
        [JsonIgnore]
        public virtual Repair? Repair { get; set; }

        public string PaymentMethod { get; set; } = string.Empty;
        public string BillPayment { get; set; } = string.Empty;
        public DateTime? PaymentDate { get; set; }
    }
}
