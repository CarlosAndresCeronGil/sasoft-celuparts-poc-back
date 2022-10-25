using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CelupartsPoC
{
    public class RetomaPayment
    {
        [Key]
        public int IdRetomaPayment { get; set; }

        public int IdRetoma { get; set; }
        [ForeignKey("IdRetoma")]
        [JsonIgnore]
        public virtual Retoma? Retoma { get; set; }

        public string PaymentMethod { get; set; } = string.Empty;
        public DateTime? PaymentDate { get; set; }
        public string VoucherNumber { get; set; } = string.Empty;
        public string BillPaymentPath { get; set; } = string.Empty;
    }
}
