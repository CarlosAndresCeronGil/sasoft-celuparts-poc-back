using System.ComponentModel.DataAnnotations;

namespace CelupartsPoC
{
    [Index(nameof(Email), IsUnique = true)]
    [Index(nameof(IdNumber), IsUnique = true)]
    public class UserDto
    {
        [Key]
        public int? IdUser { get; set; }
        public string IdType { get; set; } = string.Empty;
        public string IdNumber { get; set; } = string.Empty;
        public string Names { get; set; } = string.Empty;
        public string Surnames { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string AlternativePhone { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string AccountStatus { get; set; } = string.Empty;
        public int LoginAttempts { get; set; } = 0;
        public string TokenRecovery { get; set; } = string.Empty;
        public virtual List<RequestWithEquipments>? Requests { get; set; } = new List<RequestWithEquipments>()!;
    }

    public class UserDtoWithRequests
    {
        public int? IdUser { get; set; } = 0!;
        public string IdType { get; set; } = string.Empty;
        public string IdNumber { get; set; } = string.Empty;
        public string Names { get; set; } = string.Empty;
        public string Surnames { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string AlternativePhone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string AccountStatus { get; set; } = string.Empty;
        public virtual List<RequestWithEquipments>? Requests { get; set; } = new List<RequestWithEquipments>()!;
    }

}
