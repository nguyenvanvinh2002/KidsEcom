using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KidsEcomAPI.Data
{
    [Table("Thongbao")]
    public class Thongbao
    {
        [Key]
        public int? Id { get; set; }
        public string? UserName { get; set; }
        public string? Title { get; set; }
      
        public int? IdSp { get; set; }
        public string? Size { get; set; }
        public DateTime DateTime { get; set; }
        public string? TenSp { get; set; }
        public int? Status { get; set; } = 1;
    }
}
