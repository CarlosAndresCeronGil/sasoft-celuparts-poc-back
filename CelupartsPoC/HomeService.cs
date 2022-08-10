using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CelupartsPoC
{
    public class HomeService
    {
        [Key]
        public int IdHomeService { get; set; }

        public int? IdRequest { get; set; }
        [ForeignKey("IdRequest")]
        [JsonIgnore]
        public virtual RequestWithEquipments? Request { get; set; }

        public int? IdCourier { get; set; }
        [ForeignKey("IdCourier")]
        [JsonIgnore]
        public virtual Courier? Courier { get; set; }

        public DateTime PickUpDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
    }
}
