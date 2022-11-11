using System.ComponentModel.DataAnnotations;

namespace CelupartsPoC
{
    public class PartsInfo
    {
        [Key]
        public int IdPartsInfo { get; set; }
        public string PartName { get; set; } = string.Empty;
    }
}
