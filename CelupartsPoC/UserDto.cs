namespace CelupartsPoC
{
    public class UserDto
    {
        public int? IdUser { get; set; }
        public string? IdType { get; set; } = string.Empty;
        public string? Names { get; set; } = string.Empty;
        public string? Surnames { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string AlternativePhone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string? AccountStatus { get; set; } = string.Empty;
        public virtual List<Request>? Requests { get; set; } = new List<Request>()!;
    }

    public class UserDtoWithRequests
    {
        public int? IdUser { get; set; }
        public string? IdType { get; set; } = string.Empty;
        public string? Names { get; set; } = string.Empty;
        public string? Surnames { get; set; } = string.Empty;
        public int? Phone { get; set; }
        public int? AlternativePhone { get; set; }
        public string Email { get; set; } = string.Empty;
        public string? AccountStatus { get; set; } = string.Empty;
        public virtual List<Request>? Requests { get; set; } = new List<Request>()!;
    }
}
