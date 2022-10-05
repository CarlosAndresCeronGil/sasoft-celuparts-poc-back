using System.ComponentModel.DataAnnotations;

namespace CelupartsPoC
{
    public class TypeOfEquipment
    {
        [Key]
        public int IdTypeOfEquipment { get; set; }
        public string EquipmentTypeName { get; set; } = string.Empty;
    }
}
