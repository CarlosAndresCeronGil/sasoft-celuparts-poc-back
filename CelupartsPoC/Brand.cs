using System.ComponentModel.DataAnnotations;

namespace CelupartsPoC
{
    public class Brand
    {
        [Key]
        public int IdBrand { get; set; }
        public string BrandName { get; set; } = string.Empty;
    }
}
