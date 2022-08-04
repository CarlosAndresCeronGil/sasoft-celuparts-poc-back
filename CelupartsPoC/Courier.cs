using System.ComponentModel.DataAnnotations;

namespace CelupartsPoC
{
    public class Courier
    {
        [Key]
        public int IdCourier { get; set; }
        public string Names { get; set; } = string.Empty;
        public string Surnames { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string AccountStatus { get; set; } = string.Empty;
    }
}
