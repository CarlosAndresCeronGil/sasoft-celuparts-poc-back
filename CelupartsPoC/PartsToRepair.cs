using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CelupartsPoC
{
    public class PartsToRepair
    {
        [Key]
        public int IdPartsToRepair { get; set; }

        public int IdRepair { get; set; }
        [ForeignKey("IdRepair")]
        [JsonIgnore]
        public virtual Repair? Repair { get; set; }
        public string Part { get; set; } = string.Empty;
        public bool ToReplace { get; set; }
        public bool ToRepair { get; set; }
    }
}
