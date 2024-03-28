using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CustomApiNetCore.Models
{
    public class CategoryModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }
        public ICollection<ProductModel> products { get; } = new List<ProductModel>();
        [StringLength(100)]
        public required string name { get; set; }
        [Column(TypeName = "text")]
        public required string description { get; set; }
        [StringLength(100)]
        public required string created_by { get; set; }
        public DateTime created_at { get; set; } = DateTime.Now;
        [Column(TypeName = "smallint")]
        public int is_deleted { get; set; } = 0;
    }
}
