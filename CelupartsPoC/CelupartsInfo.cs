using System.ComponentModel.DataAnnotations;

namespace CelupartsPoC
{
    public class CelupartsInfo
    {
        [Key]
        public int IdCelupartsInfo { get; set; }
        public string ContactPhone { get; set; } = string.Empty;
        public string ContactEmail { get; set; } = string.Empty;
    }
}
