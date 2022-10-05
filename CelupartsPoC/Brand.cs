using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CelupartsPoC
{
    public class Brand
    {
        [Key]
        public int IdBrand { get; set; }

        public int IdTypeOfEquipment { get; set; }
        [ForeignKey("IdTypeOfEquipment")]
        [JsonIgnore]
        public virtual TypeOfEquipment? TypeOfEquipment { get; set; }

        public string BrandName { get; set; } = string.Empty;
    }
}
