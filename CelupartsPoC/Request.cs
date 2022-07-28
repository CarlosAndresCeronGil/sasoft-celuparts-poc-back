using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CelupartsPoC
{
    public class Request
    {
        [Key]
        public int IdRequest { get; set; }

        public int? IdUser { get; set; } 

        [ForeignKey("IdUser")]
        [JsonIgnore]
        public virtual UserDto? UserDto { get; set; }
        public string RequestType { get; set; } = string.Empty;
        public string PickUpAddress { get; set; } = string.Empty;
        public string DeliveryAddress { get; set; } = string.Empty;
        public DateTime PickUpTime { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public int Quote { get; set; } = 0;
        public string StatusQuote { get; set; } = string.Empty;
        public string TechnicalRemarks { get; set; } = string.Empty;
        public virtual List<Equipment>? Equipments { get; set; } = new List<Equipment>()!;
        public virtual List<RequestState> RequestStates { get; set; } = new List<RequestState>()!;
    }

    public class RequestWithEquipments
    {
        [Key]
        public int IdRequest { get; set; }
        public int? IdUser { get; set; }

        [ForeignKey("IdUser")]
        [JsonIgnore]
        public virtual UserDto? UserDto { get; set; }
        public string RequestType { get; set; } = string.Empty;
        public string PickUpAddress { get; set; } = string.Empty;
        public string DeliveryAddress { get; set; } = string.Empty;
        public DateTime PickUpTime { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public int Quote { get; set; } = 0;
        public string StatusQuote { get; set; } = string.Empty;
        public string TechnicalRemarks { get; set; } = string.Empty;
        public virtual List<Equipment>? Equipments { get; set; } = new List<Equipment>()!;
        public virtual List<RequestState> RequestStates { get; set; } = new List<RequestState>()!;
    }
}
