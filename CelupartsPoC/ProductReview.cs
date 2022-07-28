using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CelupartsPoC
{
    public class ProductReview
    {
        [Key]
        public int IdProductReview { get; set; }

        public int IdRequest { get; set; }

        [ForeignKey("IdRequest")]
        [JsonIgnore]
        public virtual Request? Request { get; set; }
        public DateTime RepairDate { get; set; }
    }
}
