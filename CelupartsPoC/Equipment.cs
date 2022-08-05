using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CelupartsPoC
{
    public class Equipment
    {
        [Key]
        public int IdEquipment { get; set; }
        public string TypeOfEquipment { get; set; } = string.Empty;
        public string EquipmentBrand { get; set; } = string.Empty;
        public string ModelOrReference { get; set; } = string.Empty;
        public string Imei { get; set; } = string.Empty;
        public string EquipmentInvoice { get; set; } = string.Empty;
        public virtual List<Request> Requests { get; set; } = new List<Request>();
    }
}
