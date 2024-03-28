using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomApiNetCore.Models
{
    public class LogModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long log_id { get; set; }
        [StringLength(20)]
        public string created_by { get; set; }
        public DateTime created_at { get; set; }
        [StringLength(100)]
        public string action { get; set; }
        [StringLength(250)]
        public string description { get; set; }
    }
}
