using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomApiNetCore.Models
{
    public class ProductModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }
        public CategoryModel category { get; set; } = null!;
        [Required]
        public long category_id { get; set; }
        [StringLength(100)]
        public required string name { get; set; }
        [Column(TypeName = "text")]
        public required string description { get; set; }
        public float price { get; set; }
        public int stock { get; set; }
        [StringLength(100)]
        public required string created_by { get; set; }
        public DateTime created_at { get; set; } = DateTime.Now;
        [Column(TypeName = "text")]
        public string? image_path { get; set; }
        [Column(TypeName = "smallint")]
        public int is_deleted { get; set; } = 0;
    }
}
